using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class Yorumlar_Table
    {
        [Key]
        public int YorumId { get; set; }
        public int UstaId { get; set; } // Usta tablosuna referans
        public string? YorumDetaylari { get; set; }
    }
}