using Project.Shared.CustomValidation;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public interface IEmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [EmailDomainValidation(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [Display(Name = "Department")]
        [SelectValidation(SelectName = "Department")]
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }
        public Department Department { get; set; }

        public List<Employee> Employees { get; set; }

        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<Employee> CreateEmployee(Employee newEmployee);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<Employee> DeleteEmployee(int id);
        Task<IEnumerable<Employee>> Search(string name);

        public void HandleSuccessCreate();
        public void HandleSuccessUpdate();
        public void HandleSuccessDelete();
    }
}
