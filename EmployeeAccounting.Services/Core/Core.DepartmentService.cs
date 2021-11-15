using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class DepartmentService<T> : CoreService<T>, IDepartmentService<UIModel.Department> where T : UIModel.BaseModel
    {
        public DepartmentService(IUnitOfWork db, IMapper mapper) :  base(db)
        {
            this._mapper = mapper;
        }

        private readonly IMapper _mapper;

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Department>> GetAsync() => await Task.Run(() => _db.Departments.GetAll().AsEnumerable().Select(_mapper.Map<UIModel.Department>));

        public async Task<UIModel.Department> GetAsync(int id) => await Task.Run(() => _mapper.Map<UIModel.Department>(_db.Departments.Get(id)));

        public async Task<UIModel.Department> AddNewAsync(UIModel.Department department) => await Task.Run(async () =>
            {
                DbModel.Department departmentDb = _db.Departments.Add(_mapper.Map<DbModel.Department>(department));
                await _db.SaveAsync();
                return _mapper.Map<UIModel.Department>(departmentDb);
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
                    return _mapper.Map<UIModel.Department>(departmentDb);
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
    }
}