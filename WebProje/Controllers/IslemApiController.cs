using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebProje.Controllers
{
    // API için rota tanımlaması ve sadece Admin rolüne sahip kullanıcıların erişimi sağlanır
    [Route("api/[controller]"), Authorize(Roles = "Admin")]
    [ApiController, Authorize(Roles = "Admin")]
    public class IslemApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor: DbContext'i alır ve sınıfın context özelliğini ayarlar
        public IslemApiController(AppDbContext context)
        {
            _context = context;
        }

        // İşlemleri listelemek için GET metodu
        // Admin rolüne sahip kullanıcılar için geçerli
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Islem>>> GetIslemler()
        {
            var islemler = await _context.Islemler.ToListAsync(); // Veritabanından tüm işlemleri al
            return Ok(islemler); // İşlemleri JSON formatında döndür
        }

        // Yeni işlem eklemek için POST metodu
        // Admin rolüne sahip kullanıcılar için geçerli
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<Islem>> CreateIslem(Islem islem)
        {
            if (ModelState.IsValid) // Model doğrulaması başarılı ise
            {
                _context.Islemler.Add(islem); // Yeni işlemi veritabanına ekle
                await _context.SaveChangesAsync(); // Veritabanına kaydet
                return CreatedAtAction(nameof(GetIslemler), new { id = islem.Id }, islem); // İşlem başarıyla eklendiyse, JSON döndür
            }
            return BadRequest(ModelState); // Model geçerli değilse, hata döndür
        }

        // İşlemi düzenlemek için PUT metodu
        // Admin rolüne sahip kullanıcılar için geçerli
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIslem(int id, Islem islem)
        {
            if (id != islem.Id)
            {
                return BadRequest("ID uyumsuzluğu."); // ID uyuşmazlığı varsa, hata döndür
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Model doğrulaması başarısızsa, hata döndür
            }

            _context.Entry(islem).State = EntityState.Modified; // İşlemi güncelle

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IslemExists(id)) 
                {
                    return NotFound("Belirtilen işlem bulunamadı."); 
                }
                else
                {
                    throw; 
                }
            }

            return NoContent(); // Başarıyla güncellendiyse, içerik döndürmeden başarılı yanıt gönder
        }

        // İşlemi silmek için DELETE metodu
        // Admin rolüne sahip kullanıcılar için geçerli
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIslem(int id)
        {
            var islem = await _context.Islemler.FindAsync(id); // İşlemi ID ile bul
            if (islem == null)
            {
                return NotFound("Belirtilen işlem bulunamadı."); 
            }

            _context.Islemler.Remove(islem); // İşlemi sil
            await _context.SaveChangesAsync(); // Veritabanından silme işlemi

            return NoContent(); 
        }

        // İşlemin var olup olmadığını kontrol eden yardımcı metot
        // Veritabanında işlem var mı kontrolü yapar
        private bool IslemExists(int id)
        {
            return _context.Islemler.Any(e => e.Id == id); // Veritabanında işlem var mı kontrol et
        }
    }
}
