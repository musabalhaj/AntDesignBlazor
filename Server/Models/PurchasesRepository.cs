﻿using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class PurchasesRepository : IPurchasesRepository
    {
        private readonly ApplicationDbContext context;

        public PurchasesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Purchases> AddPurchases(Purchases purchase)
        {
            var result = await context.Purchases.AddAsync(purchase);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Purchases> DeletePurchases(int purchaseId)
        {
            var result = await context.Purchases.FirstOrDefaultAsync(e => e.Id == purchaseId);
            if (result != null)
            {
                context.Purchases.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Purchases> GetPurchases(int purchaseId)
        {
            return await context.Purchases
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == purchaseId);

        }

        public async Task<IEnumerable<Purchases>> GetPurchases()
        {
            return await context.Purchases
                .Include(e => e.Category)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

       

        public async Task<Purchases> UpdatePurchases(Purchases purchase)
        {
            var result = await context.Purchases.FirstOrDefaultAsync(e => e.Id == purchase.Id);
            if (result != null)
            {
                result.ItemName = purchase.ItemName;
                result.Description = purchase.Description;
                result.Price = purchase.Price;
                result.Quantaty = purchase.Quantaty;
                result.Date = purchase.Date;
                result.CategoryId = purchase.CategoryId;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
