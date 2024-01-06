using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class IllerDTO
    {
        [Key]
        [JsonPropertyName("ilId")]
        public int IlId { get; set; }
        [JsonPropertyName("ilAdi")]
        public string? IlAdi { get; set; }
    }
}