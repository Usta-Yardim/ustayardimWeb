using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class Musteri_Table
    {
        [Key]
        public int MusteriId { get; set; }
        public int UserId { get; set; } // User tablosuna referans
        public int FavoriUstaId { get; set; } // Usta tablosuna referans
    }
}