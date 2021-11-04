using EmployeeAccounting.Db.Interfaces;

namespace EmployeeAccounting.Services.Core
{
    public class CoreService
    {
        protected IUnitOfWork _db;
        public CoreService(IUnitOfWork db)
        {
            this._db = db;
        }
    }
}