using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using System.Linq;

namespace KhanhTinMVC.Controllers
{
    [Authorize(Roles = "1")] // Staff only
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories().ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            var model = new CategoryCreateDto();
            ViewBag.ParentCategories = GetParentCategorySelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryCreateDto model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(model);
                TempData["SuccessMessage"] = "Category created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ParentCategories = GetParentCategorySelectList();
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryUpdateDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentCategoryID = category.ParentCategoryID,
                IsActive = category.IsActive
            };

            ViewBag.ParentCategories = GetParentCategorySelectList(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(model);
                TempData["SuccessMessage"] = "Category updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ParentCategories = GetParentCategorySelectList(model.CategoryID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!_categoryService.CanDeleteCategory(id))
            {
                TempData["ErrorMessage"] = "Cannot delete category. It has associated news articles or child categories.";
                return RedirectToAction(nameof(Index));
            }

            _categoryService.DeleteCategory(id);
            TempData["SuccessMessage"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        private List<SelectListItem> GetParentCategorySelectList(int? excludeId = null)
        {
            var categories = _categoryService.GetParentCategories(excludeId);
            var selectList = new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = "-- No Parent --" }
            };

            selectList.AddRange(categories.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName
            }));

            return selectList;
        }
    }
}
