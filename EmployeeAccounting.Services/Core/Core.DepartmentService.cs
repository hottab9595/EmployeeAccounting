using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class DepartmentService : CoreService, IDepartmentService
    {
        public DepartmentService(IUnitOfWork db) : base(db)
        {
            this._db = db;
        }

        private IUnitOfWork _db;

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Department>> GetDepartmentsAsync() => await Task.Run(() => _db.Departments.GetAsync().Result.AsEnumerable().Select(CreateUiDepartmentByDbDepartment));

        public async Task<UIModel.Department> GetDepartmentAsync(int id) => await Task.Run(() => CreateUiDepartmentByDbDepartment(_db.Departments.GetAsync(id).Result));

        public async Task<UIModel.Department> AddNewDepartmentAsync(UIModel.Department department) => await Task.Run(async () =>
            {
                DbModel.Department departmentDb = await _db.Departments.AddAsync(CreateDbDepartmentByUiDepartment(department));
                await _db.SaveAsync();
                return CreateUiDepartmentByDbDepartment(departmentDb);
            });

        public async Task<UIModel.Department> UpdateDepartmentAsync(int id, UIModel.Department department)
        {
            return await Task.Run(async () =>
            {
                DbModel.Department departmentDb = await _db.Departments.GetAsync(id);

                if (departmentDb != null)
                {
                    departmentDb.Signature = department.Signature;
                    departmentDb.ParentID = department.ParentID;
                    departmentDb.IsDeleted = department.IsDeleted;

                    await _db.Departments.UpdateAsync(departmentDb);
                    await _db.SaveAsync();
                    return CreateUiDepartmentByDbDepartment(departmentDb);
                }

                return department;
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Department departmentDb = await _db.Departments.GetAsync(id);

            if (departmentDb != null)
            {
                departmentDb.IsDeleted = true;
                await _db.Departments.UpdateAsync(id);
                await _db.SaveAsync();
            }
        }

        public async Task DeleteAsync(UIModel.Department department) => await DeleteAsync(department.ID);

        public async Task FullDeleteAsync(int id)
        {
            if (await _db.Departments.GetAsync(id) != null)
            {
                _db.Departments.DeleteAsync(id);
                await _db.SaveAsync();
            }
        }

        #endregion

        #region Helpers

        private UIModel.Department CreateUiDepartmentByDbDepartment(DbModel.Department departmentDb)
        {
            return new UIModel.Department
            {
                ID = departmentDb.ID,
                Signature = departmentDb.Signature,
                ParentID = departmentDb.ParentID,
                IsDeleted = departmentDb.IsDeleted,
                Employees = departmentDb.Employees?.Select(x => new UIModel.Employee
                {
                    ID = x.ID,
                    Surname = x.Surname,
                    Name = x.Name,
                    Patronymic = x.Patronymic,
                    IsDeleted = x.IsDeleted,
                    DepartmentID = x.DepartmentID,
                    DepartmentSignature = x.Department.Signature
                }).ToList()
            };
        }

        private DbModel.Department CreateDbDepartmentByUiDepartment(UIModel.Department employeeUi)
        {
            return new DbModel.Department
            {
                Signature = employeeUi?.Signature,
                ParentID = employeeUi?.ParentID,
                IsDeleted = employeeUi?.IsDeleted ?? false
            };
        }

        #endregion

    }
}