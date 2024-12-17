using Microsoft.AspNetCore.Mvc;
using Web_proje1.Data;
using Web_proje1.Models;

namespace Web_proje1.Controllers
{
    public class IletisimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IletisimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Formu görüntüle
        public IActionResult Index()
        {
            return View();
        }

        // Form gönderildiğinde
        [HttpPost]
        public IActionResult Index(Iletisim Iletisim)
        {
            if (ModelState.IsValid)
            {
                _context.Iletisim.Add(Iletisim);
                _context.SaveChanges();  // Veritabanına kaydet

                TempData["Success"] = "Mesajınız başarıyla gönderildi!";
                return RedirectToAction("Index", "Home");
            }

            return View(Iletisim);
        }
    }
}
