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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Purchases> Purchases { get; set; }

        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }

        public DbSet<Log> Logs { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Category 1" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Category 2" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Category 3" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Category 4" });

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                Name = "John Doe",
                Email = "JohnDoe@gmail.com",
                Phone = "0995514212"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 2,
                Name = "Sara Doe",
                Email = "saraDoe@gmail.com",
                Phone = "0951245681"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 3,
                Name = "Fofo Doe",
                Email = "FofoDoe@gmail.com",
                Phone = "0124587452"
            });

            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 1,
                Name = "Item 1",
                Description = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Price = "90",
                Quantaty = "100",
                CategoryId = 1
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 2,
                Name = "Item 2",
                Description = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Price = "150",
                Quantaty = "1000",
                CategoryId = 2
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 3,
                Name = "Item 3",
                Description = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Price = "100",
                Quantaty = "100",
                CategoryId = 3
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Id = 4,
                Name = "Item 4",
                Description = "Lorem ipsum represents a long-held tradition for designers," +
                " typographers and the like. Some people hate it and argue for its demise," +
                " but others ignore the hate as they create awesome tools to help create filler " +
                "text for everyone from bacon lovers to Charlie Sheen fans.",
                Price = "1500",
                Quantaty = "100",
                CategoryId = 4
            });
        }
    }
}
