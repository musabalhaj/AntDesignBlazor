using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartments();

        Task<Department> GetDepartment(int id);

        Task<Department> CreateDepartment(Department newDepartment);

        Task<Department> UpdateDepartment(Department updatedDepartment);

        Task DeleteDepartment(int Id);
    }

}
