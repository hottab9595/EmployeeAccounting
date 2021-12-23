using System.Threading.Tasks;
using EmployeeAccounting.Common.Exceptions;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Models;
using Microsoft.EntityFrameworkCore;
using DbEmployee = EmployeeAccounting.Db.Model.Employee;
using DbDepartment = EmployeeAccounting.Db.Model.Department;
using DbCourse = EmployeeAccounting.Db.Model.Course;
using DbMembership = EmployeeAccounting.Db.Model.CourseEmployee;

namespace EmployeeAccounting.Services.Helpers
{
    public class Utils : IUtils
    {
        public Utils(IUnitOfWork db)
        {
            _db = db;
        }

        private readonly IUnitOfWork _db;

        #region Exists

        public async Task<(bool isExists, DbEmployee employee)> IsEmployeeExistsAsync(int id)
        {
            DbEmployee dbEmployee = await _db.Employees.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbEmployee != null ? (true, dbEmployee) : throw new EmployeeNotFoundException();
        }

        public async Task<(bool isExists, DbEmployee employee)> IsEmployeeExistsAsync(Employee employee)
        {
            return await IsEmployeeExistsAsync(employee.Id);
        }

        public async Task<(bool isExists, DbDepartment department)> IsDepartmentExistsAsync(int id)
        {
            DbDepartment dbDepartment = await _db.Departments.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbDepartment != null ? (true, dbDepartment) : throw new DepartmentNotFoundException();
        }

        public async Task<(bool isExists, DbDepartment department)> IsDepartmentExistsAsync(Department department)
        {
            return await IsDepartmentExistsAsync(department.Id);
        }

        public async Task<(bool isExists, DbCourse course)> IsCourseExistsAsync(int id)
        {
            DbCourse dbCourse = await _db.Courses.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbCourse != null ? (true, dbCourse) : throw new CourseNotFoundException();
        }

        public async Task<(bool isExists, DbCourse course)> IsCourseExistsAsync(Course course)
        {
            return await IsCourseExistsAsync(course.Id);
        }

        public async Task<(bool isExists, DbMembership membership)> IsMembershipExists(int id)
        {
            DbMembership dbMembership = await _db.CourseEmployees.FindBy(x => x.Id == id).FirstOrDefaultAsync();

            return dbMembership != null ? (true, dbMembership) : throw new MembershipNotFoundException();
        }

        public async Task<(bool isExists, DbMembership membership)> IsMembershipExists(Membership membership)
        {
            return await IsMembershipExists(membership.Id);
        }

        #endregion

        #region NotExists

        public async Task<bool> IsEmployeeNotExistsAsync(int id)
        {
            DbEmployee dbEmployee = await _db.Employees.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbEmployee == null ? true : throw new EmployeeAlreadyExistsException();
        }

        public async Task<bool> IsEmployeeNotExistsAsync(Employee employee)
        {
            return await IsEmployeeNotExistsAsync(employee.Id);
        }

        public async Task<bool> IsDepartmentNotExistsAsync(int id)
        {
            DbEmployee dbEmployee = await _db.Employees.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbEmployee == null ? true : throw new DepartmentAlreadyExistsException();
        }

        public async Task<bool> IsDepartmentNotExistsAsync(Department department)
        {
            return await IsDepartmentNotExistsAsync(department.Id);
        }

        public async Task<bool> IsCourseNotExistsAsync(int id)
        {
            DbEmployee dbEmployee = await _db.Employees.FindBy(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();

            return dbEmployee == null ? true : throw new CourseAlreadyExistsException();
        }
        public async Task<bool> IsCourseNotExistsAsync(Course course)
        {
            return await IsCourseNotExistsAsync(course.Id);
        }

        public async Task<bool> IsMembershipNotExists(int id)
        {
            DbMembership dbMembership = await _db.CourseEmployees.FindBy(x => x.Id == id).FirstOrDefaultAsync();

            return dbMembership == null ? true : throw new MembershipAlreadyExistsException();
        }

        public async Task<bool> IsMembershipNotExists(Membership membership)
        {
            await IsEmployeeExistsAsync(membership.Employee.Id);
            await IsCourseExistsAsync(membership.Course.Id);

            DbMembership dbMembership = await _db.CourseEmployees.FindBy(x => x.Id == membership.Id).FirstOrDefaultAsync() ??
                                        await _db.CourseEmployees
                .FindBy(x => x.EmployeeId == membership.Employee.Id && x.CourseId == membership.Course.Id)
                .FirstOrDefaultAsync();

            return dbMembership == null ? true : throw new MembershipAlreadyExistsException();
        }

        #endregion
    }
}