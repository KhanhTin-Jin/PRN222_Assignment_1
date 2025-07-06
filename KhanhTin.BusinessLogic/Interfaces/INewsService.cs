using System.Collections.Generic;
using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTin.BusinessLogic.Interfaces
{
    public interface INewsService
    {
        IEnumerable<NewsArticleDto> GetActiveNews();
        IEnumerable<NewsArticleDto> GetAllNews();
        NewsArticleDto GetNewsById(int id);
        void CreateNewsArticle(NewsArticleCreateDto dto, int creatorId);
        void UpdateNewsArticle(NewsArticleUpdateDto dto, int updaterId);
        void DeleteNewsArticle(int id);
        NewsSearchDto SearchNews(string searchTerm, int? categoryId = null, int? status = null);
        IEnumerable<NewsArticleDto> GetNewsByCategory(int categoryId);
        IEnumerable<NewsArticleDto> GetNewsByCreator(int accountId);
    }
}
