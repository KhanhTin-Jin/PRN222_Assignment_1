using System;
using System.Linq;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using KhanhTin.DataAccess.Interfaces;

namespace KhanhTin.BusinessLogic.Services
{
    public class ReportService : IReportService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAccountRepository _accountRepository;

        public ReportService(
            INewsRepository newsRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IAccountRepository accountRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _accountRepository = accountRepository;
        }

        public ReportResultDto GenerateReport(ReportRequestDto request)
        {
            var newsArticles = _newsRepository.GetByDateRange(request.StartDate, request.EndDate);

            // Apply filters
            if (request.CategoryID.HasValue)
            {
                newsArticles = newsArticles.Where(n => n.CategoryID == request.CategoryID.Value);
            }

            if (request.CreatedByID.HasValue)
            {
                newsArticles = newsArticles.Where(n => n.CreatedByID == request.CreatedByID.Value);
            }

            if (request.NewsStatus.HasValue)
            {
                newsArticles = newsArticles.Where(n => n.NewsStatus == request.NewsStatus.Value);
            }

            var newsArticlesList = newsArticles.ToList();

            var result = new ReportResultDto
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalNewsArticles = newsArticlesList.Count,
                ActiveNewsArticles = newsArticlesList.Count(n => n.NewsStatus == 1),
                InactiveNewsArticles = newsArticlesList.Count(n => n.NewsStatus == 0),
                TotalCategories = _categoryRepository.GetAll().Count(),
                TotalTags = _tagRepository.GetAll().Count(),
                TotalCreators = _accountRepository.GetAll().Count(),
                GeneratedAt = DateTime.Now
            };

            // News articles
            result.NewsArticles = newsArticlesList.Select(n => new NewsReportItemDto
            {
                NewsArticleID = n.NewsArticleID,
                NewsTitle = n.NewsTitle,
                CategoryName = n.Category?.CategoryName,
                CreatedByName = n.CreatedBy?.AccountName,
                CreatedDate = n.CreatedDate,
                NewsStatus = n.NewsStatus,
                TagCount = n.NewsTags?.Count ?? 0
            }).ToList();

            // Category statistics
            var categories = _categoryRepository.GetAll();
            result.CategoryStatistics = categories.Select(c => new CategoryReportItemDto
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                NewsCount = _newsRepository.CountByCategory(c.CategoryID),
                ActiveNewsCount = newsArticlesList.Count(n => n.CategoryID == c.CategoryID && n.NewsStatus == 1),
                InactiveNewsCount = newsArticlesList.Count(n => n.CategoryID == c.CategoryID && n.NewsStatus == 0),
                Percentage = result.TotalNewsArticles > 0 ?
                    (double)_newsRepository.CountByCategory(c.CategoryID) / result.TotalNewsArticles * 100 : 0
            }).ToList();

            // Creator statistics
            var creators = _accountRepository.GetAll();
            result.CreatorStatistics = creators.Select(a => new CreatorReportItemDto
            {
                AccountID = a.AccountID,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                NewsCount = _newsRepository.CountByCreator(a.AccountID),
                ActiveNewsCount = newsArticlesList.Count(n => n.CreatedByID == a.AccountID && n.NewsStatus == 1),
                InactiveNewsCount = newsArticlesList.Count(n => n.CreatedByID == a.AccountID && n.NewsStatus == 0),
                LastNewsDate = newsArticlesList.Where(n => n.CreatedByID == a.AccountID)
                    .OrderByDescending(n => n.CreatedDate)
                    .FirstOrDefault()?.CreatedDate,
                Percentage = result.TotalNewsArticles > 0 ?
                    (double)_newsRepository.CountByCreator(a.AccountID) / result.TotalNewsArticles * 100 : 0
            }).ToList();

            // Tag statistics
            var tags = _tagRepository.GetAll();
            result.TagStatistics = tags.Select(t => new TagReportItemDto
            {
                TagID = t.TagID,
                TagName = t.TagName,
                NewsCount = _tagRepository.CountNewsWithTag(t.TagID),
                Percentage = result.TotalNewsArticles > 0 ?
                    (double)_tagRepository.CountNewsWithTag(t.TagID) / result.TotalNewsArticles * 100 : 0
            }).ToList();

            return result;
        }

        public DashboardDto GetDashboardData()
        {
            var totalNews = _newsRepository.CountByStatus(1) + _newsRepository.CountByStatus(0);
            var activeNews = _newsRepository.CountByStatus(1);
            var inactiveNews = _newsRepository.CountByStatus(0);

            var thisMonth = DateTime.Now.AddDays(-30);
            var lastMonth = DateTime.Now.AddDays(-60);

            var newsThisMonth = _newsRepository.GetByDateRange(thisMonth, DateTime.Now).Count();
            var newsLastMonth = _newsRepository.GetByDateRange(lastMonth, thisMonth).Count();

            var growthPercentage = newsLastMonth > 0 ?
                ((double)(newsThisMonth - newsLastMonth) / newsLastMonth) * 100 : 0;

            return new DashboardDto
            {
                TotalNews = totalNews,
                ActiveNews = activeNews,
                InactiveNews = inactiveNews,
                TotalCategories = _categoryRepository.GetAll().Count(),
                TotalTags = _tagRepository.GetAll().Count(),
                TotalUsers = _accountRepository.GetAll().Count(),
                NewsThisMonth = newsThisMonth,
                NewsLastMonth = newsLastMonth,
                NewsGrowthPercentage = growthPercentage,
                RecentNews = _newsRepository.GetActive().Take(5).Select(n => new NewsReportItemDto
                {
                    NewsArticleID = n.NewsArticleID,
                    NewsTitle = n.NewsTitle,
                    CategoryName = n.Category?.CategoryName,
                    CreatedByName = n.CreatedBy?.AccountName,
                    CreatedDate = n.CreatedDate,
                    NewsStatus = n.NewsStatus,
                    TagCount = n.NewsTags?.Count ?? 0
                }).ToList()
            };
        }
    }
}