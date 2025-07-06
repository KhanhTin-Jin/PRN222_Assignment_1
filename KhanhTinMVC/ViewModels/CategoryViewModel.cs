using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KhanhTinMVC.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentCategoryID { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // Display properties
        public string ParentCategoryName { get; set; }
        public List<CategoryViewModel> ChildCategories { get; set; } = new List<CategoryViewModel>();
        public int NewsCount { get; set; }

        // For dropdown
        public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();

        public bool IsEdit => CategoryID > 0;
        public string StatusBadgeClass => IsActive ? "badge bg-success" : "badge bg-secondary";
        public string StatusName => IsActive ? "Active" : "Inactive";
    }

    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentCategoryID { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        // For dropdown
        public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();
    }

    public class CategoryEditViewModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Parent Category")]
        public int? ParentCategoryID { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        // For dropdown (excluding self and children to prevent circular reference)
        public List<SelectListItem> ParentCategories { get; set; } = new List<SelectListItem>();

        // Display info
        public int NewsCount { get; set; }
        public bool HasChildCategories { get; set; }
    }

    public class CategoryListViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ParentCategoryName { get; set; }
        public bool IsActive { get; set; }
        public int NewsCount { get; set; }
        public int ChildCategoriesCount { get; set; }

        public string StatusName => IsActive ? "Active" : "Inactive";
        public string StatusBadgeClass => IsActive ? "badge bg-success" : "badge bg-secondary";
        public bool CanDelete => NewsCount == 0 && ChildCategoriesCount == 0;
    }

    public class CategoryTreeViewModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsActive { get; set; }
        public int NewsCount { get; set; }
        public int Level { get; set; }
        public List<CategoryTreeViewModel> Children { get; set; } = new List<CategoryTreeViewModel>();

        public string StatusName => IsActive ? "Active" : "Inactive";
        public string StatusBadgeClass => IsActive ? "badge bg-success" : "badge bg-secondary";
        public string IndentClass => $"ms-{Level * 3}";
    }
}
