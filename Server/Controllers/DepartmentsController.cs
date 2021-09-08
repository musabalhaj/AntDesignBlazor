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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await departmentRepository.GetDepartments());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving data from the database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartment(int Id)
        {
            try
            {
                var result = await departmentRepository.GetDepartment(Id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving data from the database.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            try
            {
                if (department == null)
                {
                    return BadRequest();
                }
                var CreatedDepartment = await departmentRepository.AddDepartment(department);

                return CreatedAtAction(nameof(GetDepartment),
                                       new { id = CreatedDepartment.DepartmentId }, CreatedDepartment);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error adding data to the database.");
            }
        }


        [HttpPut]
        public async Task<ActionResult<Department>> UpdateDepartment(Department department)
        {
            try
            {
                var departmentToUpdate = await departmentRepository.GetDepartment(department.DepartmentId);

                if (departmentToUpdate == null)
                {
                    return NotFound($"Department with the Id = {department.DepartmentId} Not Found");
                }

                return await departmentRepository.UpdateDepartment(department);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            try
            {
                var departmentToDelete = await departmentRepository.GetDepartment(id);
                if (departmentToDelete == null)
                {
                    return NotFound($"Department with the Id = {id} Not Found");
                }
                return await departmentRepository.DeleteDepartment(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data");
            }
        }
    }
}
