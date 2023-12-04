using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ustayardım.Data
{
    public class Kategoriler
    {
        [Key]
        public int KategoriId { get; set; }
        public string? KategoriName { get; set; }
    }
}