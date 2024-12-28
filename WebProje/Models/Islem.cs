namespace WebProje.Models
{
    public class Islem
    {
        public int Id { get; set; }
        public string Ad { get; set; } 
        public double Ucret { get; set; } 
        public int Sure { get; set; } // İşlem süresi (dakika)
    }
}
