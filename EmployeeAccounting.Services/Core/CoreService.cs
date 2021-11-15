using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Core
{
    public abstract class CoreService<T> where T : BaseModel
    {
        protected IUnitOfWork _db;
        public CoreService(IUnitOfWork db)
        {
            this._db = db;
        }
    }
}