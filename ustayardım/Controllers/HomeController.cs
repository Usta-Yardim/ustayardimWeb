using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class HomeController : Controller
    {

        public async Task<IActionResult> IndexAsync()
        {
            List<AccountModel>? ustalar = new List<AccountModel>();
            List<AccountModel>? ustalarFilter = new List<AccountModel>();
            List<KategoriDTO>? Kategoriler = new List<KategoriDTO>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Account/Ustalar"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    ustalarFilter = JsonSerializer.Deserialize<List<AccountModel>>(responseData);
                }
            }
            foreach (var usta in ustalarFilter!)
            {
                if(usta.Il != null && usta.Il!.IlAdi != null && usta.ProfilImgPath != null && usta.User.PhoneNumber != null){
                    ustalar.Add(usta);
                }
            }

            ViewBag.Ustalar = ustalar;
            Kategoriler = await GetKategoriler();
            ViewBag.Kategoriler = Kategoriler;
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

        public static async Task<List<KategoriDTO>?> GetKategoriler()
        {
            using var httpClient = new HttpClient();
            
            List<KategoriDTO>? KategoriListesi = new List<KategoriDTO>();
            var responseKategori = await httpClient.GetAsync($"http://localhost:5120/api/Kategoriler");
            var responseDataKategori = await responseKategori.Content.ReadAsStringAsync();

            if (responseDataKategori != null ){
                KategoriListesi = JsonSerializer.Deserialize<List<KategoriDTO>>(responseDataKategori);
            }

            return KategoriListesi;
        }
    }
}