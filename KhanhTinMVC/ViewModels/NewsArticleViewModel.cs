using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhanhTinMVC.ViewModels
{
    public class NewsArticleViewModel
    {
        public int NewsArticleID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        [Display(Name = "News Title")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        [Display(Name = "Headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "News Content")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        [Display(Name = "News Source")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int NewsStatus { get; set; } // 1=Active, 0=Inactive

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByName { get; set; }

        [Display(Name = "Tags")]
        public List<int> SelectedTagIDs { get; set; } = new List<int>();

        // Display properties
        public string CategoryName { get; set; }
        public List<string> TagNames { get; set; } = new List<string>();
        public string StatusName => NewsStatus == 1 ? "Active" : "Inactive";
        public string StatusBadgeClass => NewsStatus == 1 ? "badge bg-success" : "badge bg-secondary";

        // For dropdowns
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();

        public bool IsEdit => NewsArticleID > 0;
    }

    public class NewsArticleCreateViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        [Display(Name = "News Title")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        [Display(Name = "Headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "News Content")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        [Display(Name = "News Source")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int NewsStatus { get; set; } = 1; // Default to Active

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Tags")]
        public List<int> SelectedTagIDs { get; set; } = new List<int>();

        // For dropdowns
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();
    }

    public class NewsArticleEditViewModel
    {
        public int NewsArticleID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        [Display(Name = "News Title")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        [Display(Name = "Headline")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [Display(Name = "News Content")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        [Display(Name = "News Source")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public int NewsStatus { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Tags")]
        public List<int> SelectedTagIDs { get; set; } = new List<int>();

        // For dropdowns
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Tags { get; set; } = new List<SelectListItem>();

        // Display info
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UpdatedByName { get; set; }
    }

    public class NewsArticleListViewModel
    {
        public int NewsArticleID { get; set; }
        public string NewsTitle { get; set; }
        public string Headline { get; set; }
        public string CategoryName { get; set; }
        public int NewsStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UpdatedByName { get; set; }
        public List<string> TagNames { get; set; } = new List<string>();

        public string StatusName => NewsStatus == 1 ? "Active" : "Inactive";
        public string StatusBadgeClass => NewsStatus == 1 ? "badge bg-success" : "badge bg-secondary";
    }

    public class NewsSearchViewModel
    {
        [Display(Name = "Search Term")]
        public string SearchTerm { get; set; }

        [Display(Name = "Category")]
        public int? CategoryID { get; set; }

        [Display(Name = "Status")]
        public int? NewsStatus { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<NewsArticleListViewModel> Results { get; set; } = new List<NewsArticleListViewModel>();

        public int TotalResults { get; set; }
    }
}
