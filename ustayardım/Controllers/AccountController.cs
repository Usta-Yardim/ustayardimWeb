using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet("Account/{id}")]
        public async Task<IActionResult> Account(int id)
        {
            
            var Usta = await GetUsta(id);
            
            if(Usta != null){
                await GetAddressData(Usta);
                await KategoriController.GetKategoriler(Usta!);
                return View(Usta);
            }

            return View();
            
        }
        [HttpGet("Musteri/{id}")]
        public async Task<IActionResult> AccountMusteri(int id)
        {
            var Musteri = await GetMusteri(id);
            if(Musteri != null){
                await GetAddressData(Musteri);
                await KategoriController.GetKategoriler(Musteri!);
                return View(Musteri);
            }

            return View();
            
        }

        [HttpPost("Account/{id}")]
        public async Task<IActionResult> Account(int id, AccountModel updatedModel)
        {
            if(updatedModel.ActiveTabPane == "#account-change-password"){
                if (!ModelState.IsValid){
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();
                await GetAddressData(updatedModel);
                return View("Account", updatedModel);
                }
            }
            
            if (id != updatedModel.UserId)
            {
                return BadRequest(); // Yanlış id ile gelen istekleri reddet
            }

            using var httpClient = new HttpClient();
            MemoryStream memoryStream = new MemoryStream();
            
            if(updatedModel.ProfilImgBase64 != null){
                updatedModel.ProfilImgBase64?.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                updatedModel.ProfilImgPath = Convert.ToBase64String(bytes);
            }
            
            
            updatedModel.ReferansImgPath = new List<string>();
            if(updatedModel.ReferansImgBase64 != null){
                for(int i = 0; i < updatedModel.ReferansImgBase64.Count; i++){
                    using (MemoryStream memoryStreams = new MemoryStream())
                    {
                        await updatedModel.ReferansImgBase64[i].CopyToAsync(memoryStreams);
                        memoryStreams.Seek(0, SeekOrigin.Begin);
                        byte[] bytess = memoryStreams.ToArray();
                        string base64String = Convert.ToBase64String(bytess);
                        updatedModel.ReferansImgPath.Add(base64String);
                    }
                }
            }
            
            var json = JsonSerializer.Serialize(updatedModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"http://localhost:5120/api/Account/{id}",data))
            {
                if (response.IsSuccessStatusCode)
               {
                    var Usta = await GetUsta(id);
                    await KategoriController.GetKategoriler(Usta!);
#pragma warning disable CS8602 // Olası bir null başvurunun başvurma işlemi.
                    Usta.succes = true;
#pragma warning restore CS8602
                    return View(Usta);      // Hata mesajıda göster kullanıcıya güncellenemedi diye ve güncellenen değerleri sıfırla dbdeki verriyi getir.
                }
                else
                {
                    var Usta = await GetUsta(id);
                    await KategoriController.GetKategoriler(Usta!);
#pragma warning disable CS8602 // Olası bir null başvurunun başvurma işlemi.
                    Usta.error = true;
#pragma warning restore CS8602
                    return View(Usta);
                }
            }
        }

        [HttpPost("Account/MusteriUpdate/{id}")]
        public async Task<IActionResult> MusteriUpdate(int id, AccountModel updatedModel)
        {
            if(updatedModel.ActiveTabPane == "#account-change-password"){
                if (!ModelState.IsValid){
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                         .Select(e => e.ErrorMessage)
                                         .ToList();
                await GetAddressData(updatedModel);
                return View("AccountMusteri", updatedModel);
                }
            }
            
            if (id != updatedModel.UserId)
            {
                return BadRequest(); // Yanlış id ile gelen istekleri reddet
            }

            using var httpClient = new HttpClient();
            MemoryStream memoryStream = new MemoryStream();
            
            if(updatedModel.ProfilImgBase64 != null){
                updatedModel.ProfilImgBase64?.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                updatedModel.ProfilImgPath = Convert.ToBase64String(bytes);
            }
            
            var json = JsonSerializer.Serialize(updatedModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"http://localhost:5120/api/Account/MusteriUpdate/{id}",data))
            {
                if (response.IsSuccessStatusCode)
               {
                    var Musteri = await GetMusteri(id);
                    await KategoriController.GetKategoriler(Musteri!);
#pragma warning disable CS8602 // Olası bir null başvurunun başvurma işlemi.
                    Musteri.succes = true;
#pragma warning restore CS8602
                    return View("AccountMusteri",Musteri);      // Hata mesajıda göster kullanıcıya güncellenemedi diye ve güncellenen değerleri sıfırla dbdeki verriyi getir.
                }
                else
                {
                    var Musteri = await GetMusteri(id);
                    await KategoriController.GetKategoriler(Musteri!);
#pragma warning disable CS8602 // Olası bir null başvurunun başvurma işlemi.
                    Musteri.error = true;
#pragma warning restore CS8602
                    return View("AccountMusteri",Musteri);
                }
            }
        }

        public async Task GetAddressData(AccountModel Kullanıcı)
        {
            using var httpClient = new HttpClient();

            var responseIl = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Iller");
            var responseDataIl = await responseIl.Content.ReadAsStringAsync();

            if (Kullanıcı != null ){
                Kullanıcı.IlListesi = JsonSerializer.Deserialize<List<IllerDTO>>(responseDataIl);
                ViewBag.IlListesi = new SelectList(Kullanıcı.IlListesi,"IlId","IlAdi");


                if(Kullanıcı.Il != null){
                    var ilId = Kullanıcı.Il.IlId;
                    var responseIlce = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Ilceler/{ilId}");
                    var responseDataIlce = await responseIlce.Content.ReadAsStringAsync();
                    Kullanıcı.IlceListesi = JsonSerializer.Deserialize<List<IlcelerDTO>>(responseDataIlce);
                    ViewBag.IlceListesi = new SelectList(Kullanıcı.IlceListesi,"IlceId","IlceAdi");
                }

                if(Kullanıcı.Ilce != null){
                    var ilceId = Kullanıcı.Ilce.IlceId;
                    var responseMahalle = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Mahalleler/{ilceId}");
                    var responseDataMahalle = await responseMahalle.Content.ReadAsStringAsync();
                    Kullanıcı.MahalleListesi = JsonSerializer.Deserialize<List<MahallelerDTO>>(responseDataMahalle);
                    ViewBag.MahalleListesi = new SelectList(Kullanıcı.MahalleListesi,"MahalleId","MahalleAdi");
                }
        
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

            if (account != null)
            {
                await GetAddressData(account);
            }

            ModelState.Clear();
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