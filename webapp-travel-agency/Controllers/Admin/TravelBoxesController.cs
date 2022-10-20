using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp_travel_agency.Data;

namespace webapp_travel_agency.Controllers.Admin
{
    public class TravelBoxesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelBoxesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TravelBoxes
        public async Task<IActionResult> Index()
        {
              return View(await _context.TravelBox.ToListAsync());
        }

        // GET: TravelBoxes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TravelBox == null)
            {
                return NotFound();
            }

            var travelBox = await _context.TravelBox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelBox == null)
            {
                return NotFound();
            }

            return View(travelBox);
        }

        // GET: TravelBoxes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelBoxes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Title,Price,Description,TravelDays")] TravelBox travelBox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelBox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelBox);
        }

        // GET: TravelBoxes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TravelBox == null)
            {
                return NotFound();
            }

            var travelBox = await _context.TravelBox.FindAsync(id);
            if (travelBox == null)
            {
                return NotFound();
            }
            return View(travelBox);
        }

        // POST: TravelBoxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Title,Price,Description,TravelDays")] TravelBox travelBox)
        {
            if (id != travelBox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelBox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelBoxExists(travelBox.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(travelBox);
        }

        // GET: TravelBoxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TravelBox == null)
            {
                return NotFound();
            }

            var travelBox = await _context.TravelBox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelBox == null)
            {
                return NotFound();
            }

            return View(travelBox);
        }

        // POST: TravelBoxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TravelBox == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TravelBox'  is null.");
            }
            var travelBox = await _context.TravelBox.FindAsync(id);
            if (travelBox != null)
            {
                _context.TravelBox.Remove(travelBox);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelBoxExists(int id)
        {
          return _context.TravelBox.Any(e => e.Id == id);
        }
    }
}
