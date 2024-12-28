using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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

                // Şifreyi olduğu gibi kaydet
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
            // E-posta ve şifreyi veritabanında kontrol et
            var existingAdmin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Email.ToLower() == admin.Email.ToLower());

            // Eğer email veritabanında yoksa veya şifre yanlışsa
            if (existingAdmin == null || existingAdmin.Password != admin.Password)
            {
                TempData["ErrorMessage"] = "Geçersiz e-posta veya şifre.";
                return RedirectToAction("Login");
            }

            // Kullanıcı için claim'leri oluştur
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingAdmin.Email)
            };

            // Admin olup olmadığını kontrol et ve ona göre rol belirle
            if (existingAdmin.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User")); // Normal kullanıcı rolü
            }

            // ClaimsIdentity oluşturuluyor ve CookieAuthentication ile giriş yapılıyor
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Admin'e yönlendirme, değilse normal kullanıcıya yönlendirme
            if (claims.Any(c => c.Value == "Admin"))
            {
                return RedirectToAction("Dashboard", "Admin"); // Admin Dashboard
            }
            else
            {
                return RedirectToAction("UserDashboard", "Home"); // Kullanıcı Dashboard
            }
        }

        // Admin Dashboard - Admin rolüne sahip kullanıcılar erişebilir
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
