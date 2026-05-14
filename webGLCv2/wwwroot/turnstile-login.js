window.glcTurnstile = {
    renderLogin: function (elementId, siteKey) {
        window.glcTurnstile.render(elementId, siteKey, "login-turnstile-token");
    },
    renderRegister: function (elementId, siteKey) {
        window.glcTurnstile.render(elementId, siteKey, "register-turnstile-token");
    },
    render: function (elementId, siteKey, tokenInputId) {
        const host = document.getElementById(elementId);
        if (!host || !siteKey) {
            return;
        }

        const tokenInput = document.getElementById(tokenInputId);
        const syncToken = (value) => {
            if (!tokenInput) {
                return;
            }

            tokenInput.value = value || "";
            tokenInput.dispatchEvent(new Event("input", { bubbles: true }));
        };

        if (tokenInput) {
            syncToken("");
        }

        const render = () => {
            if (!window.turnstile) {
                window.setTimeout(render, 150);
                return;
            }

            host.innerHTML = "";
            window.turnstile.render(host, {
                sitekey: siteKey,
                theme: "light",
                language: "auto",
                callback: function (token) {
                    syncToken(token);
                },
                "expired-callback": function () {
                    syncToken("");
                },
                "error-callback": function () {
                    syncToken("");
                }
            });
        };

        render();
    }
};
