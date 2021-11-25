using System.Collections;
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
    public class CourseService<T> : CoreService<T>, ICourseService<UIModel.Course> where T : UIModel.BaseModel
    {
        public CourseService(IUnitOfWork db, IMapper mapper) : base(db)
        {
            this._mapper = mapper;
        }

        private readonly IMapper _mapper;

        #region Interfaces realization

        public async Task<IEnumerable<UIModel.Course>> GetAsync() => await Task.Run(() => _db.Courses.GetAll().AsEnumerable()
            .Select(_mapper.Map<UIModel.Course>));

        public async Task<UIModel.Course> GetAsync(int id) => await Task.Run(() => _mapper.Map<UIModel.Course>(_db.Courses.Get(id)));

        public async Task<UIModel.Course> AddNewAsync(UIModel.Course course) => await Task.Run(async () =>
        {
            DbModel.Course courseDb = _db.Courses.Add(_mapper.Map<DbModel.Course>(course));
            await _db.SaveAsync();
            IEnumerable<DbModel.Course> lol = new List<DbModel.Course> { new DbModel.Course(), new DbModel.Course(), new DbModel.Course() };
            IEnumerable<UIModel.Course> lol1 = _mapper.Map<IEnumerable<UIModel.Course>>(lol);
            return _mapper.Map<UIModel.Course>(courseDb);
        });

        public async Task<UIModel.Course> UpdateAsync(int id, UIModel.Course course)
        {
            return await Task.Run(async () =>
            {
                DbModel.Course courseDb = _db.Courses.Get(id);

                if (courseDb != null)
                {
                    courseDb.Signature = course.Signature;
                    courseDb.Duration = course.Duration;

                    foreach (DbModel.Employee employee in _mapper.Map<IEnumerable<DbModel.Employee>>(course.Employees))
                    {
                        if (courseDb.Employees.All(e => e.ID != employee.ID))
                        {
                            courseDb.Employees.Add(employee);
                        }
                    }

                    _db.Courses.Update(courseDb);
                    await _db.SaveAsync();
                    return _mapper.Map<UIModel.Course>(courseDb);
                }

                return course;
            });
        }

        public async Task DeleteAsync(int id)
        {
            DbModel.Course courseDb = _db.Courses.Get(id);

            if (courseDb != null)
            {
                courseDb.IsDeleted = true;
                _db.Employees.Update(id);
                await _db.SaveAsync();
            }
        }

        public Task DeleteAsync(UIModel.Course t)
        {
            throw new System.NotImplementedException();
        }


        public async Task FullDeleteAsync(int id)
        {
            DbModel.Course courseDb = _db.Courses.Get(id);

            if (courseDb != null)
            {
                _db.Courses.Delete(id);
                await _db.SaveAsync();
            }
        }

        public async Task<UIModel.Course> AddEmployeeAsync(int id, int employeeId)
        {
            (bool isExists, DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = IsExists(id, employeeId);

            if (tuple.isExists)
            {
                tuple.courseDb.Employees.Add(tuple.employeeDb);
                _db.Courses.Update(tuple.courseDb);
                await _db.SaveAsync();

                return await GetAsync(id);
            }

            return null;
        }

        public async Task<UIModel.Course> RemoveEmployeeAsync(int id, int employeeId)
        {
            (bool isExists, DbModel.Course courseDb, DbModel.Employee employeeDb) tuple = IsExists(id, employeeId);

            if (tuple.isExists)
            {
                tuple.courseDb.Employees.Remove(tuple.employeeDb);
                _db.Courses.Update(tuple.courseDb);
                await _db.SaveAsync();

                return await GetAsync(id);
            }

            return null;
        }

        #endregion

        private (bool isExists, DbModel.Course courseDb, DbModel.Employee employeeDb) IsExists(int idCourse, int idEmployee)
        {
            DbModel.Course courseDb = _db.Courses.Get(idCourse);
            DbModel.Employee employeeDb = _db.Employees.Get(idEmployee);

            return (isExists: courseDb != null && employeeDb != null, course: courseDb, employee: employeeDb);
        }

        private (bool isExists, DbModel.Course courseDb) IsExists(int idCourse)
        {
            DbModel.Course courseDb = _db.Courses.Get(idCourse);

            return (isExists: courseDb != null, course: courseDb);
        }
    }
}