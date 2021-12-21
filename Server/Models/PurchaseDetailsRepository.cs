using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class PurchaseDetailsRepository : IPurchaseDetailsRepository
    {
        private readonly ApplicationDbContext context;

        public PurchaseDetailsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<PurchaseDetails> AddPurchaseDetails(PurchaseDetails purchaseDetails)
        {
            var result = await context.PurchaseDetails.AddAsync(purchaseDetails);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PurchaseDetails> DeletePurchaseDetails(int purchaseDetailsId)
        {
            var result = await context.PurchaseDetails.FirstOrDefaultAsync(e => e.Id == purchaseDetailsId);
            if (result != null)
            {
                context.PurchaseDetails.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<PurchaseDetails> GetPurchaseDetails(int purchaseDetailsId)
        {
            return await context.PurchaseDetails
                .Include(p=>p.Purchases)
                .FirstOrDefaultAsync(e => e.Id == purchaseDetailsId);

        }

        public async Task<IEnumerable<PurchaseDetails>> GetPurchaseDetailsByPurchaseId(int purchaseId)
        {
            return await context.PurchaseDetails.Where(e=>e.PurchasesId == purchaseId)
                .Include(p => p.Purchases)
                .ToListAsync();

        }

        public async Task<IEnumerable<PurchaseDetails>> GetPurchaseDetails()
        {
            return await context.PurchaseDetails
                .Include(p => p.Purchases)
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }

       

        public async Task<PurchaseDetails> UpdatePurchaseDetails(PurchaseDetails purchaseDetails)
        {
            var result = await context.PurchaseDetails.FirstOrDefaultAsync(e => e.Id == purchaseDetails.Id);
            if (result != null)
            {
                result.PurchasesId = purchaseDetails.PurchasesId;
                result.Item = purchaseDetails.Item;
                result.Price = purchaseDetails.Price;
                result.Quantaty = purchaseDetails.Quantaty;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
