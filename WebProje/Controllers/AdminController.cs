using Microsoft.AspNetCore.Mvc;
using WebProje.Models;
using WebProje.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebProje.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Kayıt Ol - GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Kayıt Ol - POST
        [HttpPost]
        public async Task<IActionResult> Register(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var existingAdmin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == admin.Email);
                if (existingAdmin != null)
                {
                    ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                    return View(admin);
                }

                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View(admin);
        }

        // Login - GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login - POST
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var existingAdmin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email == admin.Email && a.Password == admin.Password);

            if (existingAdmin != null)
            {
                // Kullanıcı oturum açıyor
                return RedirectToAction("Dashboard", "Admin");
            }

            ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
            return View(admin);

        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
