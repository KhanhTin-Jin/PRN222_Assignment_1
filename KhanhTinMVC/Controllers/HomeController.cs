using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using KhanhTinMVC.Models;
using KhanhTin.BusinessLogic.Interfaces;
using System.Security.Claims;

namespace KhanhTinMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly IReportService _reportService;

        public HomeController(
            INewsService newsService,
            ICategoryService categoryService,
            IReportService reportService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                // Admin sees dashboard
                if (userRole == "0")
                {
                    var dashboardData = _reportService.GetDashboardData();
                    return View("Dashboard", dashboardData);
                }

                // Staff and Lecturer see news management
                var allNews = _newsService.GetAllNews();
                return View("NewsManagement", allNews);
            }

            // Public users see active news
            var activeNews = _newsService.GetActiveNews();
            return View("PublicNews", activeNews);
        }

        [Authorize(Roles = "1,2")] // Cho phép cả Staff và Lecturer truy cập trang danh sách tin tức
        public IActionResult NewsManagement()
        {
            // Gọi phương thức GetActiveNews() đã có sẵn trong INewsService
            var activeNews = _newsService.GetActiveNews();
            return View(activeNews);
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            var dashboardData = _reportService.GetDashboardData();
            return View(dashboardData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
