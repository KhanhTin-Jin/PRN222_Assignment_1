using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanhTin.DataAccess.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public required string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? ParentCategoryID { get; set; } // Nullable for top-level categories
        public bool IsActive { get; set; }

        public  Category? ParentCategory { get; set; }
        public  ICollection<Category>? ChildCategories { get; set; }
        public  ICollection<NewsArticle>? NewsArticles { get; set; }
    }
}
