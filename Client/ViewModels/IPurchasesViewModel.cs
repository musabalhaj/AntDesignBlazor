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
        [MinLength(2)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [MinLength(2)]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Quantaty { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Category")]
        [SelectValidation(SelectName = "Cateogry")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

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
