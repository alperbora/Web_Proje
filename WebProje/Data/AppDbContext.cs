using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
