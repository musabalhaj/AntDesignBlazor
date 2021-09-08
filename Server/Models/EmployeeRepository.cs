using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var result = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                context.Employees.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await context.Employees
                .Include(e => e.Department)
                .OrderByDescending(e => e.EmployeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = context.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.FirstName.Contains(name) ||
                                    e.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.Email = employee.Email;
                result.DateOfBirth = employee.DateOfBirth;
                result.Gender = employee.Gender;
                result.DepartmentId = employee.DepartmentId;
                result.PhotoPath = employee.PhotoPath;
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
