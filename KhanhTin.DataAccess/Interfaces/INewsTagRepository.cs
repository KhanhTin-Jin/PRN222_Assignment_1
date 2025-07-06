using System.Collections.Generic;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Interfaces
{
    public interface INewsTagRepository
    {
        IEnumerable<NewsTag> GetByNewsId(int newsId);
        IEnumerable<NewsTag> GetByTagId(int tagId);
        void Add(NewsTag newsTag);
        void DeleteByNewsId(int newsId);
        void DeleteByTagId(int tagId);
        void SaveChanges();
    }
}
