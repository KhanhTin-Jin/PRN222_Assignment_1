using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTinMVC.ViewModels
{
    public class ReportViewModel
    {
        public ReportRequestDto RequestDto { get; set; } = new ReportRequestDto();
        public ReportResultDto ResultDto { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CreatorOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Status" },
            new SelectListItem { Value = "1", Text = "Active" },
            new SelectListItem { Value = "0", Text = "Inactive" }
        };
    }

    public class ReportResultViewModel
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
        public List<NewsReportItemViewModel> NewsArticles { get; set; } = new List<NewsReportItemViewModel>();
        public List<CategoryReportItemViewModel> CategoryStatistics { get; set; } = new List<CategoryReportItemViewModel>();
        public List<CreatorReportItemViewModel> CreatorStatistics { get; set; } = new List<CreatorReportItemViewModel>();
        public List<TagReportItemViewModel> TagStatistics { get; set; } = new List<TagReportItemViewModel>();

        // Charts data
        public List<ChartDataViewModel> NewsPerDayChart { get; set; } = new List<ChartDataViewModel>();
        public List<ChartDataViewModel> NewsByCategoryChart { get; set; } = new List<ChartDataViewModel>();
        public List<ChartDataViewModel> NewsByCreatorChart { get; set; } = new List<ChartDataViewModel>();

        public DateTime GeneratedAt { get; set; } = DateTime.Now;
        public string GeneratedBy { get; set; }
    }

    public class NewsReportItemViewModel
    {
        public int NewsArticleID { get; set; }
        public string NewsTitle { get; set; }
        public string CategoryName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NewsStatus { get; set; }
        public string StatusName => NewsStatus == 1 ? "Active" : "Inactive";
        public string StatusBadgeClass => NewsStatus == 1 ? "badge bg-success" : "badge bg-secondary";
        public int TagCount { get; set; }
    }

    public class CategoryReportItemViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int NewsCount { get; set; }
        public int ActiveNewsCount { get; set; }
        public int InactiveNewsCount { get; set; }
        public double Percentage { get; set; }
    }

    public class CreatorReportItemViewModel
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

    public class TagReportItemViewModel
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public int NewsCount { get; set; }
        public double Percentage { get; set; }
        public string UsageBadgeClass
        {
            get
            {
                if (Percentage >= 75) return "badge bg-success";
                if (Percentage >= 50) return "badge bg-warning";
                if (Percentage >= 25) return "badge bg-info";
                return "badge bg-secondary";
            }
        }
    }

    public class ChartDataViewModel
    {
        public string Label { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public double Percentage { get; set; }
    }

    public class DashboardViewModel
    {
        public DashboardDto DashboardData { get; set; }
        public List<ChartDataViewModel> NewsPerDayChart { get; set; } = new List<ChartDataViewModel>();
        public List<ChartDataViewModel> NewsByCategoryChart { get; set; } = new List<ChartDataViewModel>();
    }
}
