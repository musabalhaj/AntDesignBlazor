using Project.Shared.CustomValidation;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public interface ICustomerViewModel
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

        public List<Customer> Customers { get; set; }

        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> CreateCustomer(Customer newCustomer);
        Task<Customer> UpdateCustomer(Customer updatedCustomer);
        Task<Customer> DeleteCustomer(int id);

        public void HandleSuccessCreate();
        public void HandleSuccessUpdate();
        public void HandleSuccessDelete();
    }
}
