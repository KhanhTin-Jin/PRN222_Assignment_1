using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using System.Linq;

namespace KhanhTinMVC.Controllers
{
    [Authorize(Roles = "1")] // Staff only
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index()
        {
            var tags = _tagService.GetAllTags().ToList();
            return View(tags);
        }

        public IActionResult Details(int id)
        {
            var tag = _tagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        public IActionResult Create()
        {
            return View(new TagCreateDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagCreateDto model)
        {
            if (ModelState.IsValid)
            {
                _tagService.CreateTag(model);
                TempData["SuccessMessage"] = "Tag created successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var tag = _tagService.GetTagById(id);
            if (tag == null)
            {
                return NotFound();
            }

            var model = new TagUpdateDto
            {
                TagID = tag.TagID,
                TagName = tag.TagName,
                Note = tag.Note
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                _tagService.UpdateTag(model);
                TempData["SuccessMessage"] = "Tag updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Tags/Delete/{id?}")]
        public IActionResult Delete(int id)
        {
            if (!_tagService.CanDeleteTag(id))
            {
                TempData["ErrorMessage"] = "Cannot delete tag. It is associated with news articles.";
                return RedirectToAction("Index");
            }
            _tagService.DeleteTag(id);
            TempData["SuccessMessage"] = "Tag deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}