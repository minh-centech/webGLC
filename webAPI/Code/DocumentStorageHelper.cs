using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace webAPI.Code
{
    public static class DocumentStorageHelper
    {
        private const string DocumentStorageRootKey = "DocumentStorageRoot";

        public static string GetStorageRootPath()
        {
            string configuredPath = ConfigurationManager.AppSettings[DocumentStorageRootKey];
            string resolvedPath;

            if (string.IsNullOrWhiteSpace(configuredPath))
            {
                resolvedPath = HostingEnvironment.MapPath("~/App_Data/UploadedFiles");
            }
            else if (Path.IsPathRooted(configuredPath))
            {
                resolvedPath = configuredPath;
            }
            else
            {
                string appRelativePath = configuredPath.StartsWith("~/", StringComparison.Ordinal)
                    ? configuredPath
                    : "~/" + configuredPath.TrimStart('/', '\\');

                resolvedPath = HostingEnvironment.MapPath(appRelativePath);
            }

            if (string.IsNullOrWhiteSpace(resolvedPath))
            {
                throw new InvalidOperationException("Không xác định được đường dẫn lưu tài liệu từ cấu hình DocumentStorageRoot.");
            }

            return Path.GetFullPath(resolvedPath);
        }

        public static string SanitizeRelativePath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                throw new ArgumentException("Đường dẫn file không hợp lệ.");
            }

            string normalizedPath = relativePath.Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            string[] segments = normalizedPath.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length == 0 || segments.Any(segment => segment == "." || segment == ".."))
            {
                throw new ArgumentException("Đường dẫn file không hợp lệ.");
            }

            foreach (string segment in segments)
            {
                if (segment.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    throw new ArgumentException("Đường dẫn file chứa ký tự không hợp lệ.");
                }
            }

            return string.Join(Path.DirectorySeparatorChar.ToString(), segments);
        }

        public static string ResolveAbsolutePath(string relativePath)
        {
            string rootPath = GetStorageRootPath();
            string safeRelativePath = SanitizeRelativePath(relativePath);
            string absolutePath = Path.GetFullPath(Path.Combine(rootPath, safeRelativePath));

            if (!absolutePath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Đường dẫn file vượt ra ngoài thư mục cho phép.");
            }

            return absolutePath;
        }
    }
}
