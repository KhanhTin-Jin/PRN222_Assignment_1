namespace KhanhTin.BusinessLogic.Models
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Admin = "0";
            public const string Staff = "1";
            public const string Lecturer = "2";
        }

        public static class NewsStatus
        {
            public const int Inactive = 0;
            public const int Active = 1;
            public const int Draft = 2;
            public const int Archived = 3;
        }

        public static class CacheKeys
        {
            public const string AllCategories = "all_categories";
            public const string ActiveCategories = "active_categories";
            public const string AllTags = "all_tags";
            public const string ActiveNews = "active_news";
            public const string DashboardData = "dashboard_data";
        }

        public static class ErrorCodes
        {
            public const string ValidationFailed = "VALIDATION_FAILED";
            public const string NotFound = "NOT_FOUND";
            public const string Unauthorized = "UNAUTHORIZED";
            public const string DuplicateEntry = "DUPLICATE_ENTRY";
            public const string CannotDelete = "CANNOT_DELETE";
            public const string InvalidOperation = "INVALID_OPERATION";
        }

        public static class FileSettings
        {
            public const int MaxFileSize = 5 * 1024 * 1024; // 5MB
            public static readonly string[] AllowedImageTypes = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            public static readonly string[] AllowedDocumentTypes = { ".pdf", ".doc", ".docx", ".txt" };
        }

        public static class Pagination
        {
            public const int DefaultPageSize = 10;
            public const int MaxPageSize = 100;
        }

        public static class Validation
        {
            public const int MaxTitleLength = 200;
            public const int MaxHeadlineLength = 500;
            public const int MaxNameLength = 100;
            public const int MaxEmailLength = 100;
            public const int MaxDescriptionLength = 500;
            public const int MaxNoteLength = 200;
            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 100;
        }
    }
}
