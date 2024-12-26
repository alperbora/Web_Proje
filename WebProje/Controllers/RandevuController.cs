﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProje.Data;
using WebProje.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebProje.Controllers
{
    public class RandevuController : Controller
    {
        private readonly AppDbContext _context;

        public RandevuController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
            ViewBag.Islemler = await _context.Islemler.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Randevu randevu)
        {
            // Çakışma kontrolü
            var mevcutRandevu = await _context.Randevular
                .Where(r => r.CalisanId == randevu.CalisanId &&
                            r.RandevuSaati == randevu.RandevuSaati)
                .FirstOrDefaultAsync();

            if (mevcutRandevu != null)
            {
                ModelState.AddModelError("", "Seçilen saat dolu. Lütfen başka bir zaman seçin.");
                ViewBag.Calisanlar = await _context.Calisanlar.ToListAsync();
                ViewBag.Islemler = await _context.Islemler.ToListAsync();
                return View(randevu);
            }

            // Randevuyu veritabanına ekle
            _context.Randevular.Add(randevu);
            await _context.SaveChangesAsync();

            // Başarı mesajı ekle
            TempData["SuccessMessage"] = "Randevunuz başarıyla alınmıştır!";
            return RedirectToAction("Create"); // Randevu alındıktan sonra aynı sayfada kalacak
        }

        public async Task<IActionResult> Index()
        {
            // Admin kontrolü kaldırıldı
            var randevular = await _context.Randevular
                .Include(r => r.Calisan)
                .ToListAsync();

            return View(randevular);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                randevu.Durum = "Onaylandı";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var randevu = await _context.Randevular.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}