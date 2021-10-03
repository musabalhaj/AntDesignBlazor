using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class LogService : ILogService
    {
        private readonly HttpClient httpClient;

        public LogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Log> CreateLog(Log newLog)
        {
            var category = await httpClient.PostAsJsonAsync<Log>("/api/Logs", newLog, null);
            if (category.IsSuccessStatusCode)
            {
                return newLog;
            }
            return null;
        }

        public async Task DeleteLog(int Id)
        {
            await httpClient.DeleteAsync($"/api/Logs/{Id}");
        }

        public async Task<Log> GetLog(int Id)
        {
            return await httpClient.GetFromJsonAsync<Log>($"/api/Logs/{Id}");
        }

        public async Task<IEnumerable<Log>> GetLogs()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Log>>("/api/Logs");
        }

        public async Task<Log> UpdateLog(Log updatedLog)
        {
            var Log = await httpClient.PutAsJsonAsync<Log>("/api/Logs", updatedLog, null);
            if (Log.IsSuccessStatusCode)
            {
                return updatedLog;
            }
            return null;
        }
    }
}
