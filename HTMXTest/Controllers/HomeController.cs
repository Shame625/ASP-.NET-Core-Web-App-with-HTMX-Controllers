using HTMXTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HTMXTest.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return ViewOrPartial("Index");
        }

        public IActionResult Privacy()
        {
            return ViewOrPartial("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
