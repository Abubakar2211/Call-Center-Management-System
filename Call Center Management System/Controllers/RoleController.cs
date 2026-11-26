using Call_Center_Management_System.Data;
using Call_Center_Management_System.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Call_Center_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        public RoleController(RoleManager<IdentityRole> roleManager,ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string model)
        {
            return Ok(new { message = "Role created successfully." });
            //var result = await _roleManager.CreateAsync(new IdentityRole(model));
            //if (result.Succeeded)
            //{
            //    return Ok(new { message = "Role created successfully." });
            //}
            //var error = result.Errors.Select(x => x.Description);
            //return StatusCode(422, new {error = error});

        }
    }
}
