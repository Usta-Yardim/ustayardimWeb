using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View("Login");
        }


        public IActionResult Login()
        {
            return View("Login");
        }
    }
}