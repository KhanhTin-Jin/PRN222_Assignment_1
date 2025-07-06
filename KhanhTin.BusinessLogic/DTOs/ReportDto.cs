using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KhanhTin.BusinessLogic.DTOs
{
    public class ReportRequestDto
    {
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public int? CategoryID { get; set; }
        public int? CreatedByID { get; set; }
        public int? NewsStatus { get; set; }
    }

    public class ReportResultDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FilterSummary { get; set; }

        // Summary statistics
        public int TotalNewsArticles { get; set; }
        public int ActiveNewsArticles { get; set; }
        public int InactiveNewsArticles { get; set; }
        public int TotalCategories { get; set; }
        public int TotalTags { get; set; }
        public int TotalCreators { get; set; }

        // Detailed data
        public List<NewsReportItemDto> NewsArticles { get; set; } = new List<NewsReportItemDto>();
        public List<CategoryReportItemDto> CategoryStatistics { get; set; } = new List<CategoryReportItemDto>();
        public List<CreatorReportItemDto> CreatorStatistics { get; set; } = new List<CreatorReportItemDto>();
        public List<TagReportItemDto> TagStatistics { get; set; } = new List<TagReportItemDto>();

        public DateTime GeneratedAt { get; set; } = DateTime.Now;
        public string GeneratedBy { get; set; }
    }

    public class NewsReportItemDto
    {
        public int NewsArticleID { get; set; }
        public string NewsTitle { get; set; }
        public string CategoryName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NewsStatus { get; set; }
        public int TagCount { get; set; }

        public string StatusName => NewsStatus == 1 ? "Active" : "Inactive";
    }

    public class CategoryReportItemDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int NewsCount { get; set; }
        public int ActiveNewsCount { get; set; }
        public int InactiveNewsCount { get; set; }
        public double Percentage { get; set; }
    }

    public class CreatorReportItemDto
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

    public class TagReportItemDto
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public int NewsCount { get; set; }
        public double Percentage { get; set; }
    }

    public class DashboardDto
    {
        public int TotalNews { get; set; }
        public int ActiveNews { get; set; }
        public int InactiveNews { get; set; }
        public int TotalCategories { get; set; }
        public int TotalTags { get; set; }
        public int TotalUsers { get; set; }

        public List<NewsReportItemDto> RecentNews { get; set; } = new List<NewsReportItemDto>();
        public List<CategoryReportItemDto> TopCategories { get; set; } = new List<CategoryReportItemDto>();
        public List<CreatorReportItemDto> TopCreators { get; set; } = new List<CreatorReportItemDto>();

        public int NewsThisMonth { get; set; }
        public int NewsLastMonth { get; set; }
        public double NewsGrowthPercentage { get; set; }
    }
}
