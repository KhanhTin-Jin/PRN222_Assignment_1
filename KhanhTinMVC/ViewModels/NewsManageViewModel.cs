using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTinMVC.ViewModels
{
    public class NewsManageViewModel
    {
        public List<NewsArticleDto> NewsArticles { get; set; } = new List<NewsArticleDto>();
        public string SearchTerm { get; set; }
        public int? CategoryFilter { get; set; }
        public int? StatusFilter { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "All Status" },
            new SelectListItem { Value = "1", Text = "Active" },
            new SelectListItem { Value = "0", Text = "Inactive" }
        };
    }

    public class NewsFormViewModel
    {
        public NewsArticleCreateDto CreateDto { get; set; }
        public NewsArticleUpdateDto UpdateDto { get; set; }
        public bool IsEdit { get; set; }

        public List<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TagOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> StatusOptions { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Active" },
            new SelectListItem { Value = "0", Text = "Inactive" }
        };
    }

    public class NewsPublicViewModel
    {
        public List<NewsArticleDto> FeaturedNews { get; set; } = new List<NewsArticleDto>();
        public List<NewsArticleDto> LatestNews { get; set; } = new List<NewsArticleDto>();
        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public NewsSearchDto SearchResults { get; set; }
    }
}
