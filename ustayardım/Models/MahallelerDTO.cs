using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class MahallelerDTO
    {
        [Key]
        [JsonPropertyName("mahalleId")]
        public int MahalleId { get; set; }
        [JsonPropertyName("mahalleAdi")]
        public string? MahalleAdi { get; set; }
    }
}