using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ustayardım.Controllers
{
    public class AccountController: Controller
    {
        public IActionResult Account()
        {
            return View();
        }

    }
}