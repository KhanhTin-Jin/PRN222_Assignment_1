using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using System.Linq; // Đảm bảo đã include

namespace KhanhTinMVC.Controllers
{
    // Mặc định, chỉ Staff (Role "1") có thể truy cập các hành động trong Controller này
    // trừ khi có thuộc tính [AllowAnonymous] hoặc [Authorize(Roles = "...")] khác ghi đè.
    [Authorize(Roles = "1,2")] // Staff only
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public NewsController(
            INewsService newsService,
            ICategoryService categoryService,
            ITagService tagService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        // Action mặc định cho Staff: Xem TẤT CẢ tin tức (bao gồm cả active và inactive) để quản lý
        public IActionResult Index()
        {
            var news = _newsService.GetAllNews().ToList();
            return View(news);
        }

        // --- Các Actions dành riêng cho Giảng viên ---

        // Action này dành cho trang chính của Giảng viên (NewsManagement.cshtml).
        // Chỉ Giảng viên (Role "2") và Staff (Role "1") mới được phép truy cập trang này.
        // Nó sẽ chỉ hiển thị các tin tức ĐANG HOẠT ĐỘNG.
        [Authorize(Roles = "1,2")] // Cho phép cả Staff và Lecturer truy cập trang danh sách tin tức
        public IActionResult NewsManagement()
        {
            // Gọi phương thức GetActiveNews() đã có sẵn trong INewsService
            var activeNews = _newsService.GetActiveNews();
            return View(activeNews);
        }

        // Action Details (Xem chi tiết tin tức):
        // Cả Staff (Role "1") và Giảng viên (Role "2") đều có thể truy cập.
        // Quan trọng: Giảng viên chỉ xem được tin tức ĐANG HOẠT ĐỘNG.
        [Authorize(Roles = "1,2")] // Cho phép cả Staff và Lecturer truy cập chi tiết tin tức
        public IActionResult Details(int id)
        {
            var news = _newsService.GetNewsById(id);

            // Kiểm tra: Nếu tin tức không tồn tại HOẶC (nếu người dùng là Giảng viên VÀ tin tức KHÔNG active)
            // LƯU Ý: User.IsInRole("2") kiểm tra xem người dùng hiện tại có vai trò "2" (Lecturer) hay không.
            // Điều này đảm bảo Staff vẫn có thể xem chi tiết tin tức inactive nếu cần.
            if (news == null || (User.IsInRole("2") && news.NewsStatus != 1))
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy hoặc không được phép xem
            }

            return View(news);
        }

        // --- Các Actions quản lý tiêu chuẩn cho Staff/Admin ---
        // Các action này vẫn chịu sự ràng buộc từ [Authorize(Roles = "1")] ở cấp Controller,
        // đảm bảo chỉ Staff (Role 1) mới có thể tạo, chỉnh sửa, xóa.
        public IActionResult Create()
        {
            var model = new NewsArticleCreateDto();
            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticleCreateDto model)
        {
            if (ModelState.IsValid)
            {
                int creatorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _newsService.CreateNewsArticle(model, creatorId);
                TempData["SuccessMessage"] = "News article created successfully.";
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(model);
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var news = _newsService.GetNewsById(id);
            if (news == null)
            {
                return NotFound();
            }

            var model = new NewsArticleUpdateDto
            {
                NewsArticleID = news.NewsArticleID,
                NewsTitle = news.NewsTitle,
                Headline = news.Headline,
                NewsContent = news.NewsContent,
                NewsSource = news.NewsSource,
                NewsStatus = news.NewsStatus,
                CategoryID = news.CategoryID,
                SelectedTagIDs = news.SelectedTagIDs
            };

            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticleUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                int updaterId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _newsService.UpdateNewsArticle(model, updaterId);
                TempData["SuccessMessage"] = "News article updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _newsService.DeleteNewsArticle(id);
            TempData["SuccessMessage"] = "News article deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string searchTerm, int? categoryId, int? status)
        {
            var results = _newsService.SearchNews(searchTerm, categoryId, status);
            ViewBag.Categories = GetCategorySelectList();
            return View(results);
        }

        public IActionResult MyNews()
        {
            int accountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var news = _newsService.GetNewsByCreator(accountId).ToList();
            return View(news);
        }

        // --- Các phương thức helper (không thay đổi) ---
        private void PopulateDropdowns(NewsArticleCreateDto model)
        {
            ViewBag.Categories = GetCategorySelectList();
            ViewBag.Tags = GetTagSelectList();
            ViewBag.StatusOptions = GetStatusSelectList();
        }

        private void PopulateDropdowns(NewsArticleUpdateDto model)
        {
            ViewBag.Categories = GetCategorySelectList();
            ViewBag.Tags = GetTagSelectList();
            ViewBag.StatusOptions = GetStatusSelectList();
        }

        private List<SelectListItem> GetCategorySelectList()
        {
            return _categoryService.GetActiveCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList();
        }

        private List<SelectListItem> GetTagSelectList()
        {
            return _tagService.GetAllTags()
                .Select(t => new SelectListItem
                {
                    Value = t.TagID.ToString(),
                    Text = t.TagName
                }).ToList();
        }

        private List<SelectListItem> GetStatusSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Active" },
                new SelectListItem { Value = "0", Text = "Inactive" }
            };
        }
    }
}