using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetLogs();
        Task<Log> GetLog(int departmentId);

        Task<Log> AddLog(Log newLog);
        Task<Log> UpdateLog(Log updatedLog);

        Task<Log> DeleteLog(int Id);
    }
}
