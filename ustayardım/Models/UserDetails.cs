using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
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