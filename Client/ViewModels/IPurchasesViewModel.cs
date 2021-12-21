using Project.Shared.CustomValidation;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public interface IPurchasesViewModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public double Total { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [SelectValidation(SelectName = "Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Purchases> Purchases { get; set; }

        Task<IEnumerable<Purchases>> GetPurchases();
        Task<Purchases> GetPurchases(int id);
        Task<Purchases> CreatePurchases(Purchases newPurchases);
        Task<Purchases> UpdatePurchases(Purchases updatedPurchases);
        Task<Purchases> DeletePurchases(int id);

        public void HandleSuccessCreate();
        public void HandleSuccessUpdate();
        public void HandleSuccessDelete();
    }
}
