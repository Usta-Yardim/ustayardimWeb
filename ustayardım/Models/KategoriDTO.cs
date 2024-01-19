using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class KategoriDTO
    {
        [JsonPropertyName("id")]
        public int KategoriId { get; set; }
        [JsonPropertyName("kategoriName")]
        public string? KategoriName { get; set; }
    }
}