using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Email == email && a.Password == password);

            if (admin != null)
            {
                TempData["SuccessMessage"] = "Giriş başarılı.";
                return RedirectToAction("Dashboard", "Admin");
            }

            TempData["ErrorMessage"] = "Geçersiz e-posta veya şifre.";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
