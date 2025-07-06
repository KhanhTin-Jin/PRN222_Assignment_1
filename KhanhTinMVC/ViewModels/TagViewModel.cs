using System.ComponentModel.DataAnnotations;

namespace KhanhTinMVC.ViewModels
{
    public class TagViewModel
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        [Display(Name = "Note")]
        public string Note { get; set; }

        // Display properties
        public int NewsCount { get; set; }

        public bool IsEdit => TagID > 0;
        public bool CanDelete => NewsCount == 0;
    }

    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        [Display(Name = "Note")]
        public string Note { get; set; }
    }

    public class TagEditViewModel
    {
        public int TagID { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name cannot exceed 50 characters")]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        [StringLength(200, ErrorMessage = "Note cannot exceed 200 characters")]
        [Display(Name = "Note")]
        public string Note { get; set; }

        // Display info
        public int NewsCount { get; set; }
    }

    public class TagListViewModel
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string Note { get; set; }
        public int NewsCount { get; set; }

        public bool CanDelete => NewsCount == 0;
    }

    public class TagUsageViewModel
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public string Note { get; set; }
        public int NewsCount { get; set; }
        public int UsagePercentage { get; set; }

        public string UsageBadgeClass
        {
            get
            {
                if (UsagePercentage >= 75) return "badge bg-success";
                if (UsagePercentage >= 50) return "badge bg-warning";
                if (UsagePercentage >= 25) return "badge bg-info";
                return "badge bg-secondary";
            }
        }
    }
}
