namespace WebProje.Models
{
    public class Calisan
    {
        public int Id { get; set; } // Çalışan ID'si
        public string AdSoyad { get; set; } // Çalışanın tam adı
        public string UzmanlikAlani { get; set; } // Uzmanlık alanı
        public string UygunSaatler { get; set; } // Uygun saatler (örneğin: "09:00-12:00, 13:00-17:00")
        public List<string> Beceriler { get; set; } = new List<string>(); // Uzmanlık becerileri
    }
}
