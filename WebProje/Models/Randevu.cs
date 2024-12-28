namespace WebProje.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int CalisanId { get; set; } 
        public string Islem { get; set; } 
        public DateTime RandevuSaati { get; set; } 
        public double Ucret { get; set; } 
        public string Durum { get; set; } = "Bekliyor"; 
        public Calisan Calisan { get; set; } 
    }
}
