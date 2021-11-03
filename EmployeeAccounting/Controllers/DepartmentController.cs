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
    public class DepartmentController : Controller
    {
        public DepartmentController(Context db)
        {
            this._db = db;
        }

        private Context _db;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var lol = _db.Departments.ToList();
            return  _db.Departments.ToList();
            return await _db.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetSpecificDepartment(int id)
        {
            Department department = _db.Departments.FirstOrDefault(e => e.ID == id);
            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> AddDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }

            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            return Ok(department);
        }

        [HttpPut]
        public async Task<ActionResult<Department>> UpdateDepartment(Department department)
        {
            if (department == null)
            {
                return BadRequest();
            }
            if (!_db.Departments.Any(e => e.ID == department.ID))
            {
                return NotFound();
            }

            _db.Update(department);
            await _db.SaveChangesAsync();
            return Ok(department);
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            Department department = _db.Departments.FirstOrDefault(e => e.ID == id);

            if (department == null)
            {
                return NotFound();
            }

            department.IsDeleted = true;

            _db.Update(department);
            await _db.SaveChangesAsync();
            return Ok(department);
        }

       

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> FullDeleteDepartment(int id)
        {
            Department department = _db.Departments.FirstOrDefault(e => e.ID == id);
            if (department == null)
            {
                return NotFound();
            }
            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();
            return Ok(department);
        }
    }
}
