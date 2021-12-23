using System.Collections.Generic;
using EmployeeAccounting.Services.Interfaces;
using SmEmployee = EmployeeAccounting.Services.Models.Employee;
using EmployeeAccounting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeService<SmEmployee> es, IMapper mapper)
        {
            _es = es;
            _mapper = mapper;
        }

        private readonly IEmployeeService<SmEmployee> _es;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(_mapper.Map<List<Employee>>(await _es.GetAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificEmployee(int id)
        {
            return Ok(_mapper.Map<Employee>(await _es.GetAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(Employee employee)
        {
            return Ok(await _es.AddNewAsync(_mapper.Map<SmEmployee>(employee)));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            return Ok(await _es.UpdateAsync(id, _mapper.Map<SmEmployee>(employee)));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteEmployee(int id)
        {
            await _es.DeleteAsync(id);
            return Ok();
        }
    }
}
