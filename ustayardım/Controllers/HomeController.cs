using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("Login");
        }
    }
}