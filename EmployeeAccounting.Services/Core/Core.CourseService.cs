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
    public class CourseService<T> : CoreService<T>, ICourseService<Course> where T : BaseModel
    {
        public CourseService(IUnitOfWork db, IMapper mapper, IUtils utils) : base(db)
        {
            _mapper = mapper;
            _utils = utils;
        }

        private readonly IMapper _mapper;
        private readonly IUtils _utils;

        #region Interfaces realization

        public async Task<IEnumerable<Course>> GetAsync() => _mapper.Map<List<Course>>(await _db.Courses.FindBy(x => !x.IsDeleted).ToListAsync());

        public async Task<Course> GetAsync(int id) => _mapper.Map<Course>((await _utils.IsCourseExistsAsync(id)).course);

        public async Task<Course> AddNewAsync(Course course)
        {
            await _utils.IsCourseNotExistsAsync(course);

            DbModel.Course courseDb = _db.Courses.Add(_mapper.Map<DbModel.Course>(course));
            await _db.SaveAsync();
            return _mapper.Map<Course>(courseDb);
        }

        public async Task<Course> UpdateAsync(int id, Course course)
        {
            DbModel.Course courseDb = (await _utils.IsCourseExistsAsync(id)).course;

            courseDb.Signature = course.Signature;
            courseDb.Duration = course.Duration;

            _db.Courses.Update(courseDb);
            await _db.SaveAsync();
            return _mapper.Map<Course>(courseDb);
        }
        
        public async Task DeleteAsync(int id)
        {
            await _utils.IsCourseExistsAsync(id);

            _db.Courses.Delete(id);
            await _db.SaveAsync();
        }

        #endregion
    }
}