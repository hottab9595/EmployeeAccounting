using System.Collections.Generic;
using EmployeeAccounting.Services.Interfaces;
using SmMembership = EmployeeAccounting.Services.Models.Membership;
using EmployeeAccounting.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

namespace EmployeeAccounting.Controllers
{
    [ApiController]
    [Route("api/Membership")]
    public class MembershipController : Controller
    {
        public MembershipController(IMembershipService<SmMembership> ms, IMapper mapper)
        {
            _ms = ms;
            _mapper = mapper;
        }

        private readonly IMembershipService<Services.Models.Membership> _ms;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<IActionResult> GetMemberships()
        {
            return Ok(_mapper.Map<List<Membership>>(await _ms.GetAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpecificMembership(int id)
        {
            return Ok(_mapper.Map<Membership>(await _ms.GetAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMembership(Membership membership)
        {
            return Ok(await _ms.AddNewAsync(_mapper.Map<SmMembership>(membership)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> FullDeleteMembership(int id)
        {
            await _ms.DeleteAsync(id);
            return Ok();
        }
    }
}
