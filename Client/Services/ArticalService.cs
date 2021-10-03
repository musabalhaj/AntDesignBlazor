using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class ArticalService : IArticalService
    {
        private readonly HttpClient httpClient;

        public ArticalService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Artical> CreateArtical(Artical newArtical)
        {
            var Artical = await httpClient.PostAsJsonAsync<Artical>("/api/Articals", newArtical, null);
            return Artical.IsSuccessStatusCode ? newArtical : null;
        }

        public async Task DeleteArtical(int Id)
        {
            await httpClient.DeleteAsync($"/api/Articals/{Id}");
        }

        public async Task<Artical> GetArtical(int Id)
        {
            return await httpClient.GetFromJsonAsync<Artical>($"/api/Articals/{Id}");
        }

        public async Task<IEnumerable<Artical>> GetArticals()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Artical>>("/api/Articals");
        }

        public async Task<Artical> UpdateArtical(Artical updatedArtical)
        {
            var Artical = await httpClient.PutAsJsonAsync<Artical>("/api/Articals", updatedArtical, null);
            return Artical.IsSuccessStatusCode ? updatedArtical : null;
        }
    }
}
