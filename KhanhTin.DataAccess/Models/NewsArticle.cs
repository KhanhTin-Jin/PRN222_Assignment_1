using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanhTin.DataAccess.Models
{
    public class NewsArticle
    {
        public int NewsArticleID { get; set; }
        public required string NewsTitle { get; set; }
        public required string Headline { get; set; }
        public DateTime CreatedDate { get; set; }
        public required string NewsContent { get; set; }
        public required string NewsSource { get; set; }
        public int NewsStatus { get; set; } // 1=Active, 0=Inactive
        public int CategoryID { get; set; }
        public int CreatedByID { get; set; }
        public int? UpdatedByID { get; set; } // Nullable
        public DateTime? ModifiedDate { get; set; } // Nullable

        // Navigation properties
        public Category? Category { get; set; }
        public SystemAccount? CreatedBy { get; set; }
        public SystemAccount? UpdatedBy { get; set; }
        public ICollection<NewsTag>? NewsTags { get; set; }
    }
}
