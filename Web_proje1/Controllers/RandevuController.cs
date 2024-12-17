using Microsoft.AspNetCore.Mvc;
using Web_proje1.Data;
using Web_proje1.Models;

public class RandevuController : Controller
{
    private readonly ApplicationDbContext _context;

    public RandevuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Randevu sayfası gösterme
    public IActionResult Index()
    {
        var calisanlar = _context.Calisanlar.ToList();
        ViewBag.Calisanlar = calisanlar;
        return View();
    }

    // Randevu oluşturma
    [HttpPost]
    public IActionResult RandevuAl(Randevu randevu)
    {
        if (_context.Randevular.Any(r => r.CalisanId == randevu.CalisanId &&
                                         r.RandevuZamani == randevu.RandevuZamani))
        {
            ModelState.AddModelError("", "Bu saatte başka bir randevu alınmış. Lütfen başka bir saat seçin.");
            return RedirectToAction("Index");
        }

        _context.Randevular.Add(randevu);
        _context.SaveChanges();
        TempData["Basari"] = "Randevunuz başarıyla alındı!";
        return RedirectToAction("Index");
    }
}
