using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using coreDTO;
using webAPI.Code;
using webAPI.Models;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaiLieuController : ApiController
    {
        [HttpPost]
        public webAPIresponse UploadPdf(string folder = "NguoiDungDoanhNghiep")
        {
            webAPIresponse response = new webAPIresponse();

            try
            {
                HttpRequest request = HttpContext.Current?.Request;
                if (request == null || request.Files.Count == 0)
                {
                    throw new Exception("Không tìm thấy file upload.");
                }

                HttpPostedFile file = request.Files[0];
                if (file == null || file.ContentLength <= 0)
                {
                    throw new Exception("File upload không hợp lệ.");
                }

                string originalFileName = Path.GetFileName(file.FileName);
                if (!string.Equals(Path.GetExtension(originalFileName), ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Chỉ hỗ trợ upload file PDF.");
                }

                string safeFolder = DocumentStorageHelper.SanitizeRelativePath(folder);
                string storageRoot = DocumentStorageHelper.GetStorageRootPath();
                string targetFolder = Path.Combine(storageRoot, safeFolder);
                Directory.CreateDirectory(targetFolder);

                string savedFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid():N}.pdf";
                string absolutePath = Path.Combine(targetFolder, savedFileName);
                file.SaveAs(absolutePath);

                string relativePath = Path.Combine(safeFolder, savedFileName).Replace("\\", "/");
                string baseUrl = Request?.RequestUri?.GetLeftPart(UriPartial.Authority) ?? string.Empty;

                UploadPdfResult result = new UploadPdfResult
                {
                    OriginalFileName = originalFileName,
                    SavedFileName = savedFileName,
                    RelativePath = relativePath,
                    ViewUrl = $"{baseUrl}/api/TaiLieu/ViewPdf?path={Uri.EscapeDataString(relativePath)}"
                };

                response.Status = 0;
                response.Data = JsonConvert.SerializeObject(result);
                response.ErrorMsg = string.Empty;
            }
            catch (Exception ex)
            {
                response.Status = 1;
                response.Data = string.Empty;
                response.ErrorMsg = ex.Message;
            }

            return response;
        }

        [HttpGet]
        public HttpResponseMessage ViewPdf(string path)
        {
            try
            {
                string absolutePath = DocumentStorageHelper.ResolveAbsolutePath(path);
                if (!File.Exists(absolutePath))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Không tìm thấy file PDF.");
                }

                FileStream stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
                {
                    FileName = Path.GetFileName(absolutePath)
                };

                return response;
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
