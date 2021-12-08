using System.Collections.Generic;

namespace EmployeeAccounting.Services.Helpers
{
    public interface ICheckHelper
    {
        void CheckDbModelExists(Db.Model.Course courseDb, Db.Model.Employee employeeDb);
        void CheckDbModelExists(Db.Model.Course courseDb);
        void CheckDbModelExists(Db.Model.Employee employeeDb);
        void CheckDbModelExists(Db.Model.Department departmentDb);
        void CheckDbModelsIsAlreadyAdded(IEnumerable<Db.Model.CourseEmployee> courseEmployees, Db.Model.Course courseDb, Db.Model.Employee employeeDb);
    }
}