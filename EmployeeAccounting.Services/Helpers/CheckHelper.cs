using System.Collections.Generic;
using System.Linq;
using EmployeeAccounting.Common.Exceptions;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Helpers
{
    public class CheckHelper : ICheckHelper
    {
        private IUnitOfWork _db;

        public CheckHelper(IUnitOfWork db)
        {
            this._db = db;
        }

        public void CheckDbModelExists(Db.Model.Course courseDb, Db.Model.Employee employeeDb)
        {
            CheckDbModelExists(courseDb);
            CheckDbModelExists(employeeDb);
        }

        public void CheckDbModelExists(Db.Model.Course courseDb)
        {
            if (courseDb == null)
            {
                throw new CourseNotFoundException();
            }
        }

        public void CheckDbModelExists(Db.Model.Employee employeeDb)
        {
            if (employeeDb == null)
            {
                throw new EmployeeNotFoundException();
            }
        }

        public void CheckDbModelExists(Department departmentDb)
        {
            if (departmentDb == null)
            {
                throw new DepartmentNotFoundException();
            }
        }

        public void CheckDbModelsIsAlreadyAdded(IEnumerable<CourseEmployee> courseEmployees, Course courseDb, Employee employeeDb)
        {
            if (courseEmployees
                .Where(x => x.Employee == employeeDb)
                .Any(x => x.Course == courseDb))
            {
                throw new CourseEmployeeAlreadyAddedException();
            }
        }
    }
}