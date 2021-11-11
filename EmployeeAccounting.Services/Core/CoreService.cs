using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Interfaces;

namespace EmployeeAccounting.Services.Core
{
    public class CoreService<T> : ICoreService<T> where T : UI.Model.BaseModel
    {
        protected IUnitOfWork _db;
        public CoreService(IUnitOfWork db)
        {
            this._db = db;
        }

        public virtual Task<IEnumerable<T>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> AddNewAsync(T t)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<T> UpdateAsync(int id, T t)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task DeleteAsync(T t) => await DeleteAsync(t.ID);

        public virtual Task FullDeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}