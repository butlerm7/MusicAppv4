using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicApp2017.Models;

namespace MusicApp2017.Controllers
{
    public class RatingsController : Controller
    {
        private readonly MusicDbContext _context;

        public RatingsController(MusicDbContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rating.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .SingleOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingID,AlbumID,Username,Stars")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rating);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }
            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingID,AlbumID,Username,Stars")] Rating rating)
        {
            if (id != rating.RatingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(rating);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .SingleOrDefaultAsync(m => m.RatingID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.RatingID == id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.RatingID == id);
        }
    }
}