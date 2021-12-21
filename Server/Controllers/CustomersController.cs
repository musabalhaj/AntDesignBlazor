using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Server.Models;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomers()
        {
            try
            {
                return Ok(await customerRepository.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int Id)
        {
            try
            {
                var result = await customerRepository.GetCustomer(Id);
                if (result == null)
                {
                    return NotFound("Sorry, Customer Not Found.");
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var emp = await customerRepository.GetCustomerByEmail(customer.Email);
                if (emp != null)
                {
                    return BadRequest($"Email '{customer.Email}' is Allready in use");
                }
                var CreatedCustomer = await customerRepository.AddCustomer(customer);

                return CreatedAtAction(nameof(GetCustomer),
                                       new { id = CreatedCustomer.Id }, CreatedCustomer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error Adding Data To The Database.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer customer)
        {
            try
            {
                var customerToUpdate = await customerRepository.GetCustomer(customer.Id);

                if (customerToUpdate == null)
                {
                    return NotFound("Sorry, Customer Not Found.");
                }

                return await customerRepository.UpdateCustomer(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating Data.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                var customerToDelete = await customerRepository.GetCustomer(id);
                if (customerToDelete == null)
                {
                    return NotFound("Sorry, Customer Not Found.");
                }
                return await customerRepository.DeleteCustomer(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data.");
            }
        }


    }
}
