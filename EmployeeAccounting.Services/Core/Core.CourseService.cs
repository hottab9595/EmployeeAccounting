using AutoMapper;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EmployeeAccounting.Common.Exceptions;
using EmployeeAccounting.Services.Helpers;
using DbModel = EmployeeAccounting.Db.Model;
using UIModel = EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Core
{
    public class CourseService<T> : CoreService<T>, ICourseService<UIModel.Course> where T : UIModel.BaseModel
    {
        public CourseService(IUnitOfWork db, IMapper mapper, ICheckHelper checkHelper) : base(db)
        {
            this._mapper = mapper;
            this._checkHelper = checkHelper;
        }

        private readonly IMapper _mapper;
        private readonly ICheckHelper _checkHelper;

        #region Interfaces realization
        
        public async Task<IEnumerable<UIModel.Course>> GetAsync()
        {
            return await Task.Run(() =>
            {
                List<DbModel.Course> dbCourses = _db.Courses.GetAll().ToList();

                List<UIModel.Course> uiCourses = _db.Courses.GetAll().AsEnumerable().Select(_mapper.Map<UIModel.Course>).ToList();

                foreach (UIModel.Course uiCourse in uiCourses)
                {
                    foreach (DbModel.Course dbCourse in dbCourses.Where(x => x.ID == uiCourse.ID))
                    {
                        _checkHelper.CheckDbModelExists(dbCourse);

                        foreach (DbModel.CourseEmployee dbCourseCourseEmployee in dbCourse.CourseEmployees)
                        {
                            uiCourse.Employees.Add(_mapper.Map<UIModel.Employee>(dbCourseCourseEmployee.Employee));
                        }
                    }
                }

                return uiCourses.AsEnumerable();
            });
        }

        public async Task<UIModel.Course> GetAsync(int id)
        {
            return await Task.Run(() =>
            {
                DbModel.Course dbCourse = _db.Courses.Get(id);

                _checkHelper.CheckDbModelExists(dbCourse);

                UIModel.Course uiCourse = _mapper.Map<UIModel.Course>(dbCourse);

                foreach (DbModel.CourseEmployee courseEmployee in dbCourse.CourseEmployees)
                {
                    uiCourse.Employees.Add(_mapper.Map<UIModel.Employee>(courseEmployee.Employee));
                }

                return uiCourse;
            });
        }

        public async Task<UIModel.Course> AddNewAsync(UIModel.Course course) => await Task.Run(async () =>
        {
            DbModel.Course courseDb = _db.Courses.Add(_mapper.Map<DbModel.Course>(course));
            await _db.SaveAsync();
            return _mapper.Map<UIModel.Course>(courseDb);
        });

        public async Task<UIModel.Course> UpdateAsync(int id, UIModel.Course course)
        {
            return await Task.Run(async () =>
            {
                DbModel.Course courseDb = _db.Courses.Get(id);

                _checkHelper.CheckDbModelExists(courseDb);
                courseDb.Signature = course.Signature;
                courseDb.Duration = course.Duration;

                _db.Courses.Update(courseDb);
                await _db.SaveAsync();
                return _mapper.Map<UIModel.Course>(courseDb);
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Course courseDb = _db.Courses.Get(id);

            _checkHelper.CheckDbModelExists(courseDb);
            courseDb.IsDeleted = true;
            _db.Employees.Update(id);
            await _db.SaveAsync();
        }

        public Task DeleteAsync(UIModel.Course t)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task FullDeleteAsync(int id)
        {
            DbModel.Course courseDb = _db.Courses.Get(id);

            _checkHelper.CheckDbModelExists(courseDb);
            _db.Courses.Delete(id);
            await _db.SaveAsync();
        }

        public async Task<UIModel.Course> AddEmployeeAsync(int id, int employeeId)
        {
            (DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = GetCourseEmployeeTuple(id, employeeId);

            _checkHelper.CheckDbModelsIsAlreadyAdded(_db.CourseEmployees.GetAll().AsEnumerable(), tuple.courseDb, tuple.employeeDb);

            tuple.employeeDb.CourseEmployees.Add(new DbModel.CourseEmployee { Employee = tuple.employeeDb, Course = tuple.courseDb});
            await _db.SaveAsync();

            return await GetAsync(id);
        }

        public async Task<UIModel.Course> RemoveEmployeeAsync(int id, int employeeId)
        {
            (DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = GetCourseEmployeeTuple(id, employeeId);

            DbModel.CourseEmployee courseEmployee = _db.CourseEmployees.FindBy(x => x.CourseId == tuple.courseDb.ID && x.EmployeeId == tuple.employeeDb.ID).FirstOrDefault();
            _db.CourseEmployees.Delete(courseEmployee);
            _db.Courses.Update(tuple.courseDb);
            await _db.SaveAsync();
            return await GetAsync(id);
        }

        #endregion

        private (Db.Model.Course courseDb, Db.Model.Employee employeeDb) GetCourseEmployeeTuple(int idCourse, int idEmployee)
        {
            Db.Model.Course courseDb = _db.Courses.Get(idCourse);
            Db.Model.Employee employeeDb = _db.Employees.Get(idEmployee);

            _checkHelper.CheckDbModelExists(courseDb, employeeDb);

            return (course: courseDb, employee: employeeDb);
        }
    }
}