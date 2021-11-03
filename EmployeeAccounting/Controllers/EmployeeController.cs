using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db;
using EmployeeAccounting.Db.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        public EmployeeController(Context db)
        {
            this._db = db;
        }

        private Context _db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _db.Employees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetSpecificEmployee(int id)
        {
            Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }
        
        [HttpPost]
        public async Task<ActionResult<Employee>> AddNewEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            if (!_db.Employees.Any(e => e.ID == employee.ID))
            {
                return NotFound();
            }

            _db.Update(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<Employee>> DeletEmployee(int id)
        {
            Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.IsDeleted = true;

            _db.Update(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> FullDeleteEmployee(int id)
        {
            Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);
            if (employee == null)
            {
                return NotFound();
            }
            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();
            return Ok(employee);
        }
    }
}
