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
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? Eposta { get; set; }
        public string? Sifre { get; set; }
        public int IlId { get; set; } // Sehir tablosuna referans
        public DateTime KayitTarihi { get; set; }
    }
}