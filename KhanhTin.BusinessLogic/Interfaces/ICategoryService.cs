using System.Collections.Generic;
using KhanhTin.BusinessLogic.DTOs;

namespace KhanhTin.BusinessLogic.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDto> GetAllCategories();
        IEnumerable<CategoryDto> GetActiveCategories();
        CategoryDto GetCategoryById(int id);
        void CreateCategory(CategoryCreateDto dto);
        void UpdateCategory(CategoryUpdateDto dto);
        void DeleteCategory(int id);
        IEnumerable<CategoryDto> GetParentCategories(int? excludeId = null);
        bool CanDeleteCategory(int id);
    }
}
