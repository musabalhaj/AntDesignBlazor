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
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository logRepository;

        public LogsController(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetLogs()
        {
            try
            {
                return Ok(await logRepository.GetLogs());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Retrieving data from the database. {ex}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Log>> GetLog(int Id)
        {
            try
            {
                var result = await logRepository.GetLog(Id);
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
        public async Task<ActionResult<Log>> CreateLog(Log log)
        {
            try
            {
                if (log == null)
                {
                    return BadRequest();
                }
                var CreatedLog = await logRepository.AddLog(log);

                return CreatedAtAction(nameof(GetLog),
                                       new { id = CreatedLog.LogId }, CreatedLog);
            }
            catch (Exception ex)
            { 

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error adding data to the database. {ex}");
            }
        }


        [HttpPut]
        public async Task<ActionResult<Log>> UpdateLog(Log log)
        {
            try
            {
                var logToUpdate = await logRepository.GetLog(log.LogId);

                if (logToUpdate == null)
                {
                    return NotFound($"Log with the Id = {log.LogId} Not Found");
                }

                return await logRepository.UpdateLog(log);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating data. {ex}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Log>> DeleteLog(int id)
        {
            try
            {
                var logToDelete = await logRepository.GetLog(id);
                if (logToDelete == null)
                {
                    return NotFound($"Log with the Id = {id} Not Found");
                }
                return await logRepository.DeleteLog(id);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error Deleting data. {ex}");
            }
        }
    }
}
