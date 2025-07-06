using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KhanhTin.DataAccess.Data;
using KhanhTin.DataAccess.Interfaces;
using KhanhTin.DataAccess.Models;

namespace KhanhTin.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CategoryRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbcontext.Categories
                .Include(c => c.ParentCategory)
                .ToList();
        }

        public IEnumerable<Category> GetActive()
        {
            return _dbcontext.Categories
                .Where(c => c.IsActive)
                .Include(c => c.ParentCategory)
                .ToList();
        }

        public Category GetById(int id)
        {
            return _dbcontext.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefault(c => c.CategoryID == id);
        }

        public void Add(Category category)
        {
            _dbcontext.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _dbcontext.Categories.Update(category);
        }

        public void Delete(int id)
        {
            var category = _dbcontext.Categories.Find(id);
            if (category != null)
            {
                _dbcontext.Categories.Remove(category);
            }
        }

        public bool HasNews(int categoryId)
        {
            return _dbcontext.NewsArticles.Any(n => n.CategoryID == categoryId);
        }

        public bool HasChildCategories(int categoryId)
        {
            return _dbcontext.Categories.Any(c => c.ParentCategoryID == categoryId);
        }

        public IEnumerable<Category> GetParentCategories(int? excludeId = null)
        {
            var query = _dbcontext.Categories.AsQueryable();

            if (excludeId.HasValue)
            {
                // Exclude the category itself and its children to prevent circular references
                var childIds = _dbcontext.Categories
                    .Where(c => c.ParentCategoryID == excludeId)
                    .Select(c => c.CategoryID)
                    .ToList();

                childIds.Add(excludeId.Value);

                query = query.Where(c => !childIds.Contains(c.CategoryID));
            }

            return query.ToList();
        }

        public void SaveChanges()
        {
            _dbcontext.SaveChanges();
        }
    }
}
