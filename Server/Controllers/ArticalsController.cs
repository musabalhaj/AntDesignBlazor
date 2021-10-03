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
    public class ArticalsController : ControllerBase
    {
        private readonly IArticalRepository articalRepository;

        public ArticalsController(IArticalRepository articalRepository)
        {
            this.articalRepository = articalRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetArticals()
        {
            try
            {
                return Ok(await articalRepository.GetArticals());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Artical>> GetArtical(int Id)
        {
            try
            {
                var result = await articalRepository.GetArtical(Id);
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
        public async Task<ActionResult<Artical>> CreateArtical(Artical artical)
        {
            try
            {
                if (artical == null)
                {
                    return BadRequest();
                }
                var CreatedArtical = await articalRepository.AddArtical(artical);

                return CreatedAtAction(nameof(GetArtical),
                                       new { id = CreatedArtical.Id }, CreatedArtical);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error adding data to the database. {ex}");
            }
        }


        [HttpPut]
        public async Task<ActionResult<Artical>> UpdateArtical(Artical artical)
        {
            try
            {
                var articalToUpdate = await articalRepository.GetArtical(artical.Id);

                if (articalToUpdate == null)
                {
                    return NotFound($"Artical with the Id = {artical.Id} Not Found");
                }

                return await articalRepository.UpdateArtical(artical);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data. {ex}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Artical>> DeleteArtical(int id)
        {
            try
            {
                var articalToDelete = await articalRepository.GetArtical(id);
                if (articalToDelete == null)
                {
                    return NotFound($"Artical with the Id = {id} Not Found");
                }
                return await articalRepository.DeleteArtical(id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Deleting data. {ex}");
            }
        }
    }
}
