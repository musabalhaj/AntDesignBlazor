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
    public class CustomerViewModel : ICustomerViewModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [EmailDomainValidation(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        private readonly HttpClient httpClient;
        private readonly ModalService modalService;
        private readonly NavigationManager navigationManager;
        private readonly StatusCodeService statusCodeService;
        public CustomerViewModel() { }
        public CustomerViewModel(HttpClient httpClient, ModalService modalService, 
                                 NavigationManager navigationManager, StatusCodeService statusCodeService)
        {
            this.httpClient = httpClient;
            this.modalService = modalService;
            this.navigationManager = navigationManager;
            this.statusCodeService = statusCodeService;
        }

        public async Task<Customer> CreateCustomer(Customer newCustomer)
        {
            var Customer = await httpClient.PostAsJsonAsync("/api/Customers", newCustomer);
            if (Customer.IsSuccessStatusCode)
            {
                return await Customer.Content.ReadFromJsonAsync<Customer>();
            }
            await statusCodeService.HandleStatusCode(Customer);
            return null;
        }
        public async Task<Customer> DeleteCustomer(int id)
        {
            var Customer = await httpClient.DeleteAsync($"/api/Customers/{id}");
            if (Customer.IsSuccessStatusCode)
            {
                return await Customer.Content.ReadFromJsonAsync<Customer>();
            }
            await statusCodeService.HandleStatusCode(Customer);
            return null;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var Customer = await httpClient.GetAsync($"/api/Customers/{id}");
            if (Customer.IsSuccessStatusCode)
            {
                return await Customer.Content.ReadFromJsonAsync<Customer>();
            }
            await statusCodeService.HandleStatusCode(Customer);
            return null;

        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var Customers = await httpClient.GetAsync("/api/Customers");
            if (Customers.IsSuccessStatusCode)
            {
                var customers = await Customers.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
                return LoadCurrentObject(customers.ToList());
            }
            await statusCodeService.HandleStatusCode(Customers);
            return null;
        }

        public async Task<Customer> UpdateCustomer(Customer updatedCustomer)
        {
            var Customer = await httpClient.PutAsJsonAsync<Customer>("/api/Customers", updatedCustomer);
            if (Customer.IsSuccessStatusCode)
                return await Customer.Content.ReadFromJsonAsync<Customer>();
            else
                await statusCodeService.HandleStatusCode(Customer);
            return null;
        }

        public void HandleSuccessCreate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Customer Added Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/customers");
        }

        public void HandleSuccessUpdate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Customer Updated Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/customers");
        }

        public void HandleSuccessDelete()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Customer Deleted Successfully.",
                OnOk = OnOkClick
            });
        }

        public Func<ModalClosingEventArgs, Task> OnOkClick = async (e) => { await Task.Delay(1000); };



        public static implicit operator CustomerViewModel(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            };
        }

        public static implicit operator Customer(CustomerViewModel customerVeiwModel)
        {
            return new Customer
            {
                Id = customerVeiwModel.Id,
                Name = customerVeiwModel.Name,
                Email = customerVeiwModel.Email,
                Phone = customerVeiwModel.Phone,
            };
        }


        public List<Customer> Customers { get; set; } = new List<Customer>();
        private IEnumerable<Customer> LoadCurrentObject(List<Customer> Customers)
        {
            this.Customers = new List<Customer>();
            foreach (Customer customer in Customers)
            {
                this.Customers.Add(customer);
            }
            return this.Customers;
        }



    }
}
