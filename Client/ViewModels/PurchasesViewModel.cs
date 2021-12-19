using AntDesign;
using Microsoft.AspNetCore.Components;
using Project.Client.Services;
using Project.Shared.CustomValidation;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public class PurchasesViewModel : IPurchasesViewModel
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

        public PurchasesViewModel() { }

        private readonly HttpClient httpClient;
        private readonly ModalService modalService;
        private readonly NavigationManager navigationManager;
        private readonly StatusCodeService statusCodeService;
        public PurchasesViewModel(HttpClient httpClient, ModalService modalService,
                                 NavigationManager navigationManager, StatusCodeService statusCodeService)
        {
            this.httpClient = httpClient;
            this.modalService = modalService;
            this.navigationManager = navigationManager;
            this.statusCodeService = statusCodeService;
        }

        public async Task<Purchases> CreatePurchases(Purchases newPurchases)
        {
            var Purchases = await httpClient.PostAsJsonAsync("/api/Purchases", newPurchases);
            if (Purchases.IsSuccessStatusCode)
            {
                return await Purchases.Content.ReadFromJsonAsync<Purchases>();
            }
            await statusCodeService.HandleStatusCode(Purchases);
            return null;
        }
        public async Task<Purchases> DeletePurchases(int id)
        {
            var Purchases = await httpClient.DeleteAsync($"/api/Purchases/{id}");
            if (Purchases.IsSuccessStatusCode)
            {
                return await Purchases.Content.ReadFromJsonAsync<Purchases>();
            }
            await statusCodeService.HandleStatusCode(Purchases);
            return null;
        }

        public async Task<Purchases> GetPurchases(int id)
        {
            var Purchases = await httpClient.GetAsync($"/api/Purchases/{id}");
            if (Purchases.IsSuccessStatusCode)
            {
                return await Purchases.Content.ReadFromJsonAsync<Purchases>();
            }
            await statusCodeService.HandleStatusCode(Purchases);
            return null;

        }

        public async Task<IEnumerable<Purchases>> GetPurchases()
        {
            var Purchases = await httpClient.GetAsync("/api/Purchases");
            if (Purchases.IsSuccessStatusCode)
            {
                var purchases = await Purchases.Content.ReadFromJsonAsync<IEnumerable<Purchases>>();
                return LoadCurrentObject(purchases.ToList());
            }
            await statusCodeService.HandleStatusCode(Purchases);
            return null;
        }

        public async Task<Purchases> UpdatePurchases(Purchases updatedPurchases)
        {
            var Purchases = await httpClient.PutAsJsonAsync<Purchases>("/api/Purchases", updatedPurchases);
            if (Purchases.IsSuccessStatusCode)
                return await Purchases.Content.ReadFromJsonAsync<Purchases>();
            else
                await statusCodeService.HandleStatusCode(Purchases);
            return null;
        }

        public void HandleSuccessCreate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Purchases Added Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/purchases");
        }

        public void HandleSuccessUpdate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Purchases Updated Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/purchases");
        }

        public void HandleSuccessDelete()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Purchases Deleted Successfully.",
                OnOk = OnOkClick
            });
        }

        public Func<ModalClosingEventArgs, Task> OnOkClick = async (e) => { await Task.Delay(1000); };



        public static implicit operator PurchasesViewModel(Purchases purchases)
        {
            return new PurchasesViewModel
            {
                Id = purchases.Id,
                ItemName = purchases.ItemName,
                Description = purchases.Description,
                Price = purchases.Price,
                Quantaty = purchases.Quantaty,
                Date = purchases.Date,
                CategoryId = purchases.CategoryId,
                Category = purchases.Category,

            };
        }

        public static implicit operator Purchases(PurchasesViewModel purchasesVeiwModel)
        {
            return new Purchases
            {
                Id = purchasesVeiwModel.Id,
                ItemName = purchasesVeiwModel.ItemName,
                Description = purchasesVeiwModel.Description,
                Price = purchasesVeiwModel.Price,
                Quantaty = purchasesVeiwModel.Quantaty,
                Date = purchasesVeiwModel.Date,
                CategoryId = purchasesVeiwModel.CategoryId,
                Category = purchasesVeiwModel.Category,
            };
        }


        public List<Purchases> Purchases { get; set; } = new List<Purchases>();
        private IEnumerable<Purchases> LoadCurrentObject(List<Purchases> Purchases)
        {
            this.Purchases = new List<Purchases>();
            foreach (Purchases purchases in Purchases)
            {
                this.Purchases.Add(purchases);
            }
            return this.Purchases;
        }



    }
}
