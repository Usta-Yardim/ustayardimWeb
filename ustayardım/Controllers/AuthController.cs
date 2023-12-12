using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ustayardım.Models;

namespace ustayardım.Controllers
{
    public class AuthController : Controller
    {

        public IActionResult Musteri()
        {
            var model = new AuthViewModel
            {
                LoginModel = new LoginDTO{
                    UserType = "musteri"
                },
                RegisterModel = new RegisterDTO{
                    UserType = "musteri"
                },
                ActionType = "LoginUser" // Eğer ActionType kullanıyorsanız bu kısmı doldurun
            };
            return View("LoginUser",model);
        }


        public IActionResult Usta()
        {
            var model = new AuthViewModel
            {
                LoginModel = new LoginDTO{
                    UserType = "usta"
                },
                RegisterModel = new RegisterDTO{
                    UserType = "usta"
                },
                ActionType = "LoginUser" // Eğer ActionType kullanıyorsanız bu kısmı doldurun
            };
            return View("LoginUser",model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AuthViewModel model)
        {
            model.LoginModel = new LoginDTO
            {
                UserType = model.RegisterModel.UserType
            }; // Yeni bir RegisterDTO nesnesi oluşturun


            using (var httpClient = new HttpClient())
            {
            var apiUrl = "http://localhost:5120/api/Users/register"; // API endpoint URL'sini buraya ekle

            var json = JsonSerializer.Serialize(model.RegisterModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Kullanıcı başarıyla oluşturulduğunda işlemler yapılabilir
                if(model.RegisterModel.UserType == "usta"){
                    return RedirectToAction("usta");
                }
                else if(model.RegisterModel.UserType == "musteri"){
                    return RedirectToAction("musteri");
                }
                else{
                    return View("LoginUser",model);
                }
                
            }
            else
            {
                // Kullanıcı oluşturma başarısız oldu
                // İşleme göre hata mesajı veya işlem gerçekleştirebilirsiniz
                ModelState.AddModelError("RegisterModel", "Kullanıcı oluşturulurken bir hata oluştu.");
                ModelState.Remove("LoginModel");
                return View("LoginUser",model); // Hata durumunda aynı sayfayı tekrar göstermek // apiden gelen hatalarla gönder alanların üsünde de belirtebiliriz spesifik hataları
            }
        }
    }
        [HttpPost]
        public async Task<IActionResult> LoginUser(AuthViewModel model)
        {
            model.RegisterModel = new RegisterDTO
            {
                UserType = model.LoginModel.UserType
            }; // Yeni bir RegisterDTO nesnesi oluşturun


            using (var httpClient = new HttpClient())
            {
            var apiUrl = "http://localhost:5120/api/Users/login"; // API endpoint URL'sini buraya ekle

            var json = JsonSerializer.Serialize(model.LoginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);
            var apiResponse = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<TokenResponseDTO>(apiResponse, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
                
                if (token != null && !string.IsNullOrEmpty(token.Token))
                {
                    // token kullanılabilir, null değil
                    // token.AccessToken gibi token özelliklerine erişebilirsiniz
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTimeOffset.Now.AddDays(30), // Örnek olarak 1 gün saklanması
                        SameSite = SameSiteMode.Strict,
                        Secure = true // HTTPS üzerinden çalıştığında aktifleştir
                    };
                    // Token'ı kullanarak gerekli işlemleri yapabilirsiniz
                    Response.Cookies.Append("AuthToken", token.Token, cookieOptions);
                    if(model.LoginModel.UserType == "usta"){
                        return RedirectToAction("ustaPage","UstaPage");
                    }
                    else{
                        return RedirectToAction("musteriPage","MusteriPage");
                    }
                    
                }
            }
            // Kullanıcı girişi başarısız oldu
            // İşleme göre hata mesajı veya işlem gerçekleştirebilirsiniz
            var message = JsonSerializer.Deserialize<ResponseDTO>(apiResponse, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});
            if(message == null){
                message = new ResponseDTO{
                 Message = "E-posta veya şifre yanlış"
                };
            }
            ModelState.AddModelError("LoginModel", message.Message); // apiden gelen hatayı gönder
            ModelState.Remove("RegisterModel");
            // Hata durumunda aynı sayfayı tekrar göstermek    
            return View(model);
            }
        }
        
    }
}