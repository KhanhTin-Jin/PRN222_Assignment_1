using System;
using System.Collections.Generic;
using System.Linq;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.BusinessLogic.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly INewsTagRepository _newsTagRepository;

        public NewsService(INewsRepository newsRepository, INewsTagRepository newsTagRepository)
        {
            _newsRepository = newsRepository;
            _newsTagRepository = newsTagRepository;
        }

        public IEnumerable<NewsArticleDto> GetActiveNews()
        {
            return _newsRepository.GetActive().Select(MapToDto);
        }

        public IEnumerable<NewsArticleDto> GetAllNews()
        {
            return _newsRepository.GetAll().Select(MapToDto);
        }

        public NewsArticleDto GetNewsById(int id)
        {
            var news = _newsRepository.GetById(id);
            return news == null ? null : MapToDto(news);
        }

        public void CreateNewsArticle(NewsArticleCreateDto dto, int creatorId)
        {
            var news = new NewsArticle
            {
                NewsTitle = dto.NewsTitle,
                Headline = dto.Headline,
                NewsContent = dto.NewsContent,
                NewsSource = dto.NewsSource,
                NewsStatus = dto.NewsStatus,
                CategoryID = dto.CategoryID,
                CreatedByID = creatorId,
                CreatedDate = DateTime.Now
            };

            _newsRepository.Add(news);
            _newsRepository.SaveChanges();

            // Add tags
            if (dto.SelectedTagIDs != null && dto.SelectedTagIDs.Any())
            {
                foreach (var tagId in dto.SelectedTagIDs)
                {
                    _newsTagRepository.Add(new NewsTag
                    {
                        NewsArticleID = news.NewsArticleID,
                        TagID = tagId
                    });
                }
                _newsTagRepository.SaveChanges();
            }
        }

        public void UpdateNewsArticle(NewsArticleUpdateDto dto, int updaterId)
        {
            var news = _newsRepository.GetById(dto.NewsArticleID);
            if (news == null)
            {
                return;
            }

            news.NewsTitle = dto.NewsTitle;
            news.Headline = dto.Headline;
            news.NewsContent = dto.NewsContent;
            news.NewsSource = dto.NewsSource;
            news.NewsStatus = dto.NewsStatus;
            news.CategoryID = dto.CategoryID;
            news.UpdatedByID = updaterId;
            news.ModifiedDate = DateTime.Now;

            _newsRepository.Update(news);
            _newsRepository.SaveChanges();

            // Update tags
            _newsTagRepository.DeleteByNewsId(news.NewsArticleID);

            if (dto.SelectedTagIDs != null && dto.SelectedTagIDs.Any())
            {
                foreach (var tagId in dto.SelectedTagIDs)
                {
                    _newsTagRepository.Add(new NewsTag
                    {
                        NewsArticleID = news.NewsArticleID,
                        TagID = tagId
                    });
                }
                _newsTagRepository.SaveChanges();
            }
        }

        public void DeleteNewsArticle(int id)
        {
            _newsTagRepository.DeleteByNewsId(id);
            _newsRepository.Delete(id);
            _newsRepository.SaveChanges();
        }

        public NewsSearchDto SearchNews(string searchTerm, int? categoryId = null, int? status = null)
        {
            var results = _newsRepository.Search(searchTerm);

            if (categoryId.HasValue)
            {
                results = results.Where(n => n.CategoryID == categoryId.Value);
            }

            if (status.HasValue)
            {
                results = results.Where(n => n.NewsStatus == status.Value);
            }

            return new NewsSearchDto
            {
                SearchTerm = searchTerm,
                CategoryID = categoryId,
                NewsStatus = status,
                Results = results.Select(MapToListDto).ToList(),
                TotalResults = results.Count()
            };
        }

        public IEnumerable<NewsArticleDto> GetNewsByCategory(int categoryId)
        {
            return _newsRepository.GetByCategory(categoryId).Select(MapToDto);
        }

        public IEnumerable<NewsArticleDto> GetNewsByCreator(int accountId)
        {
            return _newsRepository.GetByCreator(accountId).Select(MapToDto);
        }

        private NewsArticleDto MapToDto(NewsArticle news)
        {
            return new NewsArticleDto
            {
                NewsArticleID = news.NewsArticleID,
                NewsTitle = news.NewsTitle,
                Headline = news.Headline,
                NewsContent = news.NewsContent,
                NewsSource = news.NewsSource,
                NewsStatus = news.NewsStatus,
                CategoryID = news.CategoryID,
                CategoryName = news.Category?.CategoryName,
                CreatedDate = news.CreatedDate,
                CreatedByName = news.CreatedBy?.AccountName,
                ModifiedDate = news.ModifiedDate,
                UpdatedByName = news.UpdatedBy?.AccountName,
                SelectedTagIDs = news.NewsTags?.Select(nt => nt.TagID).ToList() ?? new List<int>(),
                TagNames = news.NewsTags?.Select(nt => nt.Tag.TagName).ToList() ?? new List<string>()
            };
        }

        private NewsArticleListDto MapToListDto(NewsArticle news)
        {
            return new NewsArticleListDto
            {
                NewsArticleID = news.NewsArticleID,
                NewsTitle = news.NewsTitle,
                Headline = news.Headline,
                CategoryName = news.Category?.CategoryName,
                NewsStatus = news.NewsStatus,
                CreatedDate = news.CreatedDate,
                CreatedByName = news.CreatedBy?.AccountName,
                ModifiedDate = news.ModifiedDate,
                UpdatedByName = news.UpdatedBy?.AccountName,
                TagNames = news.NewsTags?.Select(nt => nt.Tag.TagName).ToList() ?? new List<string>()
            };
        }
    }
}
