using System.Collections.Generic;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetActive();
        Category GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        bool HasNews(int categoryId);
        bool HasChildCategories(int categoryId);
        IEnumerable<Category> GetParentCategories(int? excludeId = null);
        void SaveChanges();
    }
}
