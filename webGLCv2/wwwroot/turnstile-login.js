window.glcTurnstile = {
    renderLogin: function (elementId, siteKey) {
        const host = document.getElementById(elementId);
        if (!host || !siteKey) {
            return;
        }

        const tokenInput = document.getElementById("login-turnstile-token");
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
