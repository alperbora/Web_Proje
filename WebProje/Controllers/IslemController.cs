using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebProje.Controllers
{
    public class IslemController : Controller
    {
        private readonly AppDbContext _context;

        public IslemController(AppDbContext context)
        {
            _context = context;
        }

        // İşlem Listesi
        public async Task<IActionResult> Index()
        {
            var islemler = await _context.Islemler.ToListAsync();
            return View(islemler);
        }

        // Yeni İşlem Ekle - GET
        public IActionResult Create()
        {
            return View();
        }

        // Yeni İşlem Ekle - POST
        [HttpPost]
        public async Task<IActionResult> Create(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Add(islem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(islem);
        }

        // İşlem Sil - GET
        public async Task<IActionResult> Delete(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem == null)
                return NotFound();

            return View(islem);
        }

        // İşlem Sil - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem != null)
            {
                _context.Islemler.Remove(islem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
