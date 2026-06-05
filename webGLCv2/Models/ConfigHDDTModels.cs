namespace webGLCv2.Models;

public sealed class ConfigHDDTListItem
{
    public long ID { get; set; }
    public int? Nam { get; set; }
    public string URLHDDT { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string ACPass { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Pass { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;
    public long? IDDanhMucNguoiSuDungCreate { get; set; }
    public long? IDDanhMucNguoiSuDungEdit { get; set; }
    public long? IDDanhMucDonVi { get; set; }
    public long? IDDanhMucLoaiDoiTuong { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? EditDate { get; set; }
    public bool? NgungSuDung { get; set; }
    public string? GhiChu { get; set; }
}

public sealed class ConfigHDDTEditModel
{
    public long? ID { get; set; }
    public int? Nam { get; set; } = DateTime.Now.Year;
    public string URLHDDT { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public string ACPass { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Pass { get; set; } = string.Empty;
    public string Pattern { get; set; } = string.Empty;
    public string Serial { get; set; } = string.Empty;
    public long? IDDanhMucDonVi { get; set; } = 1;
    public long? IDDanhMucLoaiDoiTuong { get; set; }
}
