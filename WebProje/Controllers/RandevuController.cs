using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebProje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor: DbContext'i alır ve sınıfın context özelliğini ayarlar
        public RandevuController(AppDbContext context)
        {
            _context = context;
        }

        // Yeni randevu oluşturma sayfasını göstermek için GET metodu
        [HttpGet, Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create()
        {
            // Çalışanları ve işlemleri ViewBag'e ekleyerek sayfada seçilebilen seçenekler sunuluyor
            ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
            ViewBag.Islemler = await _context.Islemler.ToListAsync();
            return View();
        }

        // Yeni randevu oluşturma işlemi için POST metodu
        [HttpPost, Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create(Randevu randevu)
        {
            // Çakışma kontrolü: Seçilen çalışan ve saat için başka bir randevu var mı diye kontrol edilir
            var mevcutRandevu = await _context.Randevular
                .Where(r => r.CalisanId == randevu.CalisanId &&
                            r.RandevuSaati == randevu.RandevuSaati)
                .FirstOrDefaultAsync();

            if (mevcutRandevu != null)
            {
                // Çakışma bulunursa hata mesajı gösterilir
                ModelState.AddModelError("", "Seçilen saat dolu. Lütfen başka bir zaman seçin.");
                ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
                ViewBag.Islemler = await _context.Islemler.ToListAsync();
                return View(randevu);
            }

            // Seçilen işlemin ücretini almak için işlem adı ile işlem bulunur
            var islem = await _context.Islemler
                .Where(i => i.Ad == randevu.Islem)
                .FirstOrDefaultAsync();

            if (islem != null)
            {
                randevu.Ucret = islem.Ucret;  // İşlem ücretini randevuya ata
            }
            else
            {
                // Geçersiz işlem seçilmişse hata mesajı gösterilir
                ModelState.AddModelError("", "Geçersiz işlem seçildi.");
                ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
                ViewBag.Islemler = await _context.Islemler.ToListAsync();
                return View(randevu);
            }

            // Randevuyu veritabanına ekle
            _context.Randevular.Add(randevu);
            await _context.SaveChangesAsync();

            // Başarı mesajı eklenir ve randevu oluşturma sayfasına yönlendirilir
            TempData["SuccessMessage"] = "Randevunuz başarıyla alınmıştır!";
            return RedirectToAction("Create");
        }

        // Kullanıcıların randevularını listelemek için GET metodu
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            // Randevuları ve her bir randevu için işlem ücretini alarak listele
            var randevular = await _context.Randevular
                .Include(r => r.Calisan)
                .ToListAsync();

            foreach (var randevu in randevular)
            {
                var islem = await _context.Islemler
                    .Where(i => i.Ad == randevu.Islem)
                    .FirstOrDefaultAsync();

                if (islem != null)
                {
                    randevu.Ucret = islem.Ucret;  
                }
            }

            return View(randevular);
        }

        // Admin tarafından randevu onaylamak için metod
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "Onaylandı";  // Randevuyu onayla
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            }
            return RedirectToAction("Index"); 
        }

        // Admin tarafından randevuyu silme işlemi
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu); // Randevuyu sil
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            }
            return RedirectToAction("Index"); 
        }
    }
}
