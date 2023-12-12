using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class User_Table
    {
        [Key]
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        [Key]
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public int IlId { get; set; } // Sehir tablosuna referans
        public DateTime KayitTarihi { get; set; }
    }
}