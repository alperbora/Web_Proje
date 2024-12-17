using System.ComponentModel.DataAnnotations;
namespace Web_proje1.Models;

    public class UygunlukSaati
{
    public int Id { get; set; }
    public int CalisanId { get; set; }
    public Calisan Calisan { get; set; }
    [Required]public DateTime BaslangicSaati { get; set; }
    [Required]public DateTime BitisSaati { get; set; }
}
