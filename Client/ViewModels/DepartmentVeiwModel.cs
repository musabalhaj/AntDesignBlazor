using AntDesign;
using Microsoft.AspNetCore.Components;
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
    public class DepartmentVeiwModel : IDepartmentVeiwModel
    {
        private readonly HttpClient httpClient;
        private readonly ModalService modalService;
        private readonly NavigationManager navigationManager;

        public int DepartmentId { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();

        public DepartmentVeiwModel() { }

        public DepartmentVeiwModel(HttpClient httpClient, ModalService modalService, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.modalService = modalService;
            this.navigationManager = navigationManager;
        }
        public async Task<Department> CreateDepartment(Department newDepartment)
        {
            var department = await httpClient.PostAsJsonAsync<Department>("/api/Departments", newDepartment);
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
            var departments =  await httpClient.GetFromJsonAsync<IEnumerable<Department>>("/api/Departments");
            return LoadCurrentObject(departments.ToList());
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
        public void HandleSuccessCreate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Department Added Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/departments");
        }

        public void HandleSuccessUpdate()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Department Updated Successfully.",
                OnOk = OnOkClick
            });
            navigationManager.NavigateTo("/departments");
        }

        public void HandleSuccessDelete()
        {
            modalService.Success(new ConfirmOptions()
            {
                Title = "Success.",
                Content = "Department Deleted Successfully.",
                OnOk = OnOkClick
            });
        }

        public Func<ModalClosingEventArgs, Task> OnOkClick = async (e) =>
        {
            await Task.Delay(1000);
        };

        public static implicit operator DepartmentVeiwModel(Department department)
        {
            return new DepartmentVeiwModel
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            };
        }

        public static implicit operator Department(DepartmentVeiwModel departmentVeiwModel)
        {
            return new Department
            {
                DepartmentId = departmentVeiwModel.DepartmentId,
                DepartmentName = departmentVeiwModel.DepartmentName
            };
        }

        private IEnumerable<Department> LoadCurrentObject(List<Department> Departments)
        {
            this.Departments = new List<Department>();
            foreach (Department department in Departments)
            {
                this.Departments.Add(department);
            }
            return this.Departments;
        }
    }
}
