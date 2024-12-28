namespace WebProje.Models
{
    public class Calisan
    {
        public int Id { get; set; } 
        public string AdSoyad { get; set; } 
        public string UzmanlikAlani { get; set; } 
        public string UygunSaatler { get; set; }
        public List<string> Beceriler { get; set; } = new List<string>(); 
    }
}
