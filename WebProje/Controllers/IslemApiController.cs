using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebProje.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "Admin")]
    [ApiController, Authorize(Roles = "Admin")]
    public class IslemApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IslemApiController(AppDbContext context)
        {
            _context = context;
        }

        // İşlemleri listelemek için GET metodu
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Islem>>> GetIslemler()
        {
            var islemler = await _context.Islemler.ToListAsync();
            return Ok(islemler);
        }

        // Yeni işlem eklemek için POST metodu
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<Islem>> CreateIslem(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Add(islem);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetIslemler), new { id = islem.Id }, islem);
            }
            return BadRequest(ModelState);
        }

        // İşlemi düzenlemek için PUT metodu
        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIslem(int id, Islem islem)
        {
            if (id != islem.Id)
            {
                return BadRequest("ID uyumsuzluğu.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(islem).State = EntityState.Modified;

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

            return NoContent();
        }

        // İşlemi silmek için DELETE metodu
        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteIslem(int id)
        {
            var islem = await _context.Islemler.FindAsync(id);
            if (islem == null)
            {
                return NotFound("Belirtilen işlem bulunamadı.");
            }

            _context.Islemler.Remove(islem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // İşlemin var olup olmadığını kontrol eden yardımcı metot
        private bool IslemExists(int id)
        {
            return _context.Islemler.Any(e => e.Id == id);
        }
    }

}