using Call_Center_Management_System.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Call_Center_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(x => new
            {
                Id = x.Id,
                Username = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber
            }).ToListAsync();
            return Ok(users);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new
                {
                    message = "User Created Successfully"
                });
            }
            var errors = result.Errors.Select(error => error.Description);
            return BadRequest(new { error = errors });
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });
            return Ok(new
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }


        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] EditUserDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new { message = "User updated successfully." });
                }
                return BadRequest(new
                {
                    errors = result.Errors.Select(x => x.Description)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { error = "User Not found" });
                }
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new { message = "User deleted successfully." });
                }
                return BadRequest(new
                {
                    errors = result.Errors.Select(x => x.Description)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}