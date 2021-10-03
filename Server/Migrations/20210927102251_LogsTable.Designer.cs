﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Server.Models;

namespace Project.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210927102251_LogsTable")]
    partial class LogsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Project.Shared.Models.Artical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "John Doe",
                            Body = "Lorem ipsum represents a long-held tradition for designers, typographers and the like. Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                            CategoryId = 1,
                            Image = "images/Artical1.jpg",
                            Title = "Lorem ipsum represents a long-held tradition for designers"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Adam Jones",
                            Body = "Lorem ipsum represents a long-held tradition for designers, typographers and the like. Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                            CategoryId = 2,
                            Image = "images/Artical2.jpg",
                            Title = "Lorem ipsum represents a long-held tradition for designers"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Jonathan Burke Jr.",
                            Body = "Lorem ipsum represents a long-held tradition for designers, typographers and the like. Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                            CategoryId = 3,
                            Image = "images/Artical3.jpg",
                            Title = "Lorem ipsum represents a long-held tradition for designers"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Jay White",
                            Body = "Lorem ipsum represents a long-held tradition for designers, typographers and the like. Some people hate it and argue for its demise, but others ignore the hate as they create awesome tools to help create filler text for everyone from bacon lovers to Charlie Sheen fans.",
                            CategoryId = 4,
                            Image = "images/Artical4.jpg",
                            Title = "Lorem ipsum represents a long-held tradition for designers"
                        });
                });

            modelBuilder.Entity("Project.Shared.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Category 1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Category 2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Category 3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Category 4"
                        });
                });

            modelBuilder.Entity("Project.Shared.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            DepartmentName = "Department 1"
                        },
                        new
                        {
                            DepartmentId = 2,
                            DepartmentName = "Department 2"
                        },
                        new
                        {
                            DepartmentId = 3,
                            DepartmentName = "Department 3"
                        },
                        new
                        {
                            DepartmentId = 4,
                            DepartmentName = "Department 4"
                        });
                });

            modelBuilder.Entity("Project.Shared.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            DateOfBirth = new DateTime(1980, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            Email = "JohnDoe@gmail.com",
                            FirstName = "John",
                            Gender = 0,
                            LastName = "Doe",
                            PhotoPath = "images/user2.jpg"
                        },
                        new
                        {
                            EmployeeId = 2,
                            DateOfBirth = new DateTime(1980, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            Email = "Mcintire@gmail.com",
                            FirstName = "Nina",
                            Gender = 1,
                            LastName = "Mcintire",
                            PhotoPath = "images/user1.jpg"
                        },
                        new
                        {
                            EmployeeId = 3,
                            DateOfBirth = new DateTime(1997, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 3,
                            Email = "musab1marly@gmail.com",
                            FirstName = "Musab",
                            Gender = 0,
                            LastName = "Marly",
                            PhotoPath = "images/user4.jpg"
                        },
                        new
                        {
                            EmployeeId = 4,
                            DateOfBirth = new DateTime(1999, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 4,
                            Email = "Sarah@gmail.com",
                            FirstName = "Sarah",
                            Gender = 1,
                            LastName = "Ross",
                            PhotoPath = "images/user3.jpg"
                        });
                });

            modelBuilder.Entity("Project.Shared.Models.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Project.Shared.Models.Artical", b =>
                {
                    b.HasOne("Project.Shared.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project.Shared.Models.Employee", b =>
                {
                    b.HasOne("Project.Shared.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });
#pragma warning restore 612, 618
        }
    }
}
