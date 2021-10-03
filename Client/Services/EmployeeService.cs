using Microsoft.AspNetCore.Components;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public EmployeeService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            // var Employee = await httpClient.PostAsJsonAsync<Employee>("/api/Employees", newEmployee);
            // return await Employee.Content.ReadFromJsonAsync<Employee>();
            var Employee = await httpClient.PostAsJsonAsync<Employee>("/api/Employees", newEmployee);
            if (Employee.IsSuccessStatusCode)         
                return await Employee.Content.ReadFromJsonAsync<Employee>();           
            else
                HandleStatusCode(Employee);
            return null;
        }
        public async Task DeleteEmployee(int id)
        {
            await httpClient.DeleteAsync($"/api/Employees/{id}");
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var response = await httpClient.GetAsync($"/api/Employees/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                var Employee = JsonSerializer.Deserialize<Employee>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return Employee;        
            }
            HandleStatusCode(response);
            return null;

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Employee>>("/api/Employees");
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var Employee = await httpClient.PutAsJsonAsync<Employee>("/api/Employees", updatedEmployee);
            if (Employee.IsSuccessStatusCode)
                return await Employee.Content.ReadFromJsonAsync<Employee>();
            else
                HandleStatusCode(Employee);
            return null;
        }

        public void HandleStatusCode(HttpResponseMessage responseMessage)
        {
            var statusCode = responseMessage.StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    navigationManager.NavigateTo("/error400");
                    break;
                case HttpStatusCode.NotFound:
                    navigationManager.NavigateTo("/error404");
                    break;
                case HttpStatusCode.InternalServerError:
                    navigationManager.NavigateTo("/error500");
                    break;
                default:
                    navigationManager.NavigateTo("/error400");
                    break;
            }
        }
    }
}
