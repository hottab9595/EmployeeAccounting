using System.Collections.Generic;
using EmployeeAccounting.Services.Interfaces;
using SmCourse = EmployeeAccounting.Services.Models.Course;
using EmployeeAccounting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/Course")]
    public class CourseController : Controller
    {
        public CourseController(ICourseService<SmCourse> cs, IMapper mapper)
        {
            _cs = cs;
            _mapper = mapper;
        }

        private readonly ICourseService<SmCourse> _cs;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(_mapper.Map<List<Course>>(await _cs.GetAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificCourse(int id)
        {
            return Ok(_mapper.Map<Course>(await _cs.GetAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCourse(Course course)
        {
            return Ok(await _cs.AddNewAsync(_mapper.Map<SmCourse>(course)));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            return Ok(await _cs.UpdateAsync(id, _mapper.Map<SmCourse>(course)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteCourse(int id)
        {
            await _cs.DeleteAsync(id);
            return Ok();
        }
    }
}
