using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Data;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class UserController: Controller
    {
        private readonly DataContext _context;
        
        public UserController(DataContext context) // injection methodu deniyor buna üstteki context ile
        {
            _context = context;
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterDTO model)
        {
            using (var httpClient = new HttpClient())
            {
            var apiUrl = "http://localhost:5120/api/Users/register"; // API endpoint URL'sini buraya ekle

            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Kullanıcı başarıyla oluşturulduğunda işlemler yapılabilir
                return RedirectToAction("Usta", "Login");
            }
            else
            {
                // Kullanıcı oluşturma başarısız oldu
                // İşleme göre hata mesajı veya işlem gerçekleştirebilirsiniz
                ModelState.AddModelError("", "Kullanıcı oluşturulurken bir hata oluştu.");
                return View("Login/Usta"); // Hata durumunda aynı sayfayı tekrar göstermek
            }
        }
    }

    }
}