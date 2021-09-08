using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartments();
        Task<Department> GetDepartment(int departmentId);

        Task<Department> AddDepartment(Department newDepartment);
        Task<Department> UpdateDepartment(Department updatedDepartment);

        Task<Department> DeleteDepartment(int Id);
    }
}
