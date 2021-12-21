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
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetItems()
        {
            try
            {
                return Ok(await itemRepository.GetItems());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> GetItem(int Id)
        {
            try
            {
                var result = await itemRepository.GetItem(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }


        [HttpGet("{GetItemByName}")]
        public async Task<ActionResult<Item>> GetItemByName(string name)
        {
            try
            {
                var result = await itemRepository.GetItemByName(name);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest();
                }
                var CreatedItem = await itemRepository.AddItem(item);

                return CreatedAtAction(nameof(GetItem),
                                       new { id = CreatedItem.Id }, CreatedItem);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error adding data to the database. {ex}");
            }
        }


        [HttpPut]
        public async Task<ActionResult<Item>> UpdateItem(Item item)
        {
            try
            {
                var itemToUpdate = await itemRepository.GetItem(item.Id);

                if (itemToUpdate == null)
                {
                    return NotFound($"Item with the Id = {item.Id} Not Found");
                }

                return await itemRepository.UpdateItem(item);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data. {ex}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            try
            {
                var itemToDelete = await itemRepository.GetItem(id);
                if (itemToDelete == null)
                {
                    return NotFound($"Item with the Id = {id} Not Found");
                }
                return await itemRepository.DeleteItem(id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Deleting data. {ex}");
            }
        }
    }
}
