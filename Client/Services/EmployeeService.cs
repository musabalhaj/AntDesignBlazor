using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            var Employee = await httpClient.PostAsJsonAsync<Employee>("/api/Employees", newEmployee, null);
            if (Employee.IsSuccessStatusCode)
            {
                return newEmployee;
            }
            return null;
        }

        public async Task DeleteEmployee(int id)
        {
            await httpClient.DeleteAsync($"/api/Employees/{id}");
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetFromJsonAsync<Employee>($"/api/Employees/{id}");
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Employee>>("/api/Employees");
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var Employee = await httpClient.PutAsJsonAsync<Employee>("/api/Employees", updatedEmployee, null);
            if (Employee.IsSuccessStatusCode)
            {
                return updatedEmployee;
            }
            return null;
        }
    }
}
