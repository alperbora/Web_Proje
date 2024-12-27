using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    // API yolu "/api/islem"
    [Route("api/[controller]")]
    [ApiController]
    public class IslemApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IslemApiController(AppDbContext context)
        {
            _context = context;
        }

        // İşlemleri listelemek için GET metodu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Islem>>> GetIslemler()
        {
            var islemler = await _context.Islemler.ToListAsync();
            return Ok(islemler);
        }

        // Yeni işlem eklemek için POST metodu
        [HttpPost]
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
    }
}
