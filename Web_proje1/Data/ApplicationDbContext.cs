using Microsoft.EntityFrameworkCore;
using Web_proje1.Models;

namespace Web_proje1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // ıletisim tablosunu tanımlıyoruz.
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<UzmanlikAlani> UzmanlikAlanlari { get; set; }
        public DbSet<UygunlukSaati> UygunlukSaatleri { get; set; }

    }
}
