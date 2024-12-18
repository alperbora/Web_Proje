using Microsoft.AspNetCore.Mvc;
using WebProje.Data;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class SalonController : Controller
    {
        private readonly AppDbContext _context;

        public SalonController(AppDbContext context)
        {
            _context = context;
        }

        // Salonları listelemek
        public IActionResult Index()
        {
            var salonlar = _context.Salonlar.ToList();
            return View(salonlar);
        }

        // Salon eklemek için view
        public IActionResult Create()
        {
            return View();
        }

        // Salon eklemek
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salon);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // Salon düzenlemek için view
        public IActionResult Edit(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // Salon düzenleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Salon salon)
        {
            if (id != salon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    _context.SaveChanges();
                }
                catch
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // Salon silme işlemi
        public IActionResult Delete(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
            {
                return NotFound();
            }
            _context.Salonlar.Remove(salon);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
