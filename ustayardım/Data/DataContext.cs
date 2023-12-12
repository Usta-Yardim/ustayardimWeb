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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Entity<User_Table>().HasKey(u => new { u.UserId, u.Eposta });
            /*modelBuilder.Entity<AppUser>().HasData(new AppUser() { UserId = 1, UserName = "Sefa", UserSurname = "Demirci", Eposta = "info@info.com", Sifre = "1234", KayitTarihi=DateTime.Now});
            modelBuilder.Entity<AppUser>().HasData(new AppUser() { UserId = 2, UserName = "Ebubekir Alp", UserSurname = "Elvan", Eposta = "info1@info.com", Sifre = "1234", KayitTarihi=DateTime.Now });
            modelBuilder.Entity<AppUser>().HasData(new AppUser() { UserId = 3, UserName = "Iphone 16", UserSurname = "8000", Eposta = "info2@info.com", Sifre = "1234", KayitTarihi=DateTime.Now });
            modelBuilder.Entity<AppUser>().HasData(new AppUser() { UserId = 4, UserName = "Iphone 17", UserSurname = "9000", Eposta = "info3@info.com", Sifre = "1234", KayitTarihi=DateTime.Now });
            modelBuilder.Entity<AppUser>().HasData(new AppUser() { UserId = 5, UserName = "Iphone 18", UserSurname = "10000", Eposta = "info4@info.com", Sifre = "1234", KayitTarihi=DateTime.Now });*/
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