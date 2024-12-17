namespace Web_proje1.Models
{
    public class Iletisim
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Mesaj gönderilme tarihi
    }
}
