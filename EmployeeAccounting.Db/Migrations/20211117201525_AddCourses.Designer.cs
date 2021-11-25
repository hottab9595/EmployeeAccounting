﻿// <auto-generated />
using System;
using EmployeeAccounting.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeAccounting.Db.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20211117201525_AddCourses")]
    partial class AddCourses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseEmployee", b =>
                {
                    b.Property<int>("CoursesID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeesID")
                        .HasColumnType("int");

                    b.HasKey("CoursesID", "EmployeesID");

                    b.HasIndex("EmployeesID");

                    b.ToTable("CourseEmployee");
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Duration = 6,
                            IsDeleted = false,
                            Signature = ".NET"
                        },
                        new
                        {
                            ID = 2,
                            Duration = 6,
                            IsDeleted = false,
                            Signature = "Java"
                        },
                        new
                        {
                            ID = 3,
                            Duration = 1,
                            IsDeleted = false,
                            Signature = "SQL"
                        });
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentID")
                        .HasColumnType("int");

                    b.Property<string>("Signature")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsDeleted = false,
                            Signature = "Developers"
                        },
                        new
                        {
                            ID = 2,
                            IsDeleted = false,
                            Signature = "Administration"
                        },
                        new
                        {
                            ID = 3,
                            IsDeleted = false,
                            ParentID = 1,
                            Signature = "HR department"
                        });
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DepartmentID = 1,
                            IsDeleted = false,
                            Name = "Egor",
                            Patronymic = "Viktotovich",
                            Surname = "Koshel"
                        },
                        new
                        {
                            ID = 2,
                            DepartmentID = 2,
                            IsDeleted = false,
                            Name = "TestName",
                            Patronymic = "TestPatronymic",
                            Surname = "TestSurname"
                        },
                        new
                        {
                            ID = 3,
                            DepartmentID = 3,
                            IsDeleted = false,
                            Name = "TestName1",
                            Patronymic = "TestPatronymic1",
                            Surname = "TestSurname1"
                        });
                });

            modelBuilder.Entity("CourseEmployee", b =>
                {
                    b.HasOne("EmployeeAccounting.Db.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeAccounting.Db.Model.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Department", b =>
                {
                    b.HasOne("EmployeeAccounting.Db.Model.Department", "Parent")
                        .WithMany("Departments")
                        .HasForeignKey("ParentID");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Employee", b =>
                {
                    b.HasOne("EmployeeAccounting.Db.Model.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("EmployeeAccounting.Db.Model.Department", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}