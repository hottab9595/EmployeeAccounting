using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface ICoreService<T> where T : UI.Model.BaseModel
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> AddNewAsync(T t);
        Task<T> UpdateAsync(int id, T t);
        Task DeleteAsync(int id);
        Task DeleteAsync(T t);
        Task FullDeleteAsync(int id);
    }
}