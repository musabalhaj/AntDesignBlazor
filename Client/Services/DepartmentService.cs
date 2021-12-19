using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Department> CreateDepartment(Department newDepartment)
        {
            var department = await httpClient.PostAsJsonAsync<Department>("/api/Departments", newDepartment, null);
            if (department.IsSuccessStatusCode)
            {
                return newDepartment;
            }
            return null;
        }

        public async Task DeleteDepartment(int Id)
        {
            await httpClient.DeleteAsync($"/api/Departments/{Id}");
        }

        public async Task<Department> GetDepartment(int Id)
        {
            return await httpClient.GetFromJsonAsync<Department>($"/api/Departments/{Id}");
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Department>>("/api/Departments");
        }

        public async Task<Department> UpdateDepartment(Department updatedDepartment)
        {
            var Department = await httpClient.PutAsJsonAsync<Department>("/api/Departments", updatedDepartment, null);
            if (Department.IsSuccessStatusCode)
            {
                return updatedDepartment;
            }
            return null;
        }
    }
}
