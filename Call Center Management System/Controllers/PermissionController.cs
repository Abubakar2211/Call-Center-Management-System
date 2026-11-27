using Call_Center_Management_System.Data;
using Call_Center_Management_System.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Call_Center_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public PermissionController(RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDTO model)
        {
            var role = await _roleManager.FindByNameAsync(model.role);
            if (role == null)
                return NotFound(new { message = "Role not found" });
            var claims = await _roleManager.GetClaimsAsync(role);

            if (claims.Any(x =>x.Type == model.Permission.Type && x.Value == model.Permission.Value))
            {
                return BadRequest(new { message = "Permission already exists." });
            }
            var result = await _roleManager.AddClaimAsync(role, model.Permission);

            if (result.Succeeded)
            {
                return Ok(new { message = "Permission created successfully." });
            }
            var error = result.Errors.Select(x => x.Description);
            return BadRequest(new { error = error });
            
        }
    }
}
