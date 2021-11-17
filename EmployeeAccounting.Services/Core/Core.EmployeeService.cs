using AutoMapper;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbModel = EmployeeAccounting.Db.Model;
using UIModel = EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService<T> : CoreService<T>, IEmployeeService<UIModel.Employee> where T : UIModel.BaseModel
    {
        public EmployeeService(IUnitOfWork db, IMapper mapper) : base(db)
        {
            this._mapper = mapper;
        }

        private readonly IMapper _mapper;

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Employee>> GetAsync() => await Task.Run(() => _db.Employees.GetAll().AsEnumerable()
            .Select(_mapper.Map<UIModel.Employee>));

        public async Task<UIModel.Employee> GetAsync(int id) => await Task.Run(() => _mapper.Map<UIModel.Employee>(_db.Employees.Get(id)));

        public async Task<UIModel.Employee> AddNewAsync(UIModel.Employee employee) => await Task.Run(async () =>
        {
            DbModel.Employee employeeDb = _db.Employees.Add(_mapper.Map<DbModel.Employee>(employee));
            await _db.SaveAsync();
            return _mapper.Map<UIModel.Employee>(employeeDb);
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
                    return _mapper.Map<UIModel.Employee>(employeeDb);
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
    }
}