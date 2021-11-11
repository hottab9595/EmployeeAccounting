using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService : CoreService<UIModel.Employee>
    {
        public EmployeeService(IUnitOfWork db) : base(db)
        {

        }

        #region Interfaces realization

        //public async Task<IEnumerable<UIModel.Employee>> GetAsync() => await Task.Run(() => _db.Employees.GetAsync().Result.AsEnumerable()
        //    .Select(x => CreateUiEmployeeByDbEmployeeAsync(x).Result));

        public async Task<IEnumerable<UIModel.Employee>> GetAsync()
        {
            await Task.Run(() => _db.Employees.GetAsync().Result.AsEnumerable()
                .Select(x => CreateUiEmployeeByDbEmployeeAsync(x).Result));

            var employeeDb = _db.Employees.GetAsync().Result.AsEnumerable();
            var lol = new List<UIModel.Employee>();
            foreach (var result in employeeDb)
            {
                lol.Add(new UIModel.Employee
                {
                    ID = result.ID,
                    Surname = result.Surname,
                    Name = result.Name,
                    Patronymic = result.Patronymic,
                    IsDeleted = result.IsDeleted,
                    DepartmentID = result.DepartmentID,
                    DepartmentSignature = result.Department.Signature
                });
            }

            return lol;
            //return new UIModel.Employee
            //{
            //    ID = employeeDb.ID,
            //    Surname = employeeDb.Surname,
            //    Name = employeeDb.Name,
            //    Patronymic = employeeDb.Patronymic,
            //    IsDeleted = employeeDb.IsDeleted,
            //    DepartmentID = employeeDb.DepartmentID,
            //    DepartmentSignature = employeeDb.Department.Signature
            //};
        }

        public async Task<UIModel.Employee> GetAsync(int id) => await Task.Run(() => CreateUiEmployeeByDbEmployeeAsync(_db.Employees.GetAsync(id).Result));

        public async Task<UIModel.Employee> AddNewAsync(UIModel.Employee employee) => await Task.Run(async () =>
        {
            DbModel.Employee employeeDb = await _db.Employees.AddAsync(CreateDbEmployeeByUiEmployee(employee));
            await _db.SaveAsync();
            return await CreateUiEmployeeByDbEmployeeAsync(employeeDb);
        });

        public async Task<UIModel.Employee> UpdateAsync(int id, UIModel.Employee employee)
        {
            return await Task.Run(async () =>
            {
                DbModel.Employee employeeDb = await _db.Employees.GetAsync(id);

                if (employeeDb != null)
                {
                    employeeDb.Surname = employee.Surname;
                    employeeDb.Name = employee.Name;
                    employeeDb.Patronymic = employee.Patronymic;
                    employeeDb.IsDeleted = employee.IsDeleted;
                    employeeDb.DepartmentID = employee.DepartmentID;

                    await _db.Employees.UpdateAsync(employeeDb);
                    await _db.SaveAsync();
                    return await CreateUiEmployeeByDbEmployeeAsync(employeeDb);
                }

                return employee;
            });
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
                DepartmentSignature = employeeDb.Department?.Signature ?? await Task.Run(async () =>
                {
                    DbModel.Department departmentDb = await _db.Departments.GetAsync(employeeDb.DepartmentID);
                    return departmentDb.Signature;
                })
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