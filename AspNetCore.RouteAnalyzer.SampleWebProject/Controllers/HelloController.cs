using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.RouteAnalyzer.SampleWebProject.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult World()
        {
            return View();
        }
    }
}