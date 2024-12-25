using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebProje.Controllers
{
    public class CalisanController : Controller
    {
        private readonly AppDbContext _context;

        public CalisanController(AppDbContext context)
        {
            _context = context;
        }

        // Index - Çalışanları listele
        public async Task<IActionResult> Index()
        {
            var calisanlar = await _context.Calisanlar.ToListAsync(); // Asenkron işlem
            return View(calisanlar);
        }

        // Create - Yeni çalışan ekleme
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calisan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calisan);
        }

        // Edit - Çalışan bilgilerini düzenleme
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdSoyad,UzmanlikAlani,UygunSaatler,Beceriler")] Calisan calisan)
        {
            if (id != calisan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisan);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    if (!CalisanExists(calisan.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calisan);
        }

        // Details - Çalışan detaylarını görüntüleme
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        // Delete - Çalışan silme
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calisan == null)
            {
                return NotFound();
            }

            return View(calisan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisan = await _context.Calisanlar.FindAsync(id);
            _context.Calisanlar.Remove(calisan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanExists(int id)
        {
            return _context.Calisanlar.Any(e => e.Id == id);
        }
    }
}
