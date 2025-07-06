using System.ComponentModel.DataAnnotations;

namespace KhanhTin.BusinessLogic.DTOs
{
    public class NewsArticleDto
    {
        public int NewsArticleID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int NewsStatus { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string UpdatedByName { get; set; }

        public string CategoryName { get; set; }
        public List<int> SelectedTagIDs { get; set; } = new List<int>();
        public List<string> TagNames { get; set; } = new List<string>();

        public string StatusName => NewsStatus == 1 ? "Active" : "Inactive";
    }

    public class NewsArticleCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int NewsStatus { get; set; } = 1;

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        public List<int> SelectedTagIDs { get; set; } = new List<int>();
    }

    public class NewsArticleUpdateDto
    {
        public int NewsArticleID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Headline is required")]
        [StringLength(500, ErrorMessage = "Headline cannot exceed 500 characters")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string NewsContent { get; set; }

        [StringLength(200, ErrorMessage = "Source cannot exceed 200 characters")]
        public string NewsSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int NewsStatus { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }

        public List<int> SelectedTagIDs { get; set; } = new List<int>();
    }

    public class NewsArticleListDto
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
    }

    public class NewsSearchDto
    {
        public string SearchTerm { get; set; }
        public int? CategoryID { get; set; }
        public int? NewsStatus { get; set; }
        public List<NewsArticleListDto> Results { get; set; } = new List<NewsArticleListDto>();
        public int TotalResults { get; set; }
    }
}
