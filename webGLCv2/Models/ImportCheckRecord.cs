namespace webGLCv2.Models;

public sealed class ImportCheckRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime ReceivedDate { get; set; }
    public string MasterBill { get; set; } = string.Empty;
    public string HouseBill { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Volume { get; set; }
    public decimal Weight { get; set; }
    public string ContainerNumber { get; set; } = string.Empty;
    public string Forwarder { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CustomsDeclaration { get; set; } = string.Empty;
}
