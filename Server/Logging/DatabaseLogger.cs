using Microsoft.Extensions.Logging;
using Project.Server.Models;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly ApplicationDbContext applicationDbContext;

        public DatabaseLogger(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Log log = new();
            log.LogLevel = logLevel.ToString();
            log.EventName = eventId.Name;
            log.ExceptionMessage = exception?.Message;
            log.StackTrace = exception?.StackTrace;
            log.Source = "Server Side";
            log.CreatedDate = DateTime.Now.ToString();

            applicationDbContext.Logs.Add(log);
            applicationDbContext.SaveChanges();
        }
    }
}
