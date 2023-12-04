using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class Sehirler
    {
        [Key]
        public int SehirId { get; set; }
        public string? Il { get; set; }
        public string? Ilce { get; set; }   
    }
}