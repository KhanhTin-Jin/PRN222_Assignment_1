using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanhTin.DataAccess.Models
{
    public class SystemAccount
    {
        [Key]
        public int AccountID { get; set; }
        public required string AccountName { get; set; }
        public required string AccountEmail { get; set; }
        public int AccountRole { get; set; } // 0=Admin, 1=Staff, 2=Lecturer
        public required string AccountPassword { get; set; } // Will be hashed

        public  ICollection<NewsArticle>? CreatedArticles { get; set; }
        public  ICollection<NewsArticle>? UpdatedArticles { get; set; }
    }

}
