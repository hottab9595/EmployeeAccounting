using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;
using UIModel = EmployeeAccounting.UI.Model;
using DbModel = EmployeeAccounting.Db.Model;

namespace EmployeeAccounting.Services.Core
{
    public class EmployeeService : CoreService, IEmployeeService
    {
        public EmployeeService(IUnitOfWork db) : base(db)
        {
            
        }

        public UIModel.Employee GetEmployee(int id)
        {
            DbModel.Employee employeeDb = _db.Employees.GetAsync(id).Result;

            UIModel.Employee employee = new UIModel.Employee
            {
                ID = employeeDb.ID,
                Surname = employeeDb.Surname,
                Name = employeeDb.Name,
                Patronymic = employeeDb.Patronymic,
                IsDeleted = employeeDb.IsDeleted,
                //DepartmentSignature = employeeDb.Department.Signature
            };

            return employee;
        }
    }
}