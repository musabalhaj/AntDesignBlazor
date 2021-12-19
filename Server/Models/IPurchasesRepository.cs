using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface IPurchasesRepository
    {
        Task<IEnumerable<Purchases>> GetPurchases();
        Task<Purchases> GetPurchases(int purchaseId);

        Task<Purchases> AddPurchases(Purchases purchase);
        Task<Purchases> UpdatePurchases(Purchases purchase);
        Task<Purchases> DeletePurchases(int purchaseId);
    }
}
