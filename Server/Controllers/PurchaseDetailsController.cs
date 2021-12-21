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
    public class PurchaseDetailsController : ControllerBase
    {
        private readonly IPurchaseDetailsRepository purchaseDetailsRepository;

        public PurchaseDetailsController(IPurchaseDetailsRepository purchaseDetailsRepository)
        {
            this.purchaseDetailsRepository = purchaseDetailsRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPurchaseDetailss()
        {
            try
            {
                return Ok(await purchaseDetailsRepository.GetPurchaseDetails());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PurchaseDetails>> GetPurchaseDetails(int Id)
        {
            try
            {
                var result = await purchaseDetailsRepository.GetPurchaseDetails(Id);
                if (result == null)
                {
                    return NotFound("Sorry, Purchase Details Not Found.");
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database.");
            }
        }

        [HttpGet("GetPurchaseDetailsByPurchaseId")]
        public async Task<ActionResult> GetPurchaseDetailsByPurchaseId(int Id)
        {
            try
            {
                return Ok(await purchaseDetailsRepository.GetPurchaseDetailsByPurchaseId(Id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDetails>> CreatePurchaseDetails(PurchaseDetails purchase)
        {
            try
            {
                if (purchase == null)
                {
                    return BadRequest();
                }
                var CreatedPurchaseDetails = await purchaseDetailsRepository.AddPurchaseDetails(purchase);

                return CreatedAtAction(nameof(GetPurchaseDetails),
                                       new { id = CreatedPurchaseDetails.Id }, CreatedPurchaseDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     $"Error Adding Data To The Database.{ex}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<PurchaseDetails>> UpdatePurchaseDetails(PurchaseDetails purchase)
        {
            try
            {
                var purchaseToUpdate = await purchaseDetailsRepository.GetPurchaseDetails(purchase.Id);

                if (purchaseToUpdate == null)
                {
                    return NotFound("Sorry, Purchase Details Not Found.");
                }

                return await purchaseDetailsRepository.UpdatePurchaseDetails(purchase);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating Data.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PurchaseDetails>> DeletePurchaseDetails(int id)
        {
            try
            {
                var purchaseToDelete = await purchaseDetailsRepository.GetPurchaseDetails(id);
                if (purchaseToDelete == null)
                {
                    return NotFound("Sorry, Purchase Details Not Found.");
                }
                return await purchaseDetailsRepository.DeletePurchaseDetails(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data.");
            }
        }


    }
}
