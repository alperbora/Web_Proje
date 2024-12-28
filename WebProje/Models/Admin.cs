using Microsoft.EntityFrameworkCore;

namespace WebProje.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Email = "b221210307@sakarya.edu.tr",
                    Password = "sau",
                    IsAdmin = true
                }
            );
        }
    }
}
