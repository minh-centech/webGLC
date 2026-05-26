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

  function escapeHtml(value) {
    return String(value ?? "")
      .replace(/&/g, "&amp;")
      .replace(/</g, "&lt;")
      .replace(/>/g, "&gt;")
      .replace(/"/g, "&quot;")
      .replace(/'/g, "&#39;");
  }

  function formatVndAmount(value) {
    if (value === null || value === undefined || value === "") {
      return "";
    }

    const numericValue = typeof value === "number"
      ? value
      : Number(String(value).replace(/[^\d-]/g, ""));

    if (!Number.isFinite(numericValue)) {
      return String(value);
    }

    return new Intl.NumberFormat("vi-VN", {
      maximumFractionDigits: 0
    }).format(numericValue);
  }

  function downloadQrImage(src, receiptNo) {
    if (!src) {
      return;
    }

    const anchor = document.createElement("a");
    anchor.href = src;
    anchor.download = `qr-${String(receiptNo || "thanh-toan").trim() || "thanh-toan"}.png`;
    anchor.style.display = "none";
    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();
  }

  function downloadFile(fileName, contentType, base64Content) {
    if (!base64Content) {
      return;
    }

    const binary = atob(String(base64Content));
    const bytes = new Uint8Array(binary.length);
    for (let i = 0; i < binary.length; i++) {
      bytes[i] = binary.charCodeAt(i);
    }

    const blob = new Blob([bytes], { type: contentType || "application/pdf" });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = fileName || "download.pdf";
    anchor.style.display = "none";
    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();
    URL.revokeObjectURL(url);
  }

  function hideReceiptPaymentPopup(notifyBlazor = false, dotNetRef = null) {
    const existing = document.getElementById("receipt-payment-popup-overlay");
    if (existing) {
      existing.remove();
    }

    if (notifyBlazor && dotNetRef && typeof dotNetRef.invokeMethodAsync === "function") {
      dotNetRef.invokeMethodAsync("OnReceiptPaymentPopupClosedFromJs").catch(() => {});
    }
  }

  async function showReceiptPaymentPopup(options) {
    hideReceiptPaymentPopup();

    const base64 = options?.base64 || "";
    const receiptNo = options?.receiptNo || "";
    const amountText = options?.amountText || "";
    const dotNetRef = options?.dotNetRef || null;
    const src = normalizeImageSource(base64);
    if (!src) {
      return;
    }

    const overlay = document.createElement("div");
    overlay.id = "receipt-payment-popup-overlay";
    overlay.style.cssText = "position:fixed; inset:0; z-index:9999999; display:flex; align-items:center; justify-content:center; padding:24px; background:rgba(10, 16, 28, 0.72); backdrop-filter:blur(8px);";

    const modal = document.createElement("div");
    modal.style.cssText = "width:min(920px, 100%); max-height:calc(100vh - 48px); overflow:auto; border-radius:24px; background:#fff; box-shadow:0 30px 80px rgba(15, 23, 42, 0.28); border:1px solid rgba(15, 23, 42, 0.08);";
    modal.addEventListener("click", event => event.stopPropagation());

    const header = document.createElement("div");
    header.style.cssText = "display:flex; align-items:flex-start; justify-content:space-between; gap:16px; padding:24px 24px 0;";
    header.innerHTML = `
      <div>
        <p class="page-kicker">Biên nhận thanh toán</p>
        <h3>Thông tin chuyển khoản QR</h3>
      </div>
    `;

    const closeButton = document.createElement("button");
    closeButton.type = "button";
    closeButton.className = "payment-modal-close";
    closeButton.textContent = "×";
    closeButton.addEventListener("click", () => hideReceiptPaymentPopup(true, dotNetRef));

    header.appendChild(closeButton);

    const body = document.createElement("div");
    body.style.cssText = "padding:24px;";

    const topGrid = document.createElement("div");
    topGrid.className = "field-grid";
    topGrid.innerHTML = `
      <div>
        <label class="portal-label">Số biên nhận</label>
        <input class="portal-input" value="${escapeHtml(receiptNo)}" readonly />
      </div>
      <div>
        <label class="portal-label">Tổng tiền</label>
        <input class="portal-input" value="${escapeHtml(amountText)}" readonly />
      </div>
    `;
    body.appendChild(topGrid);

    const grid = document.createElement("div");
    grid.className = "payment-modal-grid";
    grid.style.marginTop = "16px";

    const qrCard = document.createElement("div");
    qrCard.className = "payment-qr-card";
    qrCard.innerHTML = `
      <img class="payment-qr-image" src="${src}" alt="QR thanh toán biên nhận" />
      <p class="payment-qr-caption">Mở app ngân hàng và quét mã QR này để chuyển khoản.</p>
      <button type="button" class="portal-secondary-action" style="align-self:flex-start; padding:0.65rem 1rem; margin-top:8px;" aria-label="Tải xuống ảnh QR" title="Tải xuống ảnh QR">
        Tải xuống ảnh QR
      </button>
    `;

    const details = document.createElement("div");
    details.className = "payment-details";
    details.innerHTML = '<div class="portal-empty-inline">Đang phân tích mã QR thanh toán...</div>';

    const downloadButton = qrCard.querySelector("button");
    if (downloadButton) {
      downloadButton.addEventListener("click", event => {
        event.stopPropagation();
        downloadQrImage(src, receiptNo);
      });
    }

    grid.appendChild(qrCard);
    grid.appendChild(details);
    body.appendChild(grid);

    const footer = document.createElement("div");
    footer.style.cssText = "display:flex; justify-content:flex-end; padding:0 24px 24px; gap:12px;";
    const footerClose = document.createElement("button");
    footerClose.type = "button";
    footerClose.className = "portal-secondary-action";
    footerClose.style.padding = "0.85rem 1.2rem";
    footerClose.textContent = "Đóng";
    footerClose.addEventListener("click", () => hideReceiptPaymentPopup(true, dotNetRef));
    footer.appendChild(footerClose);

    modal.appendChild(header);
    modal.appendChild(body);
    modal.appendChild(footer);
    overlay.appendChild(modal);
    overlay.addEventListener("click", () => hideReceiptPaymentPopup(true, dotNetRef));
    document.body.appendChild(overlay);

    const decoded = await decodeFromBase64Image(base64);
    if (!document.body.contains(overlay)) {
      return;
    }

    if (!decoded || !decoded.success) {
      const message = decoded?.errorMessage || "Không thể đọc thông tin từ mã QR thanh toán.";
      details.innerHTML = `<div class="portal-alert portal-alert-warning" style="margin-top:8px;">${escapeHtml(message)}</div>`;
      return;
    }

    const rows = [];
    const bankLabel = decoded.bankName || decoded.bankBin || "";
    rows.push({ label: "Ngân hàng", value: bankLabel });
    rows.push({ label: "Số tài khoản", value: decoded.accountNumber || "" });
    rows.push({ label: "Số tiền", value: formatVndAmount(decoded.amount ?? amountText) || amountText || "" });
    if (decoded.transferContent) {
      rows.push({ label: "Nội dung", value: decoded.transferContent });
    }

    details.innerHTML = rows.map(row => `
      <div class="payment-detail-row">
        <span>${escapeHtml(row.label)}</span>
        <strong>${escapeHtml(row.value)}</strong>
      </div>
    `).join("");

    details.innerHTML += `
        <div class="portal-alert portal-alert-warning" style="margin-top:12px;">
            Vui lòng giữ nguyên mã giao dịch trong nội dung chuyển khoản. Nếu sai mã, hệ thống sẽ không thể xác thực giao dịch của bạn.
        </div>
    `;
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
    showReceiptPaymentPopup,
    hideReceiptPaymentPopup,
    downloadFile,
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
