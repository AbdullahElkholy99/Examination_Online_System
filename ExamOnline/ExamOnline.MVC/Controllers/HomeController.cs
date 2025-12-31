using System.Diagnostics;
using ExamOnline.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamOnline.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SelectRole(string role)
        {
            if (role == "student")
                return RedirectToAction("Dashboard", "Student");

            if (role == "admin")
                return RedirectToAction("Dashboard", "Admin");

            return RedirectToAction("Index");
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
