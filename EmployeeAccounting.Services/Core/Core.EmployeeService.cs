using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService : CoreService, IEmployeeService
    {
        public EmployeeService(IUnitOfWork db) : base(db)
        {

        }

        #region Interfaces realisation

        public async Task<IEnumerable<UIModel.Employee>> GetEmployeesAsync()
        {
            List<UIModel.Employee> employees = new List<UIModel.Employee>();

            await Task.Run(() =>
            {
                IEnumerable<DbModel.Employee> employeesDb = _db.Employees.GetAsync().Result.Include(x => x.Department).AsEnumerable();

                foreach (DbModel.Employee employeeDb in employeesDb)
                {
                    employees.Add(CreateUiEmployeeByDbEmployee(employeeDb));
                }
            });

            return employees;
        }

        public async Task<UIModel.Employee> GetEmployeeAsync(int id)
        {
            await Task.Run(() =>
            {
                DbModel.Employee employeeDb = _db.Employees.GetAsync(id).Result;

                if (employeeDb != null)
                {
                    return CreateUiEmployeeByDbEmployee(employeeDb); ;
                }

                return null;
            });

            return null;
        }

        public async Task<UIModel.Employee> AddNewEmployeeAsync(UIModel.Employee employee)
        {
            DbModel.Employee employeeDb = _db.Employees.GetAsync(employee.ID).Result;

            if (employeeDb == null)
            {
                DbModel.Employee newEmployeeDb = _db.Employees.AddAsync(CreateDbEmployeeByUiEmployee(employee)).Result;

                await _db.SaveAsync();
                return CreateUiEmployeeByDbEmployee(newEmployeeDb);
            };

            return null;
        }

        public async Task<UIModel.Employee> UpdateEmployeeAsync(int id, UIModel.Employee employee)
        {
            DbModel.Employee employeeDb = _db.Employees.GetAsync(id).Result;

            if (employeeDb != null)
            {
                employeeDb.Surname = employee.Surname;
                employeeDb.Name = employee.Name;
                employeeDb.Patronymic = employee.Patronymic;
                employeeDb.IsDeleted = employee.IsDeleted;
                employeeDb.DepartmentID = employee.DepartmentID;

                DbModel.Employee updatedEmployeeDb = await _db.Employees.UpdateAsync(employeeDb);
                await _db.SaveAsync();

                return CreateUiEmployeeByDbEmployee(updatedEmployeeDb);
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.GetAsync(id).Result;

            if (employeeDb != null)
            {
                employeeDb.IsDeleted = true;
                await _db.Employees.UpdateAsync(id);
                await _db.SaveAsync();
            }
        }

        public async Task DeleteAsync(UIModel.Employee employee)
        {
            await DeleteAsync(employee.ID);
        }

        public async Task FullDeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.GetAsync(id).Result;

            if (employeeDb != null)
            {
                _db.Employees.DeleteAsync(id);
                await _db.SaveAsync();
            }
        }

        #endregion

        #region Helpers

        private UIModel.Employee CreateUiEmployeeByDbEmployee(DbModel.Employee employeeDb)
        {
            return new UIModel.Employee
            {
                ID = employeeDb.ID,
                Surname = employeeDb.Surname,
                Name = employeeDb.Name,
                Patronymic = employeeDb.Patronymic,
                IsDeleted = employeeDb.IsDeleted,
                DepartmentID = employeeDb.DepartmentID,
                DepartmentSignature = employeeDb.Department.Signature
            };
        }

        private DbModel.Employee CreateDbEmployeeByUiEmployee(UIModel.Employee employeeUi)
        {
            return new DbModel.Employee
            {
                Surname = employeeUi.Surname,
                Name = employeeUi.Name,
                Patronymic = employeeUi.Patronymic,
                IsDeleted = employeeUi.IsDeleted,
                DepartmentID = employeeUi.DepartmentID
            };
        }

        #endregion


    }
}