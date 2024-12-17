namespace Web_proje1.Models;

    public class Calisan
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public ICollection<UzmanlikAlani> UzmanlikAlanlari { get; set; }
    public ICollection<UygunlukSaati> UygunlukSaatleri { get; set; }
}
