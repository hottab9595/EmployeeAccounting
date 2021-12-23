using System.Threading.Tasks;
using EmployeeAccounting.Services.Models;
using DbEmployee = EmployeeAccounting.Db.Model.Employee;
using DbDepartment = EmployeeAccounting.Db.Model.Department;
using DbCourse = EmployeeAccounting.Db.Model.Course;
using DbMembership = EmployeeAccounting.Db.Model.CourseEmployee;

namespace EmployeeAccounting.Services.Helpers
{
    public interface IUtils
    {
        public Task<(bool isExists, DbEmployee employee)> IsEmployeeExistsAsync(int id);

        public Task<(bool isExists, DbEmployee employee)> IsEmployeeExistsAsync(Employee employee);

        public Task<bool> IsEmployeeNotExistsAsync(int id);

        public Task<bool> IsEmployeeNotExistsAsync(Employee employee);

        public Task<(bool isExists, DbDepartment department)> IsDepartmentExistsAsync(int id);

        public Task<(bool isExists, DbDepartment department)> IsDepartmentExistsAsync(Department department);
        public Task<bool> IsDepartmentNotExistsAsync(int id);

        public Task<bool> IsDepartmentNotExistsAsync(Department department);

        public Task<(bool isExists, DbCourse course)> IsCourseExistsAsync(int id);

        public Task<(bool isExists, DbCourse course)> IsCourseExistsAsync(Course course);
        public Task<bool> IsCourseNotExistsAsync(int id);

        public Task<bool> IsCourseNotExistsAsync(Course course);

        public Task<(bool isExists, DbMembership membership)> IsMembershipExists(int id);
        public Task<(bool isExists, DbMembership membership)> IsMembershipExists(Membership membership);
        public Task<bool> IsMembershipNotExists(int id);
        public Task<bool> IsMembershipNotExists(Membership membership);    
    }
}