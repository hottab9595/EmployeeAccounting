using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeAccounting.UI;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Services.Interfaces
{
    public interface ICoreCrud<T> where T : BaseModel
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