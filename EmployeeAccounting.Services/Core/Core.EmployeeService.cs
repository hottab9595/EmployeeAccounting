using AutoMapper;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Common.Exceptions;
using EmployeeAccounting.Services.Helpers;
using DbModel = EmployeeAccounting.Db.Model;
using UIModel = EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService<T> : CoreService<T>, IEmployeeService<UIModel.Employee> where T : UIModel.BaseModel
    {
        public EmployeeService(IUnitOfWork db, IMapper mapper, ICheckHelper checkHelper) : base(db)
        {
            this._mapper = mapper;
            this._checkHelper = checkHelper;
        }

        private readonly IMapper _mapper;
        private readonly ICheckHelper _checkHelper;

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Employee>> GetAsync()
        {
            return await Task.Run(() =>
            {
                List<DbModel.Employee> dbEmployees = _db.Employees.GetAll().ToList();

                List<UIModel.Employee> uiEmployees = _db.Employees.GetAll().AsEnumerable().Select(_mapper.Map<UIModel.Employee>).ToList();

                foreach (UIModel.Employee uiEmployee in uiEmployees)
                {
                    foreach (DbModel.Employee dbEmployee in dbEmployees.Where(x => x.ID == uiEmployee.ID))
                    {
                        _checkHelper.CheckDbModelExists(dbEmployee);

                        foreach (DbModel.CourseEmployee dbEmployeeCourseEmployee in dbEmployee.CourseEmployees)
                        {
                            uiEmployee.Courses.Add(_mapper.Map<UIModel.Course>(dbEmployeeCourseEmployee.Course));
                        }
                    }
                }

                return uiEmployees.AsEnumerable();
            });
        }

        public async Task<UIModel.Employee> GetAsync(int id)
        {
            return await Task.Run(() =>
            {
                DbModel.Employee dbEmployee = _db.Employees.Get(id);

                _checkHelper.CheckDbModelExists(dbEmployee);

                UIModel.Employee uiEmployee = _mapper.Map<UIModel.Employee>(dbEmployee);

                foreach (DbModel.CourseEmployee courseEmployee in dbEmployee.CourseEmployees)
                {
                    uiEmployee.Courses.Add(_mapper.Map<UIModel.Course>(courseEmployee.Course));
                }

                return uiEmployee;
            });
        }

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

                _checkHelper.CheckDbModelExists(employeeDb);

                employeeDb.Surname = employee.Surname;
                employeeDb.Name = employee.Name;
                employeeDb.Patronymic = employee.Patronymic;
                employeeDb.IsDeleted = employee.IsDeleted;
                employeeDb.DepartmentID = employee.DepartmentID;

                _db.Employees.Update(employeeDb);
                await _db.SaveAsync();
                return _mapper.Map<UIModel.Employee>(employeeDb);
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.Get(id);

            _checkHelper.CheckDbModelExists(employeeDb);
            employeeDb.IsDeleted = true;
            _db.Employees.Update(id);
            await _db.SaveAsync();
        }

        public Task DeleteAsync(UIModel.Employee t)
        {
            throw new System.NotImplementedException();
        }

        public async Task FullDeleteAsync(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.Get(id);
            _checkHelper.CheckDbModelExists(employeeDb);

            _db.Employees.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<UIModel.Employee> AddEmployeeAsync(int id, int courseId)
        {
            (DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = GetCourseEmployeeTuple(courseId, id);

            _checkHelper.CheckDbModelsIsAlreadyAdded(_db.CourseEmployees.GetAll().AsEnumerable(), tuple.courseDb, tuple.employeeDb);

            tuple.employeeDb.CourseEmployees.Add(new DbModel.CourseEmployee { Course = tuple.courseDb, Employee = tuple.employeeDb });
            await _db.SaveAsync();

            return await GetAsync(id);
        }

        public async Task<UIModel.Employee> RemoveEmployeeAsync(int id, int courseId)
        {
            (DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = GetCourseEmployeeTuple(courseId, id);

            DbModel.CourseEmployee courseEmployee = _db.CourseEmployees.FindBy(x => x.CourseId == tuple.courseDb.ID && x.EmployeeId == tuple.employeeDb.ID).FirstOrDefault();
            _db.CourseEmployees.Delete(courseEmployee);
            _db.Employees.Update(tuple.employeeDb);
            await _db.SaveAsync();

            return await GetAsync(id);
        }

        #endregion

        private (DbModel.Course courseDb, DbModel.Employee employeeDb) GetCourseEmployeeTuple(int idCourse, int idEmployee)
        {
            DbModel.Course courseDb = _db.Courses.Get(idCourse);
            DbModel.Employee employeeDb = _db.Employees.Get(idEmployee);

            _checkHelper.CheckDbModelExists(courseDb, employeeDb);

            return (course: courseDb, employee: employeeDb);
        }
    }
}