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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Geting Data From The Database.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int Id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(Id);
                if (result == null)
                {
                    return NotFound();
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
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Email Allready in use");
                    return BadRequest(ModelState);
                }
                var CreatedEmployee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee),
                                       new { id = CreatedEmployee.EmployeeId }, CreatedEmployee);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Adding Data To The Database.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
                var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with the Id = {employee.EmployeeId} Not Found");
                }

                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Updating Data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);
                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with the Id = {id} Not Found");
                }
                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await employeeRepository.Search(name, gender);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    "Error Geting Data From The Database");
            }
        }


    }
}
