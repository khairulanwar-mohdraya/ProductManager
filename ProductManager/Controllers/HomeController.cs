using Microsoft.AspNetCore.Mvc;

namespace ProductManager.Controllers
{
    [Route("[controller]/")]
    [Controller]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("Main")]
        public IActionResult Main()
        {
            //you code here
            return View();
        }
    }
}
