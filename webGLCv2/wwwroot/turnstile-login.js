window.glcTurnstile = {
    renderLogin: function (elementId, siteKey) {
        const host = document.getElementById(elementId);
        if (!host || !siteKey) {
            return;
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
                "response-field-name": "cf-turnstile-response"
            });
        };

        render();
    }
};
