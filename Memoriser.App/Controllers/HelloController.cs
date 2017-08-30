using Microsoft.AspNetCore.Mvc;

namespace Memoriser.App.Controllers
{
    [Route("api/[controller]")]
    public class HelloController : Controller
    {
        [HttpGet]
        public string GetHello()
        {
            return "Hello.";
        }
    }
}
