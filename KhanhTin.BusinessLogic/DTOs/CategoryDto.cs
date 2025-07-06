using System.ComponentModel.DataAnnotations;

namespace KhanhTin.BusinessLogic.DTOs
{
    public class CategoryDto
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string CategoryDescription { get; set; }

        public int? ParentCategoryID { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public string ParentCategoryName { get; set; }
        public List<CategoryDto> ChildCategories { get; set; } = new List<CategoryDto>();
        public int NewsCount { get; set; }
    }

    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string CategoryDescription { get; set; }

        public int? ParentCategoryID { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class CategoryUpdateDto
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string CategoryDescription { get; set; }

        public int? ParentCategoryID { get; set; }

        public bool IsActive { get; set; }
    }

    public class CategoryListDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ParentCategoryName { get; set; }
        public bool IsActive { get; set; }
        public int NewsCount { get; set; }
        public int ChildCategoriesCount { get; set; }

        public bool CanDelete => NewsCount == 0 && ChildCategoriesCount == 0;
    }
}
