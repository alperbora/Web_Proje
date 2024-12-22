namespace WebProje.Models
{
    public class Contact
    {
        public int Id { get; set; }  // Bu, veritabanında benzersiz bir ID olacaktır
        public string Name { get; set; }  // Ad
        public string Email { get; set; }  // E-posta
        public string Message { get; set; }  // Mesaj
        public DateTime SubmittedAt { get; set; }  // Mesaj gönderildiği tarih
    }
}
