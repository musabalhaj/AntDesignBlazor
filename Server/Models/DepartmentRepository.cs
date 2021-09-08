using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Department> AddDepartment(Department newDepartment)
        {
            var result = await context.Departments.AddAsync(newDepartment);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Department> DeleteDepartment(int Id)
        {
            var result = await context.Departments.FirstOrDefaultAsync(c => c.DepartmentId == Id);
            if (result != null)
            {
                context.Departments.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await context.Departments.FirstOrDefaultAsync(e => e.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await context.Departments.OrderByDescending(c => c.DepartmentId).ToListAsync();
        }

        public async Task<Department> UpdateDepartment(Department updatedDepartment)
        {
            var result = await context.Departments.FirstOrDefaultAsync(c => c.DepartmentId == updatedDepartment.DepartmentId);
            if (result != null)
            {
                result.DepartmentName = updatedDepartment.DepartmentName;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
