using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public interface IDepartmentVeiwModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<Department> Departments { get; set; }

        Task<IEnumerable<Department>> GetDepartments();

        Task<Department> GetDepartment(int id);

        Task<Department> CreateDepartment(Department newDepartment);

        Task<Department> UpdateDepartment(Department updatedDepartment);

        Task DeleteDepartment(int Id);

        public void HandleSuccessCreate();
        public void HandleSuccessUpdate();
        public void HandleSuccessDelete();
    }
}
