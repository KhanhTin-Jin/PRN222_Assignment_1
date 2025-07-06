using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using System.Security.Claims;
using System.Linq;

namespace KhanhTinMVC.Controllers
{
    [Authorize(Roles = "0")] // Admin only
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;

        public ReportController(
            IReportService reportService,
            ICategoryService categoryService,
            IAccountService accountService)
        {
            _reportService = reportService;
            _categoryService = categoryService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            var model = new ReportRequestDto();
            PopulateDropdowns();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Generate(ReportRequestDto model)
        {
            if (ModelState.IsValid)
            {
                var result = _reportService.GenerateReport(model);
                result.GeneratedBy = User.FindFirstValue(ClaimTypes.Name);
                return View("ReportResult", result);
            }

            PopulateDropdowns();
            return View("Index", model);
        }

        public IActionResult Dashboard()
        {
            var dashboardData = _reportService.GetDashboardData();
            return View(dashboardData);
        }

        private void PopulateDropdowns()
        {
            ViewBag.Categories = GetCategorySelectList();
            ViewBag.Creators = GetCreatorSelectList();
            ViewBag.StatusOptions = GetStatusSelectList();
        }

        private List<SelectListItem> GetCategorySelectList()
        {
            var categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "All Categories" }
            };

            categories.AddRange(_categoryService.GetAllCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }));

            return categories;
        }

        private List<SelectListItem> GetCreatorSelectList()
        {
            var creators = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "All Creators" }
            };

            creators.AddRange(_accountService.GetAllAccounts()
                .Select(a => new SelectListItem
                {
                    Value = a.AccountID.ToString(),
                    Text = a.AccountName
                }));

            return creators;
        }

        private List<SelectListItem> GetStatusSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "All Status" },
                new SelectListItem { Value = "1", Text = "Active" },
                new SelectListItem { Value = "0", Text = "Inactive" }
            };
        }
    }
}