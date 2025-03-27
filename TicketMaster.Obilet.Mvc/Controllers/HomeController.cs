using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketMaster.Obilet.Mvc.Models;

namespace TicketMaster.Obilet.Mvc.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController(ILogger<HomeController> logger):base(logger)
        {
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index page visited");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
