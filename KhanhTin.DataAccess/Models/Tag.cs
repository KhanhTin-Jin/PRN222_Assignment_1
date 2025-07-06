using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhanhTin.DataAccess.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public required string TagName { get; set; }
        public string? Note { get; set; }
   
        public  ICollection<NewsTag>? NewsTags { get; set; }
    }
}
