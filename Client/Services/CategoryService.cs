using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Category> CreateCategory(Category newCategory)
        {
            var category = await httpClient.PostAsJsonAsync<Category>("/api/Categories", newCategory, null);
            return category.IsSuccessStatusCode ? newCategory : null;
        }

        public async Task DeleteCategory(int Id)
        {
            await httpClient.DeleteAsync($"/api/Categories/{Id}");
        }

        public async Task<Category> GetCategory(int Id)
        {
            return await httpClient.GetFromJsonAsync<Category>($"/api/Categories/{Id}");
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Category>>("/api/Categories");
        }

        public async Task<Category> UpdateCategory(Category updatedCategory)
        {
            var Category = await httpClient.PutAsJsonAsync<Category>("/api/Categories", updatedCategory, null);
            return Category.IsSuccessStatusCode ? updatedCategory : null;
        }
    }
}
