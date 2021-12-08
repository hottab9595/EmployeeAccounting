using EmployeeAccounting.Services.Interfaces;
using EmployeeAccounting.UI.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeAccounting.Common.Exceptions;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeService<Employee> es)
        {
            this._es = es;
        }

        private IEmployeeService<Employee> _es;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _es.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificEmployee(int id)
        {
            return Ok(await _es.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(Employee employee)
        {
            return Ok(await _es.AddNewAsync(employee));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            return Ok(await _es.UpdateAsync(id, employee));
        }

        [HttpPut("Delete/{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _es.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}/AddCourse/{courseId:int}")]
        public async Task<IActionResult> AddCourseToEmployee(int id, int courseId, Course course)
        {
            return Ok(await _es.AddEmployeeAsync(id, courseId));
        }

        [HttpPut("{id:int}/RemoveCourse/{courseId:int}")]
        public async Task<IActionResult> RemoveCourseFromEmployee(int id, int courseId, Course course)
        {
            return Ok(await _es.RemoveEmployeeAsync(id, courseId));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteEmployee(int id)
        {
            await _es.FullDeleteAsync(id);
            return Ok();
        }
    }
}
