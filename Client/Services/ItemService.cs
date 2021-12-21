using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient httpClient;

        public ItemService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Item> CreateItem(Item newItem)
        {
            var Item = await httpClient.PostAsJsonAsync<Item>("/api/Items", newItem, null);
            return Item.IsSuccessStatusCode ? newItem : null;
        }

        public async Task DeleteItem(int Id)
        {
            await httpClient.DeleteAsync($"/api/Items/{Id}");
        }

        public async Task<Item> GetItem(int Id)
        {
            return await httpClient.GetFromJsonAsync<Item>($"/api/Items/{Id}");
        }
        public async Task<Item> GetItemByName(string name)
        {
            return await httpClient.GetFromJsonAsync<Item>($"/api/Items/GetItemByName?name={name}");
        }
        public async Task<IEnumerable<Item>> GetItems()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Item>>("/api/Items");
        }

        public async Task<Item> UpdateItem(Item updatedItem)
        {
            var Item = await httpClient.PutAsJsonAsync<Item>("/api/Items", updatedItem, null);
            return Item.IsSuccessStatusCode ? updatedItem : null;
        }
    }
}
