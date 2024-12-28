using Microsoft.AspNetCore.Mvc;
using WebProje.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using Microsoft.AspNetCore.Authorization;


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

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult UserDashboard()
        {
            return View();
        }
    }
}
