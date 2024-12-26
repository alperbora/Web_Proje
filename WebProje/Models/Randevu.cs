namespace WebProje.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public int CalisanId { get; set; } // Çalışan ID
        public string Islem { get; set; } // İşlem adı
        public DateTime RandevuSaati { get; set; } // Randevu saati
        public double Ucret { get; set; } // Ücret
        public string Durum { get; set; } = "Bekliyor"; // Durum: Bekliyor, Onaylandı vb.
        public Calisan Calisan { get; set; } // Çalışanla ilişki
    }
}
