using EmployeeAccounting.Db.Interfaces;

namespace EmployeeAccounting.Services.Core
{
    public class DepartmentService : CoreService
    {
        public DepartmentService(IUnitOfWork db) : base(db)
        {

        }


    }
}