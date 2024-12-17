using System.ComponentModel.DataAnnotations;
namespace Web_proje1.Models
{
    public class UzmanlikAlani
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Uzmanlık alanı adı gereklidir.")]
        public string Ad { get; set; }
        public ICollection<Calisan> Calisanlar { get; set; }
    }
}
