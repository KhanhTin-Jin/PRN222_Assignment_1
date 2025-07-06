using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanhTin.DataAccess.Models
{
    public class NewsTag
    {
        public int NewsArticleID { get; set; }
        public int TagID { get; set; }

        // Navigation properties
        public  NewsArticle? NewsArticle { get; set; }
        public  Tag? Tag { get; set; }
    }
}
