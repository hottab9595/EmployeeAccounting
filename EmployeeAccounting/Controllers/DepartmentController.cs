using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeAccounting.UI.Model;
using EmployeeAccounting.Services.Interfaces;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        public DepartmentController(IDepartmentService ds)
        {
            this._ds = ds;
        }

        private IDepartmentService _ds;

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(await _ds.GetDepartmentsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificDepartment(int id)
        {
            return Ok(await _ds.GetDepartmentAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            return Ok(await _ds.AddNewDepartmentAsync(department));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            return Ok(await _ds.UpdateDepartmentAsync(id, department));
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
