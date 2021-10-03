using Microsoft.Extensions.Logging;
using Project.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Logging
{
    public class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ApplicationLoggerProvider(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(applicationDbContext);
        }

        public void Dispose()
        {
        }
    }
}
