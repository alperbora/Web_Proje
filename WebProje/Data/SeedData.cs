using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, AppDbContext context)
        {
            // Veritabanını oluştur (bu adım veritabanının zaten var olup olmadığını kontrol eder)
            context.Database.EnsureCreated();

            // Admin verisi ekleme
            if (!context.Admins.Any())
            {
                context.Admins.Add(new Admin
                {
                    Email = "admin@mail.com",
                    Password = "123",  // Düz metin şifre
                    IsAdmin = true
                });
                context.SaveChanges();
            }
        }
    }
}
