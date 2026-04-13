window.addEventListener("load", () => {
    const toggleBtn = document.getElementById("chat-toggle-btn");
    const closeBtn = document.getElementById("chat-close-btn");
    const popup = document.getElementById("chat-popup");
    const chat = document.getElementById("deepchat");

    if (toggleBtn && popup) {
        toggleBtn.addEventListener("click", () => {
            popup.classList.toggle("open");
        });
    }

    if (closeBtn && popup) {
        closeBtn.addEventListener("click", () => {
            popup.classList.remove("open");
        });
    }

    const applyDeepChatStyle = () => {
        if (!chat || !chat.shadowRoot) return false;

        const sr = chat.shadowRoot;

        if (sr.getElementById("custom-deepchat-style")) return true;

        const style = document.createElement("style");
        style.id = "custom-deepchat-style";

        style.textContent = `
    #input {
        position: relative !important;
        display: flex !important;
        align-items: center !important;
        padding: 10px !important;
        background: #f5f5f5 !important;
        box-sizing: border-box !important;
    }

    #text-input-container {
        flex: 1 !important;
        position: relative !important;
        display: flex !important;
        align-items: center !important;
    }

    #text-input {
        width: 100% !important;
        min-height: 52px !important;
        padding: 12px 54px 12px 16px !important;
        font-size: 15px !important;
        line-height: 1.4 !important;
        /*border: 1px solid #ddd !important;*/
         /*border-radius: 24px !important;*/
        background: #fff !important;
        box-sizing: border-box !important;
        display: flex !important;
        align-items: center !important;
    }

    /* khung ngoài đã đúng, chỉ giữ center */
    .input-button-container.inner-button-container {
        position: absolute !important;
        right: 14px !important;
        top: 50% !important;
        transform: translateY(-50%) !important;
        width: 34px !important;
        height: 34px !important;
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
        margin: 0 !important;
        padding: 0 !important;
        z-index: 3 !important;
    }

    /* sửa đúng nút con bị lệch */
    .input-button.inside-end.submit-button.input-button-svg,
    .input-button.inside-end.input-button-svg {
        position: static !important;
        transform: none !important;
        inset: auto !important;
        width: 34px !important;
        height: 34px !important;
        min-width: 34px !important;
        min-height: 34px !important;
        margin: 0 !important;
        padding: 0 !important;
        border: none !important;
        border-radius: 50% !important;
        background: #0b6b36 !important;
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
        box-sizing: border-box !important;
        align-self: center !important;
        justify-self: center !important;
    }

    .input-button.inside-end.disabled-button.input-button-svg {
        opacity: 0.55 !important;
    }

    #submit-icon {
        width: 16px !important;
        height: 16px !important;
        display: block !important;
        margin: 0 !important;
        padding: 0 !important;
        filter: brightness(0) saturate(100%) invert(100%) !important;
    }

    .input-button-container.outer-button-container {
        display: none !important;
        width: 0 !important;
    }
`;

        sr.appendChild(style);
        return true;
    };

    const timer = setInterval(() => {
        if (applyDeepChatStyle()) {
            clearInterval(timer);
        }
    }, 300);

    applyDeepChatStyle();

    setTimeout(() => clearInterval(timer), 10000);
});
