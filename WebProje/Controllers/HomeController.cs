using Microsoft.AspNetCore.Mvc;
using WebProje.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;


namespace WebProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor'da AppDbContext'i inject ediyoruz
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Salonlar tablosundan tüm verileri çekiyoruz
            var salonlar = await _context.Salonlar.ToListAsync();

            // Veriyi View'a gönderiyoruz
            return View(salonlar);
        }


        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


    }
}
