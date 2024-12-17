using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_proje1.Data;
using Web_proje1.Models;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;  // DbContext alanını ekliyoruz

    // Constructor'a ApplicationDbContext'i enjekte ediyoruz
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;  // _context'i initialize ediyoruz
    }

    public IActionResult Index()
    {
        var calisanlar = _context.Calisanlar.ToList();  // Veritabanından çalışanları al
        return View(calisanlar);  // Model olarak gönder
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult RandevuAl(Randevu randevu)
    {
        if (ModelState.IsValid)  // Model geçerli mi?
        {
            // Veritabanına ekleme işlemi
            _context.Randevular.Add(randevu);
            _context.SaveChanges();
            TempData["Basari"] = "Randevu başarıyla kaydedildi!";
            return RedirectToAction("Index");
        }
        else
        {
            // Hatalı modelde formu tekrar gönder
            return View(randevu);
        }
    }

    public IActionResult RandevuOnayla(int id)
    {
        // _context ile veritabanı işlemi yapılabilir
        var randevu = _context.Randevular.Find(id);
        if (randevu != null)
        {
            randevu.OnayliMi = true;
            _context.SaveChanges();
            TempData["Basari"] = "Randevu başarıyla onaylandı!";
        }
        else
        {
            TempData["Hata"] = "Randevu bulunamadı.";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Randevu()
    {
        var calisanlar = _context.Calisanlar.ToList(); // Calisanlar listesi
        return View(calisanlar);
    }

    public IActionResult Calisanlar()
    {
        var calisanlar = _context.Calisanlar.Include(c => c.UzmanlikAlanlari).ToList();
        return View(calisanlar); // Bu satır, veriyi View'a gönderiyor
    }

    public IActionResult Iletisim()
    {
        return View();
        ViewBag.Success = TempData["Success"];
        ViewBag.Error = TempData["Error"];
    }

    public IActionResult Admin_giris()
    {
        return View();
    }

    public IActionResult Ekle()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
