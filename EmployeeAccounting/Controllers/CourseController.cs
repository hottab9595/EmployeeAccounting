using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmployeeAccounting.Services.Interfaces;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        public CourseController(ICourseService<Course> cs)
        {
            this._cs = cs;
        }

        private ICourseService<Course> _cs;

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _cs.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificCourse(int id)
        {
            return Ok(await _cs.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourse(Course course)
        {
            return Ok(await _cs.AddNewAsync(course));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            return Ok(await _cs.UpdateAsync(id, course));
        }

        [HttpPut("/{id:int}/AddEmployee/{employeeId:int}")]
        public async Task<IActionResult> AddEmployeeToCourse(int id, int employeeId)
        {
            return Ok(await _cs.AddEmployeeAsync(id, employeeId));
        }

        [HttpPut("/{id:int}/RemoveEmployee/{employeeId:int}")]
        public async Task<IActionResult> RemoveEmployeeFromCourse(int id, int employeeId)
        {
            return Ok(await _cs.RemoveEmployeeAsync(id, employeeId));
        }

        [HttpPut("Delete/{id:int}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _cs.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteCourse(int id)
        {
            await _cs.FullDeleteAsync(id);
            return Ok();
        }
    }
}
