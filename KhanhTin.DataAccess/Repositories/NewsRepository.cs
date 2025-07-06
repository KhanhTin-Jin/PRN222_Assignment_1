using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KhanhTin.DataAccess.Data;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public NewsRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IEnumerable<NewsArticle> GetActive()
        {
            return _dbcontext.NewsArticles
                .Where(n => n.NewsStatus == 1)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetAll()
        {
            return _dbcontext.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public NewsArticle GetById(int id)
        {
            return _dbcontext.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .FirstOrDefault(n => n.NewsArticleID == id);
        }

        public void Add(NewsArticle news)
        {
            _dbcontext.NewsArticles.Add(news);
        }

        public void Update(NewsArticle news)
        {
            _dbcontext.NewsArticles.Update(news);
        }

        public void Delete(int id)
        {
            var news = _dbcontext.NewsArticles.Find(id);
            if (news != null)
            {
                _dbcontext.NewsArticles.Remove(news);
            }
        }

        public IEnumerable<NewsArticle> Search(string searchTerm)
        {
            return _dbcontext.NewsArticles
                .Where(n => n.NewsTitle.Contains(searchTerm) ||
                           n.Headline.Contains(searchTerm) ||
                           n.NewsContent.Contains(searchTerm))
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetByCategory(int categoryId)
        {
            return _dbcontext.NewsArticles
                .Where(n => n.CategoryID == categoryId)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetByCreator(int accountId)
        {
            return _dbcontext.NewsArticles
                .Where(n => n.CreatedByID == accountId)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public IEnumerable<NewsArticle> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbcontext.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }

        public int CountByCategory(int categoryId)
        {
            return _dbcontext.NewsArticles.Count(n => n.CategoryID == categoryId);
        }

        public int CountByCreator(int accountId)
        {
            return _dbcontext.NewsArticles.Count(n => n.CreatedByID == accountId);
        }

        public int CountByStatus(int status)
        {
            return _dbcontext.NewsArticles.Count(n => n.NewsStatus == status);
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
