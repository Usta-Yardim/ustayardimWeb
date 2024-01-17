using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class AccountModel
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("user")]
        public UserDetails User { get; set; } = null!;
        public List<IllerDTO>? IlListesi { get; set; }
        public List<IlcelerDTO>? IlceListesi { get; set; }
        public List<MahallelerDTO>? MahalleListesi { get; set; }
        
        [JsonPropertyName("ilinfo")]
        public IllerDTO? Il { get; set; }

        [JsonPropertyName("ilceinfo")]
        public IlcelerDTO? Ilce { get; set; }
        [JsonPropertyName("mahalleinfo")]
        public MahallelerDTO? Mahalle { get; set; }
        
        [JsonIgnore]
        public IFormFile ProfilImgBase64 { get; set; } = null!;

        [JsonPropertyName("profilImgPath")]
        public string? ProfilImgPath { get; set; }
        [JsonPropertyName("puan")]
        public int? Puan { get; set; }
        [JsonPropertyName("hakkinda")]
        public string? Hakkinda { get; set; }
        [JsonPropertyName("birthday")]
        public DateTime? Birthday { get; set; }
        [JsonPropertyName("tamamlananIs")]
        public string? TamamlananIs { get; set; }
        [JsonIgnore]
        public List<IFormFile>? ReferansImgBase64 { get; set; } 

        [JsonPropertyName("referansImgPath")]
        public List<string>? ReferansImgPath { get; set; } = new List<string>();
        [JsonPropertyName("activeTabPane")]
        public string ActiveTabPane { get; set; } = "#account-general";
        [Required(ErrorMessage = "Mevcut Şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        [JsonPropertyName("oldPassword")]
        public string? OldPassword { get; set; }
         [Required(ErrorMessage = "Yeni Şifre alanı boş bırakılamaz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        [JsonPropertyName("newPassword")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "Yeni Şifreyi Tekrar Girin alanı boş bırakılamaz.")]
        [Compare("NewPassword", ErrorMessage = "Girilen yeni şifreler uyuşmuyor.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifreyi Tekrar Girin")]
        public string? ConfirmNewPassword { get; set; }
        public bool? error { get; set; }
        public bool? succes { get; set; }
    }

}