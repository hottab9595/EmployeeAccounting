using System;
using System.Linq;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        T Update(T entity);
        T Update(int id);
        void Delete();
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}