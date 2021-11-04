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
            //this._db = db;
            this._es = es;
        }

        private IUnitOfWork _db;
        private IEmployeeService _es;

        [HttpGet]
        public ActionResult<Task<IEnumerable<Employee>>> GetEmployees()
        { 
            //_es.GetEmployee()
            //var lol = _db.Employees.GetAsync();
            return null;
        }

        [HttpGet("{id}")]
        public Employee GetSpecificEmployee(int id)
        {
            //Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);
            //if (employee == null)
            //{
            //    return NotFound();
            //}
            

            return _es.GetEmployee(id); 
        }

        //[HttpPost]
        //public async Task<ActionResult<Employee>> AddNewEmployee(Employee employee)
        //{
        //    if (employee == null)
        //    {
        //        return BadRequest();
        //    }

        //    _db.Employees.Add(employee);
        //    await _db.SaveChangesAsync();
        //    return Ok(employee);
        //}

        //[HttpPut]
        //public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        //{
        //    if (employee == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!_db.Employees.Any(e => e.ID == employee.ID))
        //    {
        //        return NotFound();
        //    }

        //    _db.Update(employee);
        //    await _db.SaveChangesAsync();
        //    return Ok(employee);
        //}

        //[HttpPut("Delete/{id}")]
        //public async Task<ActionResult<Employee>> DeletEmployee(int id)
        //{
        //    Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    employee.IsDeleted = true;

        //    _db.Update(employee);
        //    await _db.SaveChangesAsync();
        //    return Ok(employee);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Employee>> FullDeleteEmployee(int id)
        //{
        //    Employee employee = _db.Employees.FirstOrDefault(e => e.ID == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    _db.Employees.Remove(employee);
        //    await _db.SaveChangesAsync();
        //    return Ok(employee);
        //}
    }
}
