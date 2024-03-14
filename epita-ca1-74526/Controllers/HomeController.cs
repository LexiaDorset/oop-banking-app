using epita_ca1_74526.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
// Name: Lucile Pelou
// Student number: 74526
namespace epita_ca1_74526.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// Initializes a new instance of the <see cref="HomeController"/> class.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// Displays the index page.
        public IActionResult Index()
        {
            return View();
        }

        /// Displays the privacy page.
        public IActionResult Privacy()
        {
            return View();
        }

        /// Displays the error page.
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
