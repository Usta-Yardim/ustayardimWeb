using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }  = null!;
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}