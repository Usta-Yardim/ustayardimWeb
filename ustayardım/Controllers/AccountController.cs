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
            
            var Usta = new AccountModel();
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"http://localhost:5120/api/Account/Ustalar/{id}"))
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Usta = JsonSerializer.Deserialize<AccountModel>(responseData);

                }
            }
            if(Usta != null){
                await GetAddressData(Usta);
                return View(Usta);
            }

            return View();
            
        }

        [HttpPost("Account/{id}")]
        public async Task<IActionResult> Account(int id, AccountModel updatedModel)
        {
            if (id != updatedModel.UserId)
            {
                return BadRequest(); // Yanlış id ile gelen istekleri reddet
            }

            using var formData = new MultipartFormDataContent();
            using var httpClient = new HttpClient();
            MemoryStream memoryStream = new MemoryStream();

            updatedModel.ProfilImgBase64?.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            updatedModel.ProfilImgPath = Convert.ToBase64String(bytes);
            

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
                    var Usta = new AccountModel();
                    string responseData = await response.Content.ReadAsStringAsync();
                    Usta = JsonSerializer.Deserialize<AccountModel>(responseData);
                    if (Usta != null){
                        await GetAddressData(Usta);
                    }
                    return View(Usta); // Başarılıysa yönlendirme
                }
                else
                {
                    return View(updatedModel); // Hata olursa yönlendirme
                    
                    // Hata mesajıda göster kullanıcıya güncellenemedi diye ve güncellenen değerleri sıfırla dbdeki verriyi getir.
                }
            }
            
        }

        public async Task GetAddressData(AccountModel Usta)
        {
            using var httpClient = new HttpClient();

            var responseIl = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Iller");
            var responseDataIl = await responseIl.Content.ReadAsStringAsync();

            if (Usta != null ){
                Usta.IlListesi = JsonSerializer.Deserialize<List<IllerDTO>>(responseDataIl);
                ViewBag.IlListesi = new SelectList(Usta.IlListesi,"IlId","IlAdi");


                if(Usta.Il != null){
                    var ilId = Usta.Il.IlId;
                    var responseIlce = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Ilceler/{ilId}");
                    var responseDataIlce = await responseIlce.Content.ReadAsStringAsync();
                    Usta.IlceListesi = JsonSerializer.Deserialize<List<IlcelerDTO>>(responseDataIlce);
                    ViewBag.IlceListesi = new SelectList(Usta.IlceListesi,"IlceId","IlceAdi");
                }

                if(Usta.Ilce != null){
                    var ilceId = Usta.Ilce.IlceId;
                    var responseMahalle = await httpClient.GetAsync($"http://localhost:5120/api/Adress/Mahalleler/{ilceId}");
                    var responseDataMahalle = await responseMahalle.Content.ReadAsStringAsync();
                    Usta.MahalleListesi = JsonSerializer.Deserialize<List<MahallelerDTO>>(responseDataMahalle);
                    ViewBag.MahalleListesi = new SelectList(Usta.MahalleListesi,"MahalleId","MahalleAdi");
                }
        
            }
        }

    }
}