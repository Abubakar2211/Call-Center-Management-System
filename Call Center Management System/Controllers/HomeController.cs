using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Call_Center_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            Console.WriteLine("Hello World");
            return Ok("Hello World");
        }
    }
}
