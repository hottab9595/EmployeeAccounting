using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAccounting.Db;
using EmployeeAccounting.Db.Model;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, Context db)
        {
            _logger = logger;
            this._db = db;
        }

        private Context _db;

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _db.Employees;
        }
    }
}
