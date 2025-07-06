using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KhanhTin.DataAccess.Data;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Repositories
{
    public class NewsTagRepository : INewsTagRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public NewsTagRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IEnumerable<NewsTag> GetByNewsId(int newsId)
        {
            return _dbcontext.NewsTags
                .Where(nt => nt.NewsArticleID == newsId)
                .Include(nt => nt.Tag)
                .ToList();
        }

        public IEnumerable<NewsTag> GetByTagId(int tagId)
        {
            return _dbcontext.NewsTags
                .Where(nt => nt.TagID == tagId)
                .Include(nt => nt.NewsArticle)
                .ToList();
        }

        public void Add(NewsTag newsTag)
        {
            _dbcontext.NewsTags.Add(newsTag);
        }

        public void DeleteByNewsId(int newsId)
        {
            var newsTags = _dbcontext.NewsTags.Where(nt => nt.NewsArticleID == newsId);
            _dbcontext.NewsTags.RemoveRange(newsTags);
        }

        public void DeleteByTagId(int tagId)
        {
            var newsTags = _dbcontext.NewsTags.Where(nt => nt.TagID == tagId);
            _dbcontext.NewsTags.RemoveRange(newsTags);
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
