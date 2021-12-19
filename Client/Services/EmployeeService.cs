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
        private readonly StatusCodeService statusCodeService;

        public EmployeeService(HttpClient httpClient, StatusCodeService statusCodeService)
        {
            this.httpClient = httpClient;
            this.statusCodeService = statusCodeService;
        }
        public async Task<Employee> CreateEmployee(Employee newEmployee)
        {
            var Employee = await httpClient.PostAsJsonAsync("/api/Employees", newEmployee);
            if (Employee.IsSuccessStatusCode)
            {
                return await Employee.Content.ReadFromJsonAsync<Employee>();
            }
            await statusCodeService.HandleStatusCode(Employee);
            return null;
        }
        public async Task<Employee> DeleteEmployee(int id)
        {
            var Employee = await httpClient.DeleteAsync($"/api/Employees/{id}");
            if (Employee.IsSuccessStatusCode)
            {
                return await Employee.Content.ReadFromJsonAsync<Employee>();
            }
            await statusCodeService.HandleStatusCode(Employee);
            return null;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var Employee = await httpClient.GetAsync($"/api/Employees/{id}");
            if (Employee.IsSuccessStatusCode)
            {
                return await Employee.Content.ReadFromJsonAsync<Employee>();
            }
            await statusCodeService.HandleStatusCode(Employee);
            return null;

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var Employees = await httpClient.GetAsync("/api/Employees");
            if (Employees.IsSuccessStatusCode)
            {
                return await Employees.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
            }
            await statusCodeService.HandleStatusCode(Employees);
            return null;
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {
            var Employee = await httpClient.GetAsync($"/api/Employees/search?name={name}");
            if (Employee.IsSuccessStatusCode)
            {
                return await Employee.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
            }
            await statusCodeService.HandleStatusCode(Employee);
            return null;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var Employee = await httpClient.PutAsJsonAsync<Employee>("/api/Employees", updatedEmployee);
            if (Employee.IsSuccessStatusCode)
                return await Employee.Content.ReadFromJsonAsync<Employee>();
            else
                await statusCodeService.HandleStatusCode(Employee);
            return null;
        }

    }
}
