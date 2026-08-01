window.excelHelper = (function () {
    return {
        // Nhận byteArray trực tiếp từ Blazor
        downloadFileFromStream: function (fileName, byteArray, contentType) {
            // 1. Chuyển mảng byte từ C# thành Blob
            const blob = new Blob([new Uint8Array(byteArray)], {
                type: contentType || 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            });

            // 2. Tạo URL tải xuống
            const url = URL.createObjectURL(blob);
            const anchorElement = document.createElement('a');
            anchorElement.href = url;
            anchorElement.download = fileName || 'ExportExcel.xlsx';

            // 3. Trigger click tải file
            document.body.appendChild(anchorElement);
            anchorElement.click();
            anchorElement.remove();

            // 4. Giải phóng bộ nhớ
            URL.revokeObjectURL(url);
        }
    };
})();