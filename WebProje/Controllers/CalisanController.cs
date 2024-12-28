using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebProje.Controllers
{
    public class CalisanController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor: DbContext'i alır ve sınıfın context özelliğini ayarlar.
        public CalisanController(AppDbContext context)
        {
            _context = context;
        }

        // Index - Çalışanları listeleme (Admin rolüne sahip kullanıcılar için)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var calisanlar = await _context.Calisanlar.ToListAsync(); // Veritabanından tüm çalışanları al
            return View(calisanlar); 
        }

        // Create - Yeni çalışan ekleme (Admin rolüne sahip kullanıcılar için)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost,Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Calisan calisan)
        {
            if (ModelState.IsValid) // Model doğrulaması başarılı ise
            {
                _context.Add(calisan); // Yeni çalışanı DbContext'e ekle
                await _context.SaveChangesAsync(); // Veritabanına kaydet
                return RedirectToAction(nameof(Index)); 
            }
            return View(calisan); 
        }

        // Edit - Çalışan bilgilerini düzenleme (Admin rolüne sahip kullanıcılar için)
        [Authorize(Roles = "Admin")]
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

        [HttpPost,Authorize(Roles = "Admin")]
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
                    _context.Update(calisan); // Çalışanı güncelle
                    await _context.SaveChangesAsync(); // Veritabanına kaydet
                }
                catch
                {
                    if (!CalisanExists(calisan.Id))
                    {
                        return NotFound(); 
                    }
                    throw; 
                }
                return RedirectToAction(nameof(Index)); // Başarıyla güncellendiyse Index'e yönlendir
            }
            return View(calisan); // Model geçerli değilse, tekrar form sayfasına yönlendir
        }

        // Details - Çalışan detaylarını görüntüleme (Admin rolüne sahip kullanıcılar için)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id); // Çalışanı ID ile bul
            if (calisan == null)
            {
                return NotFound(); 
            }

            return View(calisan); 
        }

        // Delete - Çalışan silme sayfasını görüntüleme (Admin rolüne sahip kullanıcılar için)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }

            var calisan = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id); // Çalışanı ID ile bul
            if (calisan == null)
            {
                return NotFound(); 
            }

            return View(calisan); 
        }

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calisan = await _context.Calisanlar.FindAsync(id); // Çalışanı ID ile bul
            _context.Calisanlar.Remove(calisan); // Çalışanı sil
            await _context.SaveChangesAsync(); // Veritabanından silme işlemi
            return RedirectToAction(nameof(Index)); // Silme işlemi başarılıysa, Index sayfasına yönlendir
        }

        // CalisanExists - Çalışan veritabanında var mı kontrolü
        private bool CalisanExists(int id)
        {
            return _context.Calisanlar.Any(e => e.Id == id); // Veritabanında çalışan var mı diye kontrol et
        }
    }
}
