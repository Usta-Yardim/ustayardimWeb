using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class KategoriController : Controller
    {
        public async Task<IActionResult> Kategori(int id, int UserId , string UserType)
        {
            List<AccountModel>? ustalar = new List<AccountModel>();
            List<AccountModel>? ustalarFilter = new List<AccountModel>();
            var User = new AccountModel();
           if(UserType == "usta"){
                User = await GetUsta(UserId);
           }else{
                User = await GetMusteri(UserId);
           }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Kategoriler/{id}"))
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
            await GetKategoriler(User!);
            User!.KategoriName = User?.KategoriListesi?.FirstOrDefault(item => item.KategoriId == id)?.KategoriName;
            return View(User);
        }
        public async Task<IActionResult> KategoriIndex(int id)
        {
            List<AccountModel>? ustalar = new List<AccountModel>();
            List<AccountModel>? ustalarFilter = new List<AccountModel>();
            List<KategoriDTO>? Kategoriler = new List<KategoriDTO>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Kategoriler/{id}"))
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
            Kategoriler = await HomeController.GetKategoriler();
            ViewBag.Kategoriler = Kategoriler;
            foreach (var item in Kategoriler!)
            {
                if(item.KategoriId == id){
                    ViewBag.KategoriName = item.KategoriName;
                }
            }
            return View();
        }
        public static async Task GetKategoriler(AccountModel Kullanıcı)
        {
             using var httpClient = new HttpClient();

            var responseKategori = await httpClient.GetAsync($"http://localhost:5120/api/Kategoriler");
            var responseDataKategori = await responseKategori.Content.ReadAsStringAsync();

            if (Kullanıcı != null ){
                Kullanıcı.KategoriListesi = JsonSerializer.Deserialize<List<KategoriDTO>>(responseDataKategori);
            }
        }

        public async Task<AccountModel?> GetAccount(int id, string endpoint)
        {
            var account = new AccountModel();
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Account/{endpoint}/{id}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    account = JsonSerializer.Deserialize<AccountModel>(responseData);
                }
            }

            return account;
        }
        public async Task<AccountModel?> GetUsta(int id)
        {
            return await GetAccount(id, "Ustalar");
        }
        public async Task<AccountModel?> GetMusteri(int id)
        {
            return await GetAccount(id, "Musteri");
        }
    }

    
}