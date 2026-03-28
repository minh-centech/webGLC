namespace webAPI.Models
{
    public class UploadPdfResult
    {
        public string OriginalFileName { get; set; }
        public string SavedFileName { get; set; }
        public string RelativePath { get; set; }
        public string ViewUrl { get; set; }
    }
}
