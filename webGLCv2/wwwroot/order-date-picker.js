window.orderDatePicker = {
  initPickupDatePicker: function (elementId) {
    if (!window.flatpickr) {
      return;
    }

    const input = document.getElementById(elementId);
    if (!input) {
      return;
    }

    if (input._flatpickr) {
      input._flatpickr.destroy();
    }

    const locale = window.flatpickr.l10ns.vn || window.flatpickr.l10ns.default;

    window.flatpickr(input, {
      locale,
      dateFormat: "d/m/Y",
      allowInput: true,
      disableMobile: true,
      onChange: function () {
        input.dispatchEvent(new Event("input", { bubbles: true }));
        input.dispatchEvent(new Event("change", { bubbles: true }));
      },
      onValueUpdate: function () {
        input.dispatchEvent(new Event("input", { bubbles: true }));
      }
    });
  }
};