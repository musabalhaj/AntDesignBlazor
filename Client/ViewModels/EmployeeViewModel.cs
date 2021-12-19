using AntDesign;
using Microsoft.AspNetCore.Components;
using Project.Client.Services;
using Project.Shared.CustomValidation;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Project.Client.ViewModels
{
    public class EmployeeViewModel : IEmployeeViewModel
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

        private readonly HttpClient httpClient;
        private readonly ModalService modalService;
        private readonly NavigationManager navigationManager;
        private readonly StatusCodeService statusCodeService;
        public EmployeeViewModel() { }
        public EmployeeViewModel(HttpClient httpClient, ModalService modalService, 
                                 NavigationManager navigationManager, StatusCodeService statusCodeService)
        {
            this.httpClient = httpClient;
            this.modalService = modalService;
            this.navigationManager = navigationManager;
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
                var employees = await Employees.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return LoadCurrentObject(employees.ToList());
            }
            await statusCodeService.HandleStatusCode(Employees);
            return null;
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {
            var Employee = await httpClient.GetAsync($"/api/Employees/search?name={name}");
            if (Employee.IsSuccessStatusCode)
            {
                var employees = await Employee.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return LoadCurrentObject(employees.ToList());
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

        public void HandleSuccessCreate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Employee Added Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/employees");
        }

        public void HandleSuccessUpdate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Employee Updated Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/employees");
        }

        public void HandleSuccessDelete()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Employee Deleted Successfully.",
                OnOk = OnOkClick
            });
        }

        public Func<ModalClosingEventArgs, Task> OnOkClick = async (e) => { await Task.Delay(1000); };



        public static implicit operator EmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName= employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                DepartmentId = employee.DepartmentId,
                Department= employee.Department,
                Email = employee.Email,
                Gender = employee.Gender,
                PhotoPath= employee.PhotoPath,

            };
        }

        public static implicit operator Employee(EmployeeViewModel employeeVeiwModel)
        {
            return new Employee
            {
                EmployeeId = employeeVeiwModel.EmployeeId,
                FirstName = employeeVeiwModel.FirstName,
                LastName = employeeVeiwModel.LastName,
                DateOfBirth = employeeVeiwModel.DateOfBirth,
                DepartmentId = employeeVeiwModel.DepartmentId,
                Department = employeeVeiwModel.Department,
                Email = employeeVeiwModel.Email,
                Gender = employeeVeiwModel.Gender,
                PhotoPath = employeeVeiwModel.PhotoPath,
            };
        }


        public List<Employee> Employees { get; set; } = new List<Employee>();
        private IEnumerable<Employee> LoadCurrentObject(List<Employee> Employees)
        {
            this.Employees = new List<Employee>();
            foreach (Employee employee in Employees)
            {
                this.Employees.Add(employee);
            }
            return this.Employees;
        }



    }
}
