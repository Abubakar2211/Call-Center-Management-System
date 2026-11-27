using Call_Center_Management_System.Data;
using Call_Center_Management_System.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(x => new{ Id = x.Id, Name = x.Name}).ToListAsync();
            return Ok(roles);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleDTO model)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(model.name));
            if (result.Succeeded)
            {
                return Ok(new { message = "Role created successfully." });
            }
            var error = result.Errors.Select(x => x.Description);
            return BadRequest(new { error = error });

        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound(new { message = "Role not found" });
            return Ok(new
            {
                Id = role.Id,
                Name = role.Name
            });
        }
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id,[FromBody] EditRoleDTO model)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound(new { message = "Role not found." });
            }
            role.Name = model.name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role update successfully." });
            }
            var error = result.Errors.Select(x => x.Description);
            return BadRequest(new { error = error });
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok(new { message = "Role deleted successfully." });
            }
            var error = result.Errors.Select(x => x.Description);
            return BadRequest(new { error = error });
        }

    }
}
