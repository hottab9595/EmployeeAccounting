using AutoMapper;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAccounting.Services.Helpers;
using DbModel = EmployeeAccounting.Db.Model;
using EmployeeAccounting.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Services.Core
{
    public class DepartmentService<T> : CoreService<T>, IDepartmentService<Department> where T : BaseModel
    {
        public DepartmentService(IUnitOfWork db, IMapper mapper, IUtils utils) : base(db)
        {
            _mapper = mapper;
            _utils = utils;
        }

        private readonly IMapper _mapper;
        private readonly IUtils _utils;

        #region Interfaces realization

        public async Task<IEnumerable<Department>> GetAsync() => _mapper.Map<List<Department>>(await _db.Departments.FindBy(x => !x.IsDeleted).ToListAsync());

        public async Task<Department> GetAsync(int id) => _mapper.Map<Department>((await _utils.IsDepartmentExistsAsync(id)).department);

        public async Task<Department> AddNewAsync(Department department)
        {
            await _utils.IsDepartmentNotExistsAsync(department);

            DbModel.Department departmentDb = _db.Departments.Add(_mapper.Map<DbModel.Department>(department));
            await _db.SaveAsync();
            return _mapper.Map<Department>(departmentDb);
        }

        public async Task<Department> UpdateAsync(int id, Department department)
        {
            DbModel.Department departmentDb = (await _utils.IsDepartmentExistsAsync(id)).department;

            departmentDb.Signature = department.Signature;
            departmentDb.ParentId = department.ParentId;
            departmentDb.IsDeleted = department.IsDeleted;

            _db.Departments.Update(departmentDb);
            await _db.SaveAsync();
            return _mapper.Map<Department>(departmentDb);
        }

        public async Task DeleteAsync(int id)
        {
            await _utils.IsDepartmentExistsAsync(id);

            _db.Departments.Delete(id);
            await _db.SaveAsync();
        }

        #endregion
    }
}