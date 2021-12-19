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
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesRepository purchasesRepository;

        public PurchasesController(IPurchasesRepository purchasesRepository)
        {
            this.purchasesRepository = purchasesRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPurchasess()
        {
            try
            {
                return Ok(await purchasesRepository.GetPurchases());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Purchases>> GetPurchases(int Id)
        {
            try
            {
                var result = await purchasesRepository.GetPurchases(Id);
                if (result == null)
                {
                    return NotFound("Sorry, Purchase Not Found.");
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
        public async Task<ActionResult<Purchases>> CreatePurchases(Purchases purchase)
        {
            try
            {
                if (purchase == null)
                {
                    return BadRequest();
                }
                var CreatedPurchases = await purchasesRepository.AddPurchases(purchase);

                return CreatedAtAction(nameof(GetPurchases),
                                       new { id = CreatedPurchases.Id }, CreatedPurchases);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Error Adding Data To The Database.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Purchases>> UpdatePurchases(Purchases purchase)
        {
            try
            {
                var purchaseToUpdate = await purchasesRepository.GetPurchases(purchase.Id);

                if (purchaseToUpdate == null)
                {
                    return NotFound("Sorry, Purchase Not Found.");
                }

                return await purchasesRepository.UpdatePurchases(purchase);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating Data.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Purchases>> DeletePurchases(int id)
        {
            try
            {
                var purchaseToDelete = await purchasesRepository.GetPurchases(id);
                if (purchaseToDelete == null)
                {
                    return NotFound("Sorry, Purchase Not Found.");
                }
                return await purchasesRepository.DeletePurchases(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data.");
            }
        }


    }
}
