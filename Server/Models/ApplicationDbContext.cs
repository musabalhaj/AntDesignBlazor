using Microsoft.EntityFrameworkCore;
using Project.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Server.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Artical> Articals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 1, DepartmentName = "Department 1" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 2, DepartmentName = "Department 2" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 3, DepartmentName = "Department 3" });
            modelBuilder.Entity<Department>().HasData(new Department { DepartmentId = 4, DepartmentName = "Department 4" });

            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Category 1" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Category 2" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Category 3" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Category 4" });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@gmail.com",
                DateOfBirth = new DateTime(1980, 10, 24),
                Gender = Gender.Male,
                DepartmentId = 1,
                PhotoPath = "images/user2.jpg"
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                FirstName = "Nina",
                LastName = "Mcintire",
                Email = "Mcintire@gmail.com",
                DateOfBirth = new DateTime(1980, 10, 24),
                Gender = Gender.Female,
                DepartmentId = 2,
                PhotoPath = "images/user1.jpg"
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 3,
                FirstName = "Musab",
                LastName = "Marly",
                Email = "musab1marly@gmail.com",
                DateOfBirth = new DateTime(1997, 1, 23),
                Gender = Gender.Male,
                DepartmentId = 3,
                PhotoPath = "images/user4.jpg"
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 4,
                FirstName = "Sarah",
                LastName = "Ross",
                Email = "Sarah@gmail.com",
                DateOfBirth = new DateTime(1999, 10, 23),
                Gender = Gender.Female,
                DepartmentId = 4,
                PhotoPath = "images/user3.jpg"
            });

            modelBuilder.Entity<Artical>().HasData(new Artical
            {
                Id = 1,
                Author = "John Doe",
                Title = "Lorem ipsum represents a long-held tradition for designers",
                Body = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Image = "images/Artical1.jpg",
                CategoryId = 1
            });
            modelBuilder.Entity<Artical>().HasData(new Artical
            {
                Id = 2,
                Author = "Adam Jones",
                Title = "Lorem ipsum represents a long-held tradition for designers",
                Body = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Image = "images/Artical2.jpg",
                CategoryId = 2
            });
            modelBuilder.Entity<Artical>().HasData(new Artical
            {
                Id = 3,
                Author = "Jonathan Burke Jr.",
                Title = "Lorem ipsum represents a long-held tradition for designers",
                Body = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Image = "images/Artical3.jpg",
                CategoryId = 3
            });
            modelBuilder.Entity<Artical>().HasData(new Artical
            {
                Id = 4,
                Author = "Jay White",
                Title = "Lorem ipsum represents a long-held tradition for designers",
                Body = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Image = "images/Artical4.jpg",
                CategoryId = 4
            });
        }
    }
}
