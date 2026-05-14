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

        /// <summary>
        /// response 
        /// {
        ///  "ten_cong_ty": "CÔNG TY CỔ PHẦN EVERLINK",
        /// "ma_so_thue": "0202097428",
        /// "dia_chi_thue": "Số 15, Tầng 2, Tòa nhà Đình Vũ Plaza Km 108, bên trái tuyến, Phường Đông Hải, TP Hải Phòng, Việt Nam",
        ///  "dia_chi": "Số 15, Tầng 2, Tòa nhà Đình Vũ Plaza Km 108, bên trái tuyến của đường quốc lộ 5 kéo dài từ Đình Vũ về Hà Nội, Phường Đông Hải 2, Quận Hải An, Thành phố Hải Phòng, Việt Nam",
        ///  "tinh_trang": "Đang hoạt động",
        ///  "ten_quoc_te": "EVERLINK JOINT STOCK COMPANY",
        /// "ten_viet_tat": "EVERLINK JSC",
        /// "nguoi_dai_dien": "NGUYỄN QUANG MINH",
        /// "dien_thoai": "0904026669",
        /// "ngay_hoat_dong": "2021-04-07",
        /// "quan_ly_boi": "Thuế cơ sở 1 thành phố Hải Phòng",
        ///   "loai_hinh_dn": "Công ty cổ phần ngoài NN",
        ///  "nganh_nghe_chinh": "Lập trình máy vi tính"
        /// }
        /// Cách gọi 
        /// private List<KeyValuePair<string, string>> TaxLookupResult { get; set; } = new();
        ///  var helperResult = await TaxHelper.ProcessTaxInfo(taxCode);
        ///  using var doc = JsonDocument.Parse(helperResult);
        ///   var root = doc.RootElement;
        /// foreach (var prop in root.EnumerateObject())
        ///  {
        ///      TaxLookupResult.Add(new KeyValuePair<string, string>(NormalizeTaxLookupLabel(prop.Name), prop.Value.ToString()));
        /// }
        /// </summary>
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

                Console.WriteLine($"Du lieu lay ten cong ty scrapeUrl: {scrapeUrl}");

                // BƯỚC 2: Cào dữ liệu từ masothue.com
                var taxData = await ScrapeTaxTable(scrapeUrl);
                string logString = string.Join("; ", taxData.Select(x => $"{x.Key}: {x.Value}"));

                Console.WriteLine($"Du lieu lay tu masothue: {logString} ");

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

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                // Nếu khác 200 thì retry
                if (!response.IsSuccessStatusCode)
                {
                    if (retryCount < 3)
                    {
                        await Task.Delay(700); // nghỉ 700ms rồi thử lại
                        return await GetCompanyNameFromApi(mst, retryCount + 1);
                    }

                    throw new Exception($"API VietQR lỗi: {(int)response.StatusCode} - {response.ReasonPhrase}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                if (json["code"]?.ToString() == "00")
                {
                    return json["data"]?["name"]?.ToString();
                }

                return null;
            }
            catch (Exception ex)
            {
                // Retry nếu lỗi mạng hoặc exception khác
                if (retryCount < 3)
                {
                    await Task.Delay(700);
                    return await GetCompanyNameFromApi(mst, retryCount + 1);
                }

                throw new Exception("Lỗi gọi API VietQR", ex);
            }
        }

        private async Task<Dictionary<string, string>> ScrapeTaxTable(string url)
        {
            var result = new Dictionary<string, string>();

            try
            {
                var web = new HtmlWeb();

                // Giả lập trình duyệt thật
                web.PreRequest = (request) =>
                {
                    request.UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                        "AppleWebKit/537.36 (KHTML, like Gecko) " +
                        "Chrome/136.0.0.0 Safari/537.36";

                    request.Accept =
                        "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8";

                    request.Headers.Add("Accept-Language", "vi-VN,vi;q=0.9,en-US;q=0.8,en;q=0.7");
                    request.Headers.Add("Cache-Control", "no-cache");
                    request.Headers.Add("Pragma", "no-cache");
                    request.Headers.Add("Upgrade-Insecure-Requests", "1");

                    request.AutomaticDecompression =
                        DecompressionMethods.GZip |
                        DecompressionMethods.Deflate;

                    request.Timeout = 15000;

                    return true;
                };

                var doc = await web.LoadFromWebAsync(url);

                var table = doc.DocumentNode
                    .SelectSingleNode("//table[contains(@class, 'table-taxinfo')]");

                if (table != null)
                {
                    // 1. Lấy tên công ty từ header
                    var headerName = table
                        .SelectSingleNode(".//thead//th")
                        ?.InnerText
                        .Trim();

                    if (!string.IsNullOrWhiteSpace(headerName))
                    {
                        result["ten_cong_ty"] = HtmlEntity.DeEntitize(headerName);
                    }

                    // 2. Duyệt tbody
                    var rows = table.SelectNodes(".//tbody/tr");

                    if (rows != null)
                    {
                        foreach (var row in rows)
                        {
                            var cells = row.SelectNodes("td");

                            if (cells != null && cells.Count >= 2)
                            {
                                // KEY
                                string rawKey = HtmlEntity.DeEntitize(cells[0].InnerText);

                                rawKey = Regex.Replace(rawKey, @"\t|\n|\r", "")
                                    .Trim();

                                string cleanKey = ConvertToUnaccentedSlug(rawKey)
                                    .Replace("-", "_");

                                string value = "";

                                // Nếu là người đại diện -> chỉ lấy tên trong thẻ <a>
                                if (cleanKey == "nguoi_dai_dien")
                                {
                                    var aNode = cells[1]
                                        .SelectSingleNode(".//span[@itemprop='name']/a");

                                    if (aNode != null)
                                    {
                                        value = HtmlEntity.DeEntitize(aNode.InnerText)
                                            .Trim();
                                    }
                                }
                                else
                                {
                                    // VALUE bình thường
                                    value = HtmlEntity.DeEntitize(cells[1].InnerText);

                                    value = Regex.Replace(value, @"\s+", " ")
                                        .Trim();

                                    // Loại text rác
                                    value = value.Replace("Ẩn số điện thoại", "")
                                                 .Trim();
                                }

                                // Bỏ dòng rác
                                if (!string.IsNullOrWhiteSpace(cleanKey)
                                    && !result.ContainsKey(cleanKey)
                                    && !cleanKey.Contains("cap_nhat_ma_so_thue"))
                                {
                                    result.Add(cleanKey, value);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Scrape lỗi: {ex.Message}");
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
