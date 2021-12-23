using System.Collections.Generic;
using EmployeeAccounting.Services.Interfaces;
using SmDepartment = EmployeeAccounting.Services.Models.Department;
using EmployeeAccounting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        public DepartmentController(IDepartmentService<SmDepartment> ds, IMapper mapper)
        {
            _ds = ds;
            _mapper = mapper;
        }

        private readonly IDepartmentService<SmDepartment> _ds;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            return Ok(_mapper.Map<List<Department>>(await _ds.GetAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificDepartment(int id)
        {
            return Ok(_mapper.Map<Department>(await _ds.GetAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            return Ok(await _ds.AddNewAsync(_mapper.Map<SmDepartment>(department)));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            return Ok(await _ds.UpdateAsync(id, _mapper.Map<SmDepartment>(department)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteDepartment(int id)
        {
            await _ds.DeleteAsync(id);
            return Ok();
        }
    }
}
