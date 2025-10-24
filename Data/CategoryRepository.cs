using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportEventManager.Core;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;

namespace SportEventManager.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SportEventDbContext _context;

        public CategoryRepository(SportEventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await Task.FromResult(_context.Categories.ToList());
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return await Task.FromResult(category);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.InternalName = category.InternalName;
                existingCategory.ExternalName = category.ExternalName;
                existingCategory.Gender = category.Gender;
                existingCategory.MinAge = category.MinAge;
                existingCategory.MaxAge = category.MaxAge;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}