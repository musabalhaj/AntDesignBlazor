using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();

        Task<Category> GetCategory(int id);

        Task<Category> CreateCategory(Category newCategory);

        Task<Category> UpdateCategory(Category updatedCategory);

        Task DeleteCategory(int Id);
    }
}
