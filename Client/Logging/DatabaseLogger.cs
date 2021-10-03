using Microsoft.Extensions.Logging;
using Project.Client.Services;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly HttpClient httpClient;

        public DatabaseLogger(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
            log.Source = "Client Side";
            log.CreatedDate = DateTime.Now.ToString();

            httpClient.PostAsJsonAsync<Log>("/api/Logs", log, null);
        }
    }
}
