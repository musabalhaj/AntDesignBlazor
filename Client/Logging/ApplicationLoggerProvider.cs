using Microsoft.Extensions.Logging;
using Project.Client.Logging;
using Project.Client.Services;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.Client.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly HttpClient httpClient;

        public ApplicationLoggerProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(httpClient);
        }

        public void Dispose()
        {
        }
    }
}
