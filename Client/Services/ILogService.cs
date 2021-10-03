using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface ILogService
    {
        Task<IEnumerable<Log>> GetLogs();

        Task<Log> GetLog(int id);

        Task<Log> CreateLog(Log newLog);

        Task<Log> UpdateLog(Log updatedLog);

        Task DeleteLog(int Id);
    }
}
