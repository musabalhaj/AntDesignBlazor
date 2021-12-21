using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Customer> DeleteCustomer(int customerId)
        {
            var result = await context.Customers.FirstOrDefaultAsync(e => e.Id == customerId);
            if (result != null)
            {
                context.Customers.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await context.Customers
                .FirstOrDefaultAsync(e => e.Id == customerId);

        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await context.Customers
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await context.Customers
                .OrderByDescending(e => e.Id)
                .ToListAsync();
        }


        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var result = await context.Customers.FirstOrDefaultAsync(e => e.Id == customer.Id);
            if (result != null)
            {
                result.Name = customer.Name;
                result.Email = customer.Email;
                result.Phone = customer.Phone;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
