using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ustayardım.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {            
        }
        public DbSet<User_Table> Users { get; set; }
        public DbSet<Usta_Table> Ustalar { get; set; }
        public DbSet<Musteri_Table> Musteriler { get; set; }
        public DbSet<Sehirler> Sehirler { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Galeri_Table> Galeri_Tables { get; set; }
        public DbSet<Evulation_Table> Evulation_Tables { get; set; }
        public DbSet<Yorumlar_Table> Yorumlar_Tables { get; set; }
    }
}