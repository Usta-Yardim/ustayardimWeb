using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class MusteriPageController : Controller
    {
        public async Task<IActionResult> MusteriPageAsync(int id)
        {
            var Musteri = new AccountModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Account/Musteri/{id}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Musteri = JsonSerializer.Deserialize<AccountModel>(responseData);
                }
            }
            return View(Musteri);
        }
    }
}