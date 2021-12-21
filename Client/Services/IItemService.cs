using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItems();

        Task<Item> GetItem(int id);

        Task<Item> GetItemByName(string name);
        Task<Item> CreateItem(Item newItem);

        Task<Item> UpdateItem(Item updatedItem);

        Task DeleteItem(int Id);
    }
}
