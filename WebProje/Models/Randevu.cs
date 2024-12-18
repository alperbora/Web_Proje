namespace WebProje.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int CalisanId { get; set; }
        public Calisan Calisan { get; set; }
        public DateTime RandevuZamani { get; set; }
        public string Islem { get; set; }
        public decimal Ucret { get; set; }
    }
}
