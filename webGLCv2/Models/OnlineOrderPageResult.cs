namespace webGLCv2.Models;

public sealed class OnlineOrderPageResult
{
    public List<OnlineOrderRecord> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
