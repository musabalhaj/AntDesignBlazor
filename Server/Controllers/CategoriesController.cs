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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                return Ok(await categoryRepository.GetCategories());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int Id)
        {
            try
            {
                var result = await categoryRepository.GetCategory(Id);
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
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                var CreatedCategory = await categoryRepository.AddCategory(category);

                return CreatedAtAction(nameof(GetCategory),
                                       new { id = CreatedCategory.Id }, CreatedCategory);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error adding data to the database. {ex}");
            }
        }


        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
        {
            try
            {
                var categoryToUpdate = await categoryRepository.GetCategory(category.Id);

                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with the Id = {category.Id} Not Found");
                }

                return await categoryRepository.UpdateCategory(category);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data. {ex}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            try
            {
                var categoryToDelete = await categoryRepository.GetCategory(id);
                if (categoryToDelete == null)
                {
                    return NotFound($"Category with the Id = {id} Not Found");
                }
                return await categoryRepository.DeleteCategory(id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Deleting data. {ex}");
            }
        }
    }
}
