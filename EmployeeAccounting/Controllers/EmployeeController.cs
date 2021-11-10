using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db.Interfaces;
using EmployeeAccounting.Services.Core;
using EmployeeAccounting.Services.Interfaces;
using EmployeeAccounting.UI.Model;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeService es)
        {
            this._es = es;
        }
        
        private IEmployeeService _es;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _es.GetEmployeesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecificEmployee(int id)
        {
            return Ok(await _es.GetEmployeeAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(Employee employee)
        {
            return Ok(await _es.AddNewEmployeeAsync(employee));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            return Ok(await _es.UpdateEmployeeAsync(id, employee));
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            await _es.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> FullDeleteEmployee(int id)
        {
            await _es.FullDeleteAsync(id);
            return Ok();
        }
    }
}
