using System.Collections.Generic;
using System.Linq;
using KhanhTin.BusinessLogic.DTOs;
using KhanhTin.BusinessLogic.Interfaces;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly INewsRepository _newsRepository;

        public CategoryService(ICategoryRepository categoryRepository, INewsRepository newsRepository)
        {
            _categoryRepository = categoryRepository;
            _newsRepository = newsRepository;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _categoryRepository.GetAll().Select(MapToDto);
        }

        public IEnumerable<CategoryDto> GetActiveCategories()
        {
            return _categoryRepository.GetActive().Select(MapToDto);
        }

        public CategoryDto GetCategoryById(int id)
        {
            var category = _categoryRepository.GetById(id);
            return category == null ? null : MapToDto(category);

        }

        public void CreateCategory(CategoryCreateDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CategoryDescription = dto.CategoryDescription,
                ParentCategoryID = dto.ParentCategoryID,
                IsActive = dto.IsActive
            };

            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
        }

        public void UpdateCategory(CategoryUpdateDto dto)
        {
            var category = _categoryRepository.GetById(dto.CategoryID);
            if (category == null)
            {
                return;
            }

            category.CategoryName = dto.CategoryName;
            category.CategoryDescription = dto.CategoryDescription;
            category.ParentCategoryID = dto.ParentCategoryID;
            category.IsActive = dto.IsActive;

            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.SaveChanges();
        }

        public IEnumerable<CategoryDto> GetParentCategories(int? excludeId = null)
        {
            return _categoryRepository.GetParentCategories(excludeId).Select(MapToDto);
        }

        public bool CanDeleteCategory(int id)
        {
            return !_categoryRepository.HasNews(id) && !_categoryRepository.HasChildCategories(id);
        }

        private CategoryDto MapToDto(Category category)
        {
            return new CategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentCategoryID = category.ParentCategoryID,
                IsActive = category.IsActive,
                ParentCategoryName = category.ParentCategory?.CategoryName,
                NewsCount = _newsRepository.CountByCategory(category.CategoryID)
            };
        }
    }
}
