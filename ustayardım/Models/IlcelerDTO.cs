using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ustayardım.Models
{
    public class IlcelerDTO
    {
        [Key]
        [JsonPropertyName("ilceId")]
        public int IlceId { get; set; }
        [JsonPropertyName("ilceAdi")]
        public string? IlceAdi { get; set; }
    }
}