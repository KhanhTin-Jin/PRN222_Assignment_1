namespace KhanhTin.BusinessLogic.Models
{
    public enum AccountRole
    {
        Admin = 0,
        Staff = 1,
        Lecturer = 2
    }

    public enum NewsStatus
    {
        Inactive = 0,
        Active = 1,
        Draft = 2,
        Archived = 3
    }

    public enum ReportType
    {
        NewsStatistics,
        CategoryAnalysis,
        UserActivity,
        TagUsage,
        CustomReport
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public enum FileType
    {
        Image,
        Document,
        Video,
        Audio,
        Other
    }

    public enum OperationType
    {
        Create,
        Read,
        Update,
        Delete,
        Search,
        Export,
        Import
    }
}
