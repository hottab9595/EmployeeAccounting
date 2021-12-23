using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Models;

namespace EmployeeAccounting.Services.Core
{
    public abstract class CoreService<T> where T : BaseModel
    {
        protected IUnitOfWork _db;
        public CoreService(IUnitOfWork db)
        {
            _db = db;
        }
    }
}