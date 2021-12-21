using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface IPurchaseDetailsRepository
    {
        Task<IEnumerable<PurchaseDetails>> GetPurchaseDetails();
        Task<PurchaseDetails> GetPurchaseDetails(int purchaseDetailsId);

        Task<IEnumerable<PurchaseDetails>> GetPurchaseDetailsByPurchaseId(int purchaseId);
        Task<PurchaseDetails> AddPurchaseDetails(PurchaseDetails purchaseDetails);
        Task<PurchaseDetails> UpdatePurchaseDetails(PurchaseDetails purchaseDetails);
        Task<PurchaseDetails> DeletePurchaseDetails(int purchaseDetailsId);
    }
}
