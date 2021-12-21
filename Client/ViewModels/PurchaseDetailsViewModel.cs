using AntDesign;
using Microsoft.AspNetCore.Components;
using Project.Client.Services;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public class PurchaseDetailsViewModel : IPurchaseDetailsViewModel
    {
        public int Id { get; set; }

        public int PurchasesId { get; set; }
        public Purchases Purchases { get; set; }
        public string Item { get; set; }

        public string Price { get; set; }
        public string Quantaty { get; set; }

        public PurchaseDetailsViewModel() { }

        private readonly HttpClient httpClient;
        private readonly ModalService modalService;
        private readonly NavigationManager navigationManager;
        private readonly StatusCodeService statusCodeService;
        public PurchaseDetailsViewModel(HttpClient httpClient, ModalService modalService,
                                 NavigationManager navigationManager, StatusCodeService statusCodeService)
        {
            this.httpClient = httpClient;
            this.modalService = modalService;
            this.navigationManager = navigationManager;
            this.statusCodeService = statusCodeService;
        }

        public async Task<PurchaseDetails> CreatePurchaseDetails(PurchaseDetails newPurchaseDetails)
        {
            var PurchaseDetails = await httpClient.PostAsJsonAsync("/api/PurchaseDetails", newPurchaseDetails);
            if (PurchaseDetails.IsSuccessStatusCode)
            {
                return await PurchaseDetails.Content.ReadFromJsonAsync<PurchaseDetails>();
            }
            await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;
        }
        public async Task<PurchaseDetails> DeletePurchaseDetails(int id)
        {
            var PurchaseDetails = await httpClient.DeleteAsync($"/api/PurchaseDetails/{id}");
            if (PurchaseDetails.IsSuccessStatusCode)
            {
                return await PurchaseDetails.Content.ReadFromJsonAsync<PurchaseDetails>();
            }
            await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;
        }

        public async Task<PurchaseDetails> GetPurchaseDetails(int id)
        {
            var PurchaseDetails = await httpClient.GetAsync($"/api/PurchaseDetails/{id}");
            if (PurchaseDetails.IsSuccessStatusCode)
            {
                return await PurchaseDetails.Content.ReadFromJsonAsync<PurchaseDetails>();
            }
            await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;

        }

        public async Task<IEnumerable<PurchaseDetails>> GetPurchaseDetailsByPurchaseId(int id)
        {
            var PurchaseDetails = await httpClient.GetAsync($"/api/PurchaseDetails/GetPurchaseDetailsByPurchaseId/{id}");
            if (PurchaseDetails.IsSuccessStatusCode)
            {
                var purchases = await PurchaseDetails.Content.ReadFromJsonAsync<IEnumerable<PurchaseDetails>>();
                return LoadCurrentObject(purchases.ToList());
            }
            await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;

        }

        public async Task<IEnumerable<PurchaseDetails>> GetPurchaseDetails()
        {
            var PurchaseDetails = await httpClient.GetAsync("/api/PurchaseDetails");
            if (PurchaseDetails.IsSuccessStatusCode)
            {
                var purchases = await PurchaseDetails.Content.ReadFromJsonAsync<IEnumerable<PurchaseDetails>>();
                return LoadCurrentObject(purchases.ToList());
            }
            await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;
        }

        public async Task<PurchaseDetails> UpdatePurchaseDetails(PurchaseDetails updatedPurchaseDetails)
        {
            var PurchaseDetails = await httpClient.PutAsJsonAsync<PurchaseDetails>("/api/PurchaseDetails", updatedPurchaseDetails);
            if (PurchaseDetails.IsSuccessStatusCode)
                return await PurchaseDetails.Content.ReadFromJsonAsync<PurchaseDetails>();
            else
                await statusCodeService.HandleStatusCode(PurchaseDetails);
            return null;
        }

        public void HandleSuccessCreate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "PurchaseDetails Added Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/purchases");
        }

        public void HandleSuccessUpdate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "PurchaseDetails Updated Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/purchases");
        }

        public void HandleSuccessDelete()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "PurchaseDetails Deleted Successfully.",
                OnOk = OnOkClick
            });
        }

        public Func<ModalClosingEventArgs, Task> OnOkClick = async (e) => { await Task.Delay(1000); };



        public static implicit operator PurchaseDetailsViewModel(PurchaseDetails purchaseDetails)
        {
            return new PurchaseDetailsViewModel
            {
                Id = purchaseDetails.Id,
                Price = purchaseDetails.Price,
                Quantaty = purchaseDetails.Quantaty,
                PurchasesId = purchaseDetails.PurchasesId,
                Purchases = purchaseDetails.Purchases,
                Item = purchaseDetails.Item,

            };
        }

        public static implicit operator PurchaseDetails(PurchaseDetailsViewModel purchaseDetailsVeiwModel)
        {
            return new PurchaseDetails
            {
                Id = purchaseDetailsVeiwModel.Id,
                Price = purchaseDetailsVeiwModel.Price,
                Quantaty = purchaseDetailsVeiwModel.Quantaty,
                PurchasesId = purchaseDetailsVeiwModel.PurchasesId,
                Purchases = purchaseDetailsVeiwModel.Purchases,
                Item = purchaseDetailsVeiwModel.Item,
            };
        }


        public List<PurchaseDetails> PurchaseDetails { get; set; } = new List<PurchaseDetails>();
        private IEnumerable<PurchaseDetails> LoadCurrentObject(List<PurchaseDetails> PurchaseDetails)
        {
            this.PurchaseDetails = new List<PurchaseDetails>();
            foreach (PurchaseDetails purchases in PurchaseDetails)
            {
                this.PurchaseDetails.Add(purchases);
            }
            return this.PurchaseDetails;
        }
    }
}
