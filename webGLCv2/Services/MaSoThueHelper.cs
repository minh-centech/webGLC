using System.Net;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace webGLCv2.Services
{
    public class MaSoThueHelper
    {
        private readonly HttpClient _httpClient;

        public MaSoThueHelper()
        {
            // Khởi tạo HttpClient với cơ chế tự động giải nén nếu có
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate };
            _httpClient = new HttpClient(handler);
            // Thêm User-Agent để tránh bị một số web chặn crawler
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
        }

        public async Task<string> ProcessTaxInfo(string masothue)
        {
            try
            {
                // BƯỚC 1: Gọi API VietQR để lấy tên công ty
                string companyName = await GetCompanyNameFromApi(masothue);
                if (string.IsNullOrEmpty(companyName)) return "Không tìm thấy tên công ty từ API.";

                // Chuyển tên thành không dấu và thay dấu cách bằng '-'
                string slugName = ConvertToUnaccentedSlug(companyName);
                string scrapeUrl = $"https://masothue.com/{masothue}-{slugName}";

                // BƯỚC 2: Cào dữ liệu từ masothue.com
                var taxData = await ScrapeTaxTable(scrapeUrl);

                return JsonConvert.SerializeObject(taxData, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        private async Task<string> GetCompanyNameFromApi(string mst, int retryCount = 0)
        {
            string apiUrl = $"https://api.vietqr.io/v2/business/{mst}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.StatusCode == (HttpStatusCode)429) // Too many requests
            {
                if (retryCount < 3) // Thử lại tối đa 3 lần
                {
                    Thread.Sleep(2000); // Đợi 2 giây trước khi thử lại
                    return await GetCompanyNameFromApi(mst, retryCount + 1);
                }
                throw new Exception("API VietQR báo lỗi: Too many requests sau nhiều lần thử.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            if (json["code"]?.ToString() == "00")
            {
                return json["data"]?["name"]?.ToString();
            }

            return null;
        }

        private async Task<Dictionary<string, string>> ScrapeTaxTable(string url)
        {
            var result = new Dictionary<string, string>();
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);
            var table = doc.DocumentNode.SelectSingleNode("//table[contains(@class, 'table-taxinfo')]");

            if (table != null)
            {
                // 1. Lấy tên công ty từ header
                var headerName = table.SelectSingleNode(".//thead//th")?.InnerText.Trim();
                if (!string.IsNullOrEmpty(headerName))
                    result.Add("ten_cong_ty", headerName);

                // 2. Duyệt các dòng trong tbody
                var rows = table.SelectNodes(".//tbody/tr");
                if (rows != null)
                {
                    foreach (var row in rows)
                    {
                        var cells = row.SelectNodes("td");
                        if (cells != null && cells.Count >= 2)
                        {
                            // Lấy text gốc của key (ví dụ: "Mã số thuế")
                            string rawKey = Regex.Replace(cells[0].InnerText, @"\t|\n|\r", "").Trim();

                            // Chuyển key thành không dấu, viết thường, thay cách bằng "_"
                            string cleanKey = ConvertToUnaccentedSlug(rawKey).Replace("-", "_");

                            // Xử lý lấy giá trị sạch (Value)
                            string value = cells[1].InnerText.Trim();
                            value = Regex.Replace(value, @"\s+", " ");

                            // Loại bỏ text rác của button nếu có
                            if (value.Contains("Ẩn số điện thoại"))
                                value = value.Replace("Ẩn số điện thoại", "").Trim();

                            // Chỉ add những trường có key hợp lệ (loại bỏ dòng "Cập nhật mã số thuế...")
                            if (!string.IsNullOrEmpty(cleanKey) && !result.ContainsKey(cleanKey) && !cleanKey.Contains("cap_nhat_ma_so_thue"))
                            {
                                result.Add(cleanKey, value);
                            }
                        }
                    }
                }
            }
            return result;
        }

        private string ConvertToUnaccentedSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            // Chuyển sang chữ thường
            text = text.ToLower().Trim();

            // Thay thế các ký tự có dấu
            string[] accents = { "aàảãáạăằẳẵắặâầẩẫấậ", "dđ", "eèẻẽéẹêềểễếệ", "iìỉĩíị", "oòỏõóọôồổỗốộơờởỡớợ", "uùủũúụưừửữứự", "yỳỷỹýỵ" };
            foreach (var group in accents)
            {
                char replacement = group[0];
                for (int i = 1; i < group.Length; i++)
                {
                    text = text.Replace(group[i], replacement);
                }
            }

            // Thay thế ký tự đặc biệt và dấu cách thành '-'
            text = Regex.Replace(text, @"[^a-z0-9\s]", ""); // Xóa ký tự đặc biệt
            text = Regex.Replace(text, @"\s+", "-");        // Thay dấu cách bằng -

            return text;
        }
    }
}
