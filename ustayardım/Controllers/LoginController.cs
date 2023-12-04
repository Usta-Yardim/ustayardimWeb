using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Musteri()
        {
            return View("LoginMusteri");
        }


        public IActionResult Usta()
        {
            return View("LoginUsta");
        }
    }
}