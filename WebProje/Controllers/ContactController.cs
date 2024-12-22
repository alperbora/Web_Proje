using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Submit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.SubmittedAt = DateTime.Now;  // Mesajın gönderilme tarihi
                _context.Contacts.Add(contact);  // Veritabanına ekleniyor
                _context.SaveChanges();  // Değişiklik kaydediliyor
                return RedirectToAction("Index", "Home");  // Ana sayfaya yönlendir
            }

            return View("Index");  // Formu tekrar göster
        }
    }
}
