using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int categoryId);
        Task<Category> AddCategory(Category newCategory);
        Task<Category> UpdateCategory(Category updatedCategory);
        Task<Category> DeleteCategory(int Id);
    }
}
