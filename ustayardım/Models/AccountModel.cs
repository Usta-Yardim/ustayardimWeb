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
        [JsonPropertyName("ilinfo")]
        public IllerDTO? Il { get; set; }
        public List<IllerDTO>? IlListesi { get; set; }
        public List<IlcelerDTO>? IlceListesi { get; set; }
        public List<MahallelerDTO>? MahalleListesi { get; set; }

        [JsonPropertyName("ilceinfo")]
        public IlcelerDTO? Ilce { get; set; }
        [JsonPropertyName("mahalleinfo")]
        public MahallelerDTO? Mahalle { get; set; }
        
        [JsonIgnore]
        public IFormFile ProfilImgBase64 { get; set; } = null!;

        [JsonPropertyName("profilImgPath")]
        public string ProfilImgPath { get; set; } = null!;
        [JsonPropertyName("puan")]
        public int? Puan { get; set; }
        [JsonPropertyName("hakkinda")]
        public string? Hakkinda { get; set; }
        [JsonPropertyName("birthday")]
        public DateTime? Birthday { get; set; }
        [JsonPropertyName("tamamlananIs")]
        public string? TamamlananIs { get; set; }
        [JsonIgnore]
        public List<IFormFile> ReferansImgBase64 { get; set; } = null!;

        [JsonPropertyName("referansImgPath")]
        public List<string>? ReferansImgPath { get; set; } = new List<string>();
        [JsonPropertyName("activeTabPane")]
        public string ActiveTabPane { get; set; } = "#account-general";
        [JsonPropertyName("oldPassword")]
        public string OldPassword { get; set; } = null!;
        [JsonPropertyName("newPassword")]
        public string NewPassword { get; set; } = null!;
    }

    public class UserDetails
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;
         [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = null!;
        [JsonPropertyName("userType")]
        public string UserType { get; set; } = null!;
    
    }
}