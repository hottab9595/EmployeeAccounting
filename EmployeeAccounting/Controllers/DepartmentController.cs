using EmployeeAccounting.Services.Interfaces;
using EmployeeAccounting.UI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        public DepartmentController(IDepartmentService<Department> ds)
        {
            this._ds = ds;
        }

        private ICoreCrud<Department> _ds;

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _ds.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificDepartment(int id)
        {
            return Ok(await _ds.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            return Ok(await _ds.AddNewAsync(department));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            return Ok(await _ds.UpdateAsync(id, department));
        }

        [HttpPut("Delete/{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _ds.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteDepartment(int id)
        {
            await _ds.FullDeleteAsync(id);
            return Ok();
        }
    }
}
