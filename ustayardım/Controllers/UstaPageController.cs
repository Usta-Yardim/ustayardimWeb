using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class UstaPageController : Controller
    {
        public async Task<IActionResult> UstaPageAsync(int id)
        {
            var Usta = new AccountModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Account/Ustalar/{id}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Usta = JsonSerializer.Deserialize<AccountModel>(responseData);

                }
            }
            return View(Usta);
        }
    }
}