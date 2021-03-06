using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;

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
            modelBuilder.Entity<CourseEmployee>()
                .HasKey(c => new { c.CourseId, c.EmployeeId });

            modelBuilder.Entity<Department>().HasData(new List<Department>
            {
                new Department
                {
                    ID = 1,
                    Signature = "Developers",
                    IsDeleted = false,
                    ParentID = null
                },
                new Department
                {
                    ID = 2,
                    Signature = "Administration",
                    IsDeleted = false,
                    ParentID = null
                },
                new Department
                {
                    ID = 3,
                    Signature = "HR department",
                    IsDeleted = false,
                    ParentID = 1
                }
            });

            modelBuilder.Entity<Employee>().HasData(new List<Employee>
            {
                new Employee
                {
                    ID = 1,
                    Surname = "Koshel",
                    Name = "Egor",
                    Patronymic = "Viktotovich",
                    IsDeleted = false,
                    DepartmentID = 1
                },
                new Employee
                {
                    ID = 2,
                    Surname = "TestSurname",
                    Name = "TestName",
                    Patronymic = "TestPatronymic",
                    IsDeleted = false,
                    DepartmentID = 2
                },
                new Employee
                {
                    ID = 3,
                    Surname = "TestSurname1",
                    Name = "TestName1",
                    Patronymic = "TestPatronymic1",
                    IsDeleted = false,
                    DepartmentID = 3
                }
            });
            modelBuilder.Entity<Course>().HasData(new List<Course>
            {
                new Course
                {
                    ID = 1,
                    Signature = ".NET",
                    Duration = 6,
                    IsDeleted = false
                },
                new Course
                {
                    ID = 2,
                    Signature = "Java",
                    Duration = 6,
                    IsDeleted = false
                },
                new Course
                {
                    ID = 3,
                    Signature = "SQL",
                    Duration = 1,
                    IsDeleted = false
                },
            });
            modelBuilder.Entity<CourseEmployee>().HasData(new List<CourseEmployee>
            {
                new CourseEmployee
                {
                    CourseId = 1,
                    EmployeeId = 1
                },
                new CourseEmployee
                {
                    CourseId = 1,
                    EmployeeId = 2
                },
            });
        }
    }
}