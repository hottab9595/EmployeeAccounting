using System.Linq;

namespace EmployeeAccounting.Db.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get();
        T Get(int id);
        T Add(T entity);
        T Update(T entity);
        T Update(int id);
        void Delete();
        void Delete(T entity);
        void Delete(int id);
    }
}