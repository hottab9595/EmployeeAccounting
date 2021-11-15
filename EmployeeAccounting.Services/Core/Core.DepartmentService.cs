﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class DepartmentService<T> : CoreService<T>, IDepartmentService<UIModel.Department> where T : UIModel.BaseModel
    {
        public DepartmentService(IUnitOfWork db) :  base(db)
        {
        }

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Department>> GetAsync() => await Task.Run(() => _db.Departments.GetAll().AsEnumerable().Select(CreateUiDepartmentByDbDepartment));

        public async Task<UIModel.Department> GetAsync(int id) => await Task.Run(() => CreateUiDepartmentByDbDepartment(_db.Departments.Get(id)));

        public async Task<UIModel.Department> AddNewAsync(UIModel.Department department) => await Task.Run(async () =>
            {
                DbModel.Department departmentDb = _db.Departments.Add(CreateDbDepartmentByUiDepartment(department));
                await _db.SaveAsync();
                return CreateUiDepartmentByDbDepartment(departmentDb);
            });

        public async Task<UIModel.Department> UpdateAsync(int id, UIModel.Department department)
        {
            return await Task.Run(async () =>
            {
                DbModel.Department departmentDb = _db.Departments.Get(id);

                if (departmentDb != null)
                {
                    departmentDb.Signature = department.Signature;
                    departmentDb.ParentID = department.ParentID;
                    departmentDb.IsDeleted = department.IsDeleted;

                    _db.Departments.Update(departmentDb);
                    await _db.SaveAsync();
                    return CreateUiDepartmentByDbDepartment(departmentDb);
                }

                return department;
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Department departmentDb = _db.Departments.Get(id);

            if (departmentDb != null)
            {
                departmentDb.IsDeleted = true;
                _db.Departments.Update(id);
                await _db.SaveAsync();
            }
        }

        public Task DeleteAsync(UIModel.Department t)
        {
            throw new System.NotImplementedException();
        }

        public async Task FullDeleteAsync(int id)
        {
            if (_db.Departments.Get(id) != null)
            {
                _db.Departments.Delete(id);
                await _db.SaveAsync();
            }
        }

        #endregion

        #region Helpers

        private UIModel.Department CreateUiDepartmentByDbDepartment(DbModel.Department departmentDb)
        {
            return departmentDb == null ? null : new UIModel.Department
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
            return employeeUi == null ? null : new DbModel.Department
            {
                Signature = employeeUi?.Signature,
                ParentID = employeeUi?.ParentID,
                IsDeleted = employeeUi?.IsDeleted ?? false
            };
        }

        #endregion

    }
}