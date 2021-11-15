using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeeAccounting.Db.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(Context context)
        {
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> Get()
        {
            return _dbSet.AsQueryable();
        }


        public T Get(int id)
        {   
            return _dbSet.Find(id);
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public T Update(int id)
        {
            T item = Get(id);
            if (item != null)
            {
               _dbSet.Update(item);
               return _dbSet.Find(id);
            }

            return null;
        }

        public void Delete()
        {
            IQueryable<T> items = GetAll();
            if (items.Any())
            {
                _dbSet.RemoveRange(items);
            }
        }

        public async void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(int id)
        {
            T item = Get(id);
            if (item != null)
            {
                _dbSet.Remove(item);
            }
        }
    }
}