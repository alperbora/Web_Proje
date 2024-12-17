namespace Web_proje1.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime RandevuZamani { get; set; }
        public string Islem { get; set; }
        public int Sure { get; set; } // Süre dakika cinsinden
        public decimal Ucret { get; set; }
        public bool OnayliMi { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
    }

}
