using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class FileUploadResult
    {
        public bool Success { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();

        // File metadata
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string FileExtension { get; set; }
        public string FileHash { get; set; }

        public bool IsImage => ContentType?.StartsWith("image/") == true;
        public string FileSizeFormatted => FormatFileSize(FileSize);

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        public static FileUploadResult Failure(string errorMessage)
        {
            return new FileUploadResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }

        public static FileUploadResult ValidationFailure(List<string> validationErrors)
        {
            return new FileUploadResult
            {
                Success = false,
                ErrorMessage = "File validation failed",
                ValidationErrors = validationErrors
            };
        }
        public static FileUploadResult CreateSuccess(string fileName, string filePath, long fileSize, string contentType)
        {
            return new FileUploadResult
            {
                Success = true,
                FileName = fileName,
                FilePath = filePath,
                FileSize = fileSize,
                ContentType = contentType
            };
        }
    }
}
