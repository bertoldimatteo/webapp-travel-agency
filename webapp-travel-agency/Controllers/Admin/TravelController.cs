using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Controllers.Admin
{
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Travel
        public async Task<IActionResult> Index()
        {
            return View(await _context.Travels.ToListAsync());
        }

        // GET: Travel/Detail/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travel = await _context.Travels.FirstOrDefaultAsync(m => m.Id == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
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
        public async Task<IActionResult> Create(Travel travel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travel);
        }

        // GET: TravelBoxes/Edit/5
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travel = await _context.Travels.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }
            return View(travel);
        }

        // POST: TravelBoxes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Travel travel)
        {
            if (id != travel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelExists(travel.Id))
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
            return View(travel);
        }

        // GET: TravelBoxes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Travels == null)
            {
                return NotFound();
            }

            var travelBox = await _context.Travels
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
            if (_context.Travels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Travel'  is null.");
            }
            var travelBox = await _context.Travels.FindAsync(id);
            if (travelBox != null)
            {
                _context.Travels.Remove(travelBox);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelExists(int id)
        {
            return _context.Travels.Any(e => e.Id == id);
        }
    }
}
