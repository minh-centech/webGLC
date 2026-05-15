window.glcTurnstile = (() => {
    const widgetIds = new Map();

    const syncToken = (tokenInput, value) => {
        if (!tokenInput) {
            return;
        }

        tokenInput.value = value || "";
        tokenInput.dispatchEvent(new Event("input", { bubbles: true }));
    };

    const getFreshHost = (elementId) => {
        const currentHost = document.getElementById(elementId);
        if (!currentHost) {
            return null;
        }

        const freshHost = currentHost.cloneNode(false);
        freshHost.innerHTML = "";
        currentHost.replaceWith(freshHost);
        return freshHost;
    };

    const destroyExistingWidget = (elementId) => {
        if (!window.turnstile || !widgetIds.has(elementId)) {
            return;
        }

        const widgetId = widgetIds.get(elementId);
        widgetIds.delete(elementId);

        try {
            if (typeof window.turnstile.remove === "function") {
                window.turnstile.remove(widgetId);
                return;
            }
        }
        catch {
            // Ignore and continue with best-effort cleanup.
        }

        try {
            if (typeof window.turnstile.reset === "function") {
                window.turnstile.reset(widgetId);
            }
        }
        catch {
            // Ignore and continue with hard re-render.
        }
    };

    const renderInternal = (elementId, siteKey, tokenInputId) => {
        const tokenInput = document.getElementById(tokenInputId);
        syncToken(tokenInput, "");

        const freshHost = getFreshHost(elementId);
        if (!freshHost || !siteKey) {
            return;
        }

        const attemptRender = () => {
            if (!window.turnstile) {
                window.setTimeout(attemptRender, 100);
                return;
            }

            destroyExistingWidget(elementId);
            freshHost.innerHTML = "";

            try {
                const widgetId = window.turnstile.render(freshHost, {
                    sitekey: siteKey,
                    theme: "light",
                    language: "auto",
                    callback: function (token) {
                        syncToken(tokenInput, token);
                    },
                    "expired-callback": function () {
                        syncToken(tokenInput, "");
                    },
                    "error-callback": function () {
                        syncToken(tokenInput, "");
                    }
                });

                if (widgetId !== undefined && widgetId !== null) {
                    widgetIds.set(elementId, widgetId);
                }
            }
            catch {
                window.setTimeout(attemptRender, 200);
            }
        };

        window.requestAnimationFrame(() => attemptRender());
    };

    return {
        renderLogin: function (elementId, siteKey) {
            renderInternal(elementId, siteKey, "login-turnstile-token");
        },
        forceRenderLogin: function (elementId, siteKey) {
            renderInternal(elementId, siteKey, "login-turnstile-token");
        },
        renderRegister: function (elementId, siteKey) {
            renderInternal(elementId, siteKey, "register-turnstile-token");
        },
        forceRenderRegister: function (elementId, siteKey) {
            renderInternal(elementId, siteKey, "register-turnstile-token");
        },
        render: function (elementId, siteKey, tokenInputId) {
            renderInternal(elementId, siteKey, tokenInputId);
        }
    };
})();
