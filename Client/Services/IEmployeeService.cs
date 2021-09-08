using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> CreateEmployee(Employee newEmployee);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task DeleteEmployee(int id);
    }
}
