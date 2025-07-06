using System;
using System.Collections.Generic;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Interfaces
{
    public interface INewsRepository
    {
        IEnumerable<NewsArticle> GetActive();
        IEnumerable<NewsArticle> GetAll();
        NewsArticle GetById(int id);
        void Add(NewsArticle news);
        void Update(NewsArticle news);
        void Delete(int id);
        IEnumerable<NewsArticle> Search(string searchTerm);
        IEnumerable<NewsArticle> GetByCategory(int categoryId);
        IEnumerable<NewsArticle> GetByCreator(int accountId);
        IEnumerable<NewsArticle> GetByDateRange(DateTime startDate, DateTime endDate);
        int CountByCategory(int categoryId);
        int CountByCreator(int accountId);
        int CountByStatus(int status);
        void SaveChanges();
    }
}
