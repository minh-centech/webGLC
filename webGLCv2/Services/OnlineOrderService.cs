using System.Collections.Concurrent;
using webGLCv2.Models;

namespace webGLCv2.Services;

public sealed class OnlineOrderService
{
    private readonly ConcurrentDictionary<string, List<OnlineOrderRecord>> _ordersByOwner = new(StringComparer.OrdinalIgnoreCase);
    private readonly List<ImportCheckRecord> _importChecks = CreateImportCheckSeedData();

    public Task<List<OnlineOrderRecord>> GetOrdersAsync(string owner)
    {
        var normalizedOwner = NormalizeOwner(owner);
        var orders = _ordersByOwner.GetOrAdd(normalizedOwner, static _ => CreateSeedData());
        return Task.FromResult(orders.OrderByDescending(order => order.OrderDate).ToList());
    }

    public Task SeedDefaultsAsync(string owner)
    {
        _ordersByOwner[NormalizeOwner(owner)] = CreateSeedData();
        return Task.CompletedTask;
    }

    public Task AddOrderAsync(string owner, OnlineOrderDraft draft)
    {
        var normalizedOwner = NormalizeOwner(owner);
        var orders = _ordersByOwner.GetOrAdd(normalizedOwner, static _ => CreateSeedData());

        orders.Insert(0, new OnlineOrderRecord
        {
            OrderCode = $"LO-{DateTime.UtcNow:yyMMddHHmmss}",
            OrderDate = draft.OrderDate,
            CustomerName = draft.CustomerName.Trim(),
            PhoneNumber = draft.PhoneNumber.Trim(),
            IdentityNumber = draft.IdentityNumber.Trim(),
            VehicleNumber = draft.VehicleNumber.Trim(),
            TaxCode = draft.TaxCode.Trim(),
            CompanyName = draft.CompanyName.Trim(),
            CompanyAddress = draft.CompanyAddress.Trim(),
            CompanyEmail = draft.CompanyEmail.Trim(),
            HouseBill = draft.HouseBill.Trim(),
            ContainerNumber = draft.ContainerNumber.Trim(),
            PickupDate = draft.PickupDate,
            DeclarationNumber = draft.DeclarationNumber.Trim(),
            Status = "Chờ xử lý"
        });

        return Task.CompletedTask;
    }

    public Task<(bool Success, string Message, string CompanyName, string CompanyAddress, string CompanyEmail)> LookupCompanyAsync(
        string taxCode,
        string currentEmail)
    {
        var normalized = string.IsNullOrWhiteSpace(taxCode) ? string.Empty : taxCode.Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            return Task.FromResult((false, "Vui lòng nhập mã số thuế trước khi lấy thông tin.", string.Empty, string.Empty, string.Empty));
        }

        var samples = new Dictionary<string, (string Name, string Address, string Email)>(StringComparer.OrdinalIgnoreCase)
        {
            ["0201930936"] = ("CÔNG TY CỔ PHẦN ĐẦU TƯ CÔNG NGHỆ CENTECH", "Hải An, Hải Phòng", "info@centech.vn"),
            ["0301464823"] = ("CÔNG TY TNHH THƯƠNG MẠI EVERLINK", "Quận 1, TP. Hồ Chí Minh", "admin@everlink.com.vn")
        };

        if (samples.TryGetValue(normalized, out var match))
        {
            return Task.FromResult((true, "Đã lấy thông tin công ty theo mã số thuế.", match.Name, match.Address, match.Email));
        }

        return Task.FromResult((
            true,
            "Chưa có dữ liệu đồng bộ tự động. Hệ thống đã điền mẫu tham khảo, bạn có thể chỉnh lại trước khi lưu.",
            $"Công ty theo MST {normalized}",
            "Địa chỉ công ty cần được cập nhật",
            string.IsNullOrWhiteSpace(currentEmail) ? "contact@company.vn" : currentEmail.Trim()));
    }

    public Task<List<ImportCheckRecord>> SearchImportChecksAsync(string? houseBill, string? containerNumber)
    {
        var normalizedHouseBill = string.IsNullOrWhiteSpace(houseBill) ? string.Empty : houseBill.Trim();
        var normalizedContainer = string.IsNullOrWhiteSpace(containerNumber) ? string.Empty : containerNumber.Trim();

        var query = _importChecks.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(normalizedHouseBill))
        {
            query = query.Where(item => item.HouseBill.Contains(normalizedHouseBill, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(normalizedContainer))
        {
            query = query.Where(item => item.ContainerNumber.Contains(normalizedContainer, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(query.OrderByDescending(item => item.ReceivedDate).ToList());
    }

    private static string NormalizeOwner(string owner)
        => string.IsNullOrWhiteSpace(owner) ? "anonymous" : owner.Trim().ToLowerInvariant();

    private static List<OnlineOrderRecord> CreateSeedData()
    {
        return
        [
            new OnlineOrderRecord
            {
                OrderCode = "LO-260401-001",
                OrderDate = DateTime.Today,
                CustomerName = "Nguyễn Văn A",
                PhoneNumber = "0909123456",
                IdentityNumber = "001203456789",
                VehicleNumber = "51D-123.45",
                TaxCode = "0201930936",
                CompanyName = "CÔNG TY CỔ PHẦN ĐẦU TƯ CÔNG NGHỆ CENTECH",
                CompanyAddress = "Hải An, Hải Phòng",
                CompanyEmail = "info@centech.vn",
                HouseBill = "HB-000009",
                ContainerNumber = "TCLU1234567",
                PickupDate = DateTime.Today.AddDays(1),
                DeclarationNumber = "TK-99887766",
                Status = "Đã xác nhận"
            },
            new OnlineOrderRecord
            {
                OrderCode = "LO-260329-004",
                OrderDate = DateTime.Today.AddDays(-3),
                CustomerName = "Trần Thị B",
                PhoneNumber = "0911222333",
                IdentityNumber = "079203456789",
                VehicleNumber = "61C-888.99",
                TaxCode = "0301464823",
                CompanyName = "CÔNG TY TNHH THƯƠNG MẠI EVERLINK",
                CompanyAddress = "Quận 1, TP. Hồ Chí Minh",
                CompanyEmail = "admin@everlink.com.vn",
                HouseBill = "HB-000008",
                ContainerNumber = "OOLU7654321",
                PickupDate = DateTime.Today,
                DeclarationNumber = "TK-11223344",
                Status = "Chờ xử lý"
            }
        ];
    }

    private static List<ImportCheckRecord> CreateImportCheckSeedData()
    {
        return
        [
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today,
                MasterBill = "MBL-260401-01",
                HouseBill = "HB-000009",
                Quantity = 120,
                Volume = 18.5m,
                Weight = 10000m,
                ContainerNumber = "TCLU1234567",
                Forwarder = "Everlink Logistics",
                Status = "Đã nhận hàng",
                CustomsDeclaration = "TK-99887766"
            },
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today.AddDays(-1),
                MasterBill = "MBL-260331-03",
                HouseBill = "HB-000008",
                Quantity = 80,
                Volume = 12.2m,
                Weight = 7200m,
                ContainerNumber = "OOLU7654321",
                Forwarder = "Centech Forwarding",
                Status = "Chờ thông quan",
                CustomsDeclaration = "TK-11223344"
            },
            new ImportCheckRecord
            {
                ReceivedDate = DateTime.Today.AddDays(-3),
                MasterBill = "MBL-260329-07",
                HouseBill = "HB-000007",
                Quantity = 55,
                Volume = 9.6m,
                Weight = 4300m,
                ContainerNumber = "SEGU5566778",
                Forwarder = "Everlink Logistics",
                Status = "Đã thông quan",
                CustomsDeclaration = "TK-44332211"
            }
        ];
    }
}
