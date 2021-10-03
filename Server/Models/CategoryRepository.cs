using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Category> AddCategory(Category newCategory)
        {
            var result = await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Category> DeleteCategory(int Id)
        {
            var result = await context.Categories.FirstOrDefaultAsync(c => c.Id == Id);
            if (result != null)
            {
                context.Categories.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await context.Categories.FirstOrDefaultAsync(e => e.Id == categoryId);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await context.Categories.OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<Category> UpdateCategory(Category updatedCategory)
        {
            var result = await context.Categories.FirstOrDefaultAsync(c => c.Id == updatedCategory.Id);
            if (result != null)
            {
                result.Name = updatedCategory.Name;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
