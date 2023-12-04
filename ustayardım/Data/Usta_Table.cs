using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class Usta_Table
    {
        [Key]
        public int UstaId { get; set; }
        public int UserId { get; set; } // User tablosuna referans
        public int Puan { get; set; }
        public string? Hakkinda { get; set; }
        public string? TamamlananIs { get; set; }
    }
}