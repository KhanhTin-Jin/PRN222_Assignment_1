using System;
using System.Collections.Generic;

namespace KhanhTin.BusinessLogic.Models
{
    public class ReportResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string GeneratedBy { get; set; }

        // Report metadata
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FilterCriteria { get; set; }

        // Summary statistics
        public int TotalNewsArticles { get; set; }
        public int ActiveNewsArticles { get; set; }
        public int InactiveNewsArticles { get; set; }
        public int TotalCategories { get; set; }
        public int TotalTags { get; set; }
        public int TotalCreators { get; set; }

        // Detailed data
        public List<ReportNewsItem> NewsArticles { get; set; } = new List<ReportNewsItem>();
        public List<ReportCategoryItem> CategoryStatistics { get; set; } = new List<ReportCategoryItem>();
        public List<ReportCreatorItem> CreatorStatistics { get; set; } = new List<ReportCreatorItem>();
        public List<ReportTagItem> TagStatistics { get; set; } = new List<ReportTagItem>();

        // Export information
        public List<string> AvailableFormats { get; set; } = new List<string> { "PDF", "Excel", "CSV" };
        public string ExportPath { get; set; }

        // Static factory methods
        public static ReportResult SuccessResult(string title, DateTime startDate, DateTime endDate, string generatedBy)
        {
            return new ReportResult
            {
                Success = true,
                StartDate = startDate,
                EndDate = endDate,
                GeneratedBy = generatedBy,
                GeneratedAt = DateTime.Now
            };
        }

        public static ReportResult FailureResult(string errorMessage)
        {
            return new ReportResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                GeneratedAt = DateTime.Now
            };
        }
    }

    public class ReportNewsItem
    {
        public int NewsArticleID { get; set; }
        public string NewsTitle { get; set; }
        public string CategoryName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NewsStatus { get; set; }
        public int TagCount { get; set; }
    }

    public class ReportCategoryItem
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int NewsCount { get; set; }
        public int ActiveNewsCount { get; set; }
        public int InactiveNewsCount { get; set; }
        public double Percentage { get; set; }
    }

    public class ReportCreatorItem
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public int NewsCount { get; set; }
        public int ActiveNewsCount { get; set; }
        public int InactiveNewsCount { get; set; }
        public DateTime? LastNewsDate { get; set; }
        public double Percentage { get; set; }
    }

    public class ReportTagItem
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public int NewsCount { get; set; }
        public double Percentage { get; set; }
    }
}
