window.paymentQr = (function () {
  let bankListPromise = null;
  let bankByBin = null;

  async function loadBankMap() {
    if (bankByBin) {
      return bankByBin;
    }

    if (!bankListPromise) {
      bankListPromise = fetch("https://api.vietqr.io/v2/banks", { cache: "force-cache" })
        .then(response => {
          if (!response.ok) {
            throw new Error("Không tải được danh sách ngân hàng.");
          }

          return response.json();
        })
        .then(data => {
          const map = new Map();
          const banks = Array.isArray(data?.data) ? data.data : [];

          for (const bank of banks) {
            if (bank?.bin) {
              map.set(String(bank.bin), bank);
            }
          }

          bankByBin = map;
          return map;
        })
        .catch(() => {
          bankByBin = new Map();
          return bankByBin;
        });
    }

    return bankListPromise;
  }

  function normalizeImageSource(base64OrDataUrl) {
    if (!base64OrDataUrl) {
      return "";
    }

    const value = String(base64OrDataUrl).trim();
    if (value.startsWith("data:image/")) {
      return value;
    }

    return `data:image/png;base64,${value}`;
  }

  function parseTlv(data, options) {
    const level = options?.level ?? 0;
    const parentTag = options?.parentTag ?? null;
    const result = {};
    let index = 0;

    while (index + 4 <= data.length) {
      const tag = data.slice(index, index + 2);
      const length = parseInt(data.slice(index + 2, index + 4), 10);

      if (!Number.isFinite(length) || length < 0) {
        break;
      }

      const valueStart = index + 4;
      const valueEnd = valueStart + length;
      if (valueEnd > data.length) {
        break;
      }

      const value = data.slice(valueStart, valueEnd);
      if (shouldParseNested(tag, level, parentTag)) {
        result[tag] = parseTlv(value, {
          level: level + 1,
          parentTag: tag
        });
      } else {
        result[tag] = value;
      }
      index = valueEnd;
    }

    return result;
  }

  function shouldParseNested(tag, level, parentTag) {
    if (tag === "38" || tag === "62") {
      return true;
    }

    if (parentTag === "38" && tag === "01") {
      return true;
    }

    return false;
  }

  function findMerchantAccountInfo(root) {
    if (!root || typeof root !== "object") {
      return null;
    }

    if (root["38"] && typeof root["38"] === "object") {
      return root["38"];
    }

    for (const key of Object.keys(root)) {
      const value = root[key];
      if (value && typeof value === "object" && (value["01"] || value["02"])) {
        return value;
      }
    }

    return null;
  }

  async function decodeFromBase64Image(base64OrDataUrl) {
    try {
      if (typeof jsQR !== "function") {
        return {
          success: false,
          errorMessage: "Thư viện đọc QR chưa tải xong."
        };
      }

      const src = normalizeImageSource(base64OrDataUrl);
      if (!src) {
        return {
          success: false,
          errorMessage: "Không có dữ liệu ảnh QR."
        };
      }

      const image = await new Promise((resolve, reject) => {
        const img = new Image();
        img.onload = () => resolve(img);
        img.onerror = () => reject(new Error("Không tải được ảnh QR."));
        img.src = src;
      });

      const canvas = document.createElement("canvas");
      const context = canvas.getContext("2d", { willReadFrequently: true });
      if (!context) {
        return {
          success: false,
          errorMessage: "Không tạo được canvas để đọc QR."
        };
      }

      canvas.width = image.naturalWidth || image.width;
      canvas.height = image.naturalHeight || image.height;
      context.drawImage(image, 0, 0);

      const imageData = context.getImageData(0, 0, canvas.width, canvas.height);
      const code = jsQR(imageData.data, imageData.width, imageData.height, {
        inversionAttempts: "attemptBoth"
      });

      if (!code?.data) {
        return {
          success: false,
          errorMessage: "Không đọc được nội dung QR từ ảnh thanh toán."
        };
      }

      const payload = parseTlv(code.data);
      const merchantInfo = findMerchantAccountInfo(payload);
      const merchantBeneficiary = merchantInfo?.["01"];
      const bankBin = merchantBeneficiary && typeof merchantBeneficiary === "object"
        ? String(merchantBeneficiary["00"] || merchantBeneficiary["01"] || "")
        : "";
      const accountNumber = merchantBeneficiary && typeof merchantBeneficiary === "object"
        ? String(merchantBeneficiary["01"] || merchantBeneficiary["02"] || "")
        : "";
      const amountText = payload["54"] ? String(payload["54"]) : "";
      const amount = amountText ? Number(amountText) : null;
      const additionalData = payload["62"] && typeof payload["62"] === "object" ? payload["62"] : null;
      const transferContent = additionalData?.["08"] || additionalData?.["01"] || "";

      const bankMap = await loadBankMap();
      const bank = bankBin ? bankMap.get(String(bankBin)) : null;

      return {
        success: true,
        rawPayload: String(code.data),
        bankBin: String(bankBin || ""),
        bankName: String(bank?.shortName || bank?.name || bankBin || ""),
        accountNumber: String(accountNumber || ""),
        amount: Number.isFinite(amount) ? amount : null,
        transferContent: String(transferContent || "")
      };
    } catch (error) {
      return {
        success: false,
        errorMessage: error?.message || "Không thể giải mã QR thanh toán."
      };
    }
  }

  return {
    decodeFromBase64Image,
    copyText: async function (text) {
      if (!text) {
        return;
      }

      if (navigator.clipboard && navigator.clipboard.writeText) {
        await navigator.clipboard.writeText(String(text));
        return;
      }

      const textarea = document.createElement("textarea");
      textarea.value = String(text);
      textarea.setAttribute("readonly", "true");
      textarea.style.position = "fixed";
      textarea.style.opacity = "0";
      document.body.appendChild(textarea);
      textarea.select();
      document.execCommand("copy");
      document.body.removeChild(textarea);
    }
  };
})();
