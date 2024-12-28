using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using Microsoft.AspNetCore.Authorization;

public class IslemController : Controller
{
    private readonly AppDbContext _context;

    // Constructor: DbContext'i alır ve sınıfın context özelliğini ayarlar
    public IslemController(AppDbContext context)
    {
        _context = context;
    }

    // İşlem Listesi - Admin rolüne sahip kullanıcılar için geçerli
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var islemler = await _context.Islemler.ToListAsync(); // Tüm işlemleri asenkron olarak al
        return View(islemler); 
    }

    // Yeni İşlem Ekle - GET
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View(); 
    }

    // Yeni İşlem Ekle - POST
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Islem islem)
    {
        if (ModelState.IsValid) // Model doğrulaması başarılı ise
        {
            _context.Islemler.Add(islem); // Yeni işlemi veritabanına ekle
            await _context.SaveChangesAsync(); // Veritabanına kaydet
            return RedirectToAction(nameof(Index)); // İşlem başarıyla eklendiyse listeye yönlendir
        }
        return View(islem); 
    }

    // İşlem Düzenle - GET
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id)
    {
        var islem = await _context.Islemler.FindAsync(id); // Belirtilen id ile işlemi bul
        if (islem == null)
        {
            return NotFound(); // İşlem bulunamazsa 404 döndür
        }
        return View(islem); 
    }

    // İşlem Düzenle - POST
    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, Islem islem)
    {
        if (id != islem.Id)
        {
            return NotFound(); // ID uyuşmazsa, işlem bulunamadı hatası döndür
        }

        if (ModelState.IsValid) // Model doğrulaması başarılı ise
        {
            try
            {
                _context.Update(islem); // İşlemi güncelle
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IslemExists(islem.Id)) // Eğer işlem veritabanında yoksa
                {
                    return NotFound(); 
                }
                else
                {
                    throw; 
                }
            }
            return RedirectToAction(nameof(Index)); // Düzenleme başarılıysa listeye dön
        }
        return View(islem); 
    }

    // İşlem Sil - GET
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var islem = await _context.Islemler.FindAsync(id); // Belirtilen id ile işlemi bul
        if (islem == null)
            return NotFound(); 

        return View(islem); 
    }

    // İşlem Sil - POST
    [HttpPost, ActionName("Delete"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var islem = await _context.Islemler.FindAsync(id); // Belirtilen id ile işlemi bul
        if (islem != null)
        {
            _context.Islemler.Remove(islem); // İşlemi sil
            await _context.SaveChangesAsync(); // Veritabanından sil
        }
        return RedirectToAction(nameof(Index)); // Silme işlemi sonrası listeye yönlendir
    }

    // İşlemin var olup olmadığını kontrol eden yardımcı metot
    private bool IslemExists(int id)
    {
        return _context.Islemler.Any(e => e.Id == id); // Veritabanında işlem var mı kontrol et
    }
}
