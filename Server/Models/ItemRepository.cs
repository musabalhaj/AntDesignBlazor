using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext context;

        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Item> AddItem(Item item)
        {
            var result = await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Item> DeleteItem(int itemId)
        {
            var result = await context.Items.FirstOrDefaultAsync(a => a.Id == itemId);
            if (result != null)
            {
                context.Items.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Item> GetItem(int itemId)
        {
            return await context.Items
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == itemId);

        }

        public async Task<Item> GetItemByName(string name)
        {
            return await context.Items
                .FirstOrDefaultAsync(a => a.Name == name);

        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await context.Items
                .Include(a => a.Category)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
        }


        public async Task<Item> UpdateItem(Item item)
        {
            var result = await context.Items.FirstOrDefaultAsync(e => e.Id == item.Id);
            if (result != null)
            {
                result.Name = item.Name;
                result.Description = item.Description;
                result.Price = item.Price;
                result.CategoryId = item.CategoryId;
                result.Quantaty = item.Quantaty;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
