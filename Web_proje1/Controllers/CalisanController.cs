using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_proje1.Data;
using Web_proje1.Models;

public class CalisanController : Controller
{
    private readonly ApplicationDbContext _context;

    public CalisanController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Çalışanları listeleme
    public IActionResult Index()
    {
        var calisanlar = _context.Calisanlar.Include(c => c.UzmanlikAlanlari).Include(c => c.UygunlukSaatleri).ToList();
        return View(calisanlar);
    }

    // Yeni çalışan ekleme
    public IActionResult Create()
    {
        ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "Id", "Ad");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Calisan calisan, int[] selectedUzmanlikAlanlari)
    {
        if (ModelState.IsValid)
        {
            _context.Add(calisan);

            // Uzmanlık alanlarını ekleyin
            if (selectedUzmanlikAlanlari != null)
            {
                foreach (var uzmanlikId in selectedUzmanlikAlanlari)
                {
                    var uzmanlikAlani = await _context.UzmanlikAlanlari.FindAsync(uzmanlikId);
                    calisan.UzmanlikAlanlari.Add(uzmanlikAlani);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["UzmanlikAlanlari"] = new SelectList(_context.UzmanlikAlanlari, "Id", "Ad", selectedUzmanlikAlanlari);
        return View(calisan);
    }

    // Çalışan detaylarını gösterme
    public async Task<IActionResult> Details(int id)
    {
        var calisan = await _context.Calisanlar
            .Include(c => c.UzmanlikAlanlari)
            .Include(c => c.UygunlukSaatleri)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (calisan == null)
        {
            return NotFound();
        }

        return View(calisan);
    }

    [HttpPost]
    public IActionResult Ekle(Calisan calisan)
    {
        if (ModelState.IsValid)
        {
            // Yeni çalışan verisini veritabanına ekle
            _context.Calisanlar.Add(calisan);
            _context.SaveChanges();  // Değişiklikleri kaydet

            TempData["Mesaj"] = "Çalışan başarıyla eklendi!";
            return RedirectToAction("Index");
        }

        return View(calisan);
    }
}

