using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItems();
        Task<Item> GetItem(int itemId);

        Task<Item> GetItemByName(string name);
        Task<Item> AddItem(Item item);
        Task<Item> UpdateItem(Item item);
        Task<Item> DeleteItem(int itemId);
    }
}
