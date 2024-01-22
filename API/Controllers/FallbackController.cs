using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {

            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
