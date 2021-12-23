using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeAccounting.Db
{
    public class Context : DbContext, IContext
    {
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEmployee> CourseEmployees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Signature = "Developers",
                    IsDeleted = false,
                    ParentId = null
                },
                new Department
                {
                    Id = 2,
                    Signature = "Administration",
                    IsDeleted = false,
                    ParentId = null
                },
                new Department
                {
                    Id = 3,
                    Signature = "HR department",
                    IsDeleted = false,
                    ParentId = 1
                }
            });

            modelBuilder.Entity<Employee>().HasData(new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Surname = "Koshel",
                    Name = "Egor",
                    Patronymic = "Viktotovich",
                    IsDeleted = false,
                    DepartmentId = 1
                },
                new Employee
                {
                    Id = 2,
                    Surname = "TestSurname",
                    Name = "TestName",
                    Patronymic = "TestPatronymic",
                    IsDeleted = false,
                    DepartmentId = 2
                },
                new Employee
                {
                    Id = 3,
                    Surname = "TestSurname1",
                    Name = "TestName1",
                    Patronymic = "TestPatronymic1",
                    IsDeleted = false,
                    DepartmentId = 3
                }
            });

            modelBuilder.Entity<Course>().HasData(new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Signature = ".NET",
                    Duration = 6,
                    IsDeleted = false
                },
                new Course
                {
                    Id = 2,
                    Signature = "Java",
                    Duration = 6,
                    IsDeleted = false
                },
                new Course
                {
                    Id = 3,
                    Signature = "SQL",
                    Duration = 1,
                    IsDeleted = false
                },
            });

            modelBuilder.Entity<CourseEmployee>().HasData(new List<CourseEmployee>
            {
                new CourseEmployee
                {
                    Id = 1,
                    CourseId = 1,
                    EmployeeId = 1
                },
                new CourseEmployee
                {
                    Id = 2,
                    CourseId = 1,
                    EmployeeId = 2
                },
            });
        }
    }
}