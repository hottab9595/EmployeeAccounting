using System.Collections.Generic;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}