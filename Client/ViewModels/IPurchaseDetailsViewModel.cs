using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public interface IPurchaseDetailsViewModel
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }
        public Purchases Purchases { get; set; } 
        public string Item { get; set; }

        public string Price { get; set; }
        public string Quantaty { get; set; }

        public List<PurchaseDetails> PurchaseDetails { get; set; }

        Task<IEnumerable<PurchaseDetails>> GetPurchaseDetails();
        Task<PurchaseDetails> GetPurchaseDetails(int id);

        Task<IEnumerable<PurchaseDetails>> GetPurchaseDetailsByPurchaseId(int id);
        Task<PurchaseDetails> CreatePurchaseDetails(PurchaseDetails newPurchaseDetails);
        Task<PurchaseDetails> UpdatePurchaseDetails(PurchaseDetails updatedPurchaseDetails);
        Task<PurchaseDetails> DeletePurchaseDetails(int id);

        public void HandleSuccessCreate();
        public void HandleSuccessUpdate();
        public void HandleSuccessDelete();
    }
}
