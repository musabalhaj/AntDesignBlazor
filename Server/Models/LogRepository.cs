using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext context;

        public LogRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Log> AddLog(Log newLog)
        {
            var result = await context.Logs.AddAsync(newLog);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Log> DeleteLog(int Id)
        {
            var result = await context.Logs.FirstOrDefaultAsync(c => c.LogId == Id);
            if (result != null)
            {
                context.Logs.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Log> GetLog(int logId)
        {
            return await context.Logs.FirstOrDefaultAsync(e => e.LogId == logId);
        }

        public async Task<IEnumerable<Log>> GetLogs()
        {
            return await context.Logs.OrderByDescending(c => c.LogId).ToListAsync();
        }

        public async Task<Log> UpdateLog(Log updatedLog)
        {
            var result = await context.Logs.FirstOrDefaultAsync(c => c.LogId == updatedLog.LogId);
            if (result != null)
            {
                result.LogLevel = updatedLog.LogLevel;
                result.EventName = updatedLog.EventName;
                result.ExceptionMessage = updatedLog.ExceptionMessage;
                result.StackTrace = updatedLog.StackTrace;
                result.Source = "Server Side";
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
