namespace WebProje.Models
{
    public class Islem
    {
        public int Id { get; set; }
        public string Ad { get; set; } // İşlem adı
        public double Ucret { get; set; } // İşlem ücreti
        public int Sure { get; set; } // İşlem süresi (dakika)
    }
}
