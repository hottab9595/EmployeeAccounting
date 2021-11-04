using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Db.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(Context context)
        {
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await Task.Run(() => _dbSet.AsEnumerable());
        }

        public async Task<T> GetAsync(int id)
        {   
            return await _dbSet.FindAsync(id);
        }

        public async void AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async void UpdateAsync(T entity)
        {
            await Task.Run(() => _dbSet.Update(entity));
        }

        public async void UpdateAsync(int id)
        {
            Task<T> item = GetAsync(id);
            if (item.Result != null)
            {
               await Task.Run(() => _dbSet.Update(item.Result));
            }
        }

        public async void DeleteAsync()
        {
            Task<IEnumerable<T>> items = GetAsync();
            if (items.Result.Any())
            {
                await Task.Run(() => _dbSet.RemoveRange(items.Result));
            }
        }

        public async void DeleteAsync(T entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
        }

        public async void DeleteAsync(int id)
        {
            Task<T> item = GetAsync(id);
            if (item.Result != null)
            {
                await Task.Run(() => _dbSet.Remove(item.Result));
            }
        }
    }
}