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
        if (tokenInput) {
            tokenInput.value = "";
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
                    if (tokenInput) {
                        tokenInput.value = token || "";
                    }
                },
                "expired-callback": function () {
                    if (tokenInput) {
                        tokenInput.value = "";
                    }
                },
                "error-callback": function () {
                    if (tokenInput) {
                        tokenInput.value = "";
                    }
                }
            });
        };

        render();
    }
};
