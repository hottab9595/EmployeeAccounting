using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService<T> : CoreService<T>, IEmployeeService<UIModel.Employee> where T : UIModel.BaseModel
    {
        public EmployeeService(IUnitOfWork db) : base(db)
        {
        }

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Employee>> GetAsync() => await Task.Run(() => _db.Employees.GetAll().AsEnumerable()
            .Select(x => CreateUiEmployeeByDbEmployeeAsync(x).Result));

        //public async Task<IEnumerable<UIModel.Employee>> GetAsync()
        //{
        //    var lol = await _db.Employees.Get().Select()
        //    var employees = (await _db.Employees.GetAsync()).ToListAsync();
        //    await Task.Run(() => _db.Employees.GetAsync());
        //} 

        public async Task<UIModel.Employee> GetAsync(int id) => await Task.Run(() => CreateUiEmployeeByDbEmployeeAsync(_db.Employees.Get(id)));

        public async Task<UIModel.Employee> AddNewAsync(UIModel.Employee employee) => await Task.Run(async () =>
        {
            DbModel.Employee employeeDb = _db.Employees.Add(CreateDbEmployeeByUiEmployee(employee));
            await _db.SaveAsync();
            return await CreateUiEmployeeByDbEmployeeAsync(employeeDb);
        });

        public async Task<UIModel.Employee> UpdateAsync(int id, UIModel.Employee employee)
        {
            return await Task.Run(async () =>
            {
                DbModel.Employee employeeDb = _db.Employees.Get(id);

                if (employeeDb != null)
                {
                    employeeDb.Surname = employee.Surname;
                    employeeDb.Name = employee.Name;
                    employeeDb.Patronymic = employee.Patronymic;
                    employeeDb.IsDeleted = employee.IsDeleted;
                    employeeDb.DepartmentID = employee.DepartmentID;

                    _db.Employees.Update(employeeDb);
                    await _db.SaveAsync();
                    return await CreateUiEmployeeByDbEmployeeAsync(employeeDb);
                }

                return employee;
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.Get(id);

            if (employeeDb != null)
            {
                employeeDb.IsDeleted = true;
                _db.Employees.Update(id);
                await _db.SaveAsync();
            }
        }

        public Task DeleteAsync(UIModel.Employee t)
        {
            throw new System.NotImplementedException();
        }


        public async Task FullDeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.Get(id);

            if (employeeDb != null)
            {
                _db.Employees.Delete(id);
                await _db.SaveAsync();
            }
        }

        #endregion

        #region Helpers

        private async Task<UIModel.Employee> CreateUiEmployeeByDbEmployeeAsync(DbModel.Employee employeeDb)
        {
            return employeeDb == null ? null : new UIModel.Employee
            {
                ID = employeeDb.ID,
                Surname = employeeDb.Surname,
                Name = employeeDb.Name,
                Patronymic = employeeDb.Patronymic,
                IsDeleted = employeeDb.IsDeleted,
                DepartmentID = employeeDb.DepartmentID,
                DepartmentSignature = employeeDb.Department?.Signature ?? await Task.Run(() => _db.Departments.Get(employeeDb.DepartmentID).Signature)
            };
        }

        private DbModel.Employee CreateDbEmployeeByUiEmployee(UIModel.Employee employeeUi)
        {
            return employeeUi == null ? null : new DbModel.Employee
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