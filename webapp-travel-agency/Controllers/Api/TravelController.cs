using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TravelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TravelBoxesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travel>>> GetTravelBox()
        {
            return await _context.Travels.ToListAsync();
        }

        // GET: api/TravelBoxesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Travel>> GetTravelBox(int id)
        {
            var travelBox = await _context.Travels.FindAsync(id);

            if (travelBox == null)
            {
                return NotFound();
            }

            return travelBox;
        }

        // PUT: api/TravelBoxesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravelBox(int id, Travel travelBox)
        {
            if (id != travelBox.Id)
            {
                return BadRequest();
            }

            _context.Entry(travelBox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelBoxExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TravelBoxesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travel>> PostTravelBox(Travel travelBox)
        {
            _context.Travels.Add(travelBox);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTravelBox", new { id = travelBox.Id }, travelBox);
        }

        // DELETE: api/TravelBoxesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelBox(int id)
        {
            var travelBox = await _context.Travels.FindAsync(id);
            if (travelBox == null)
            {
                return NotFound();
            }

            _context.Travels.Remove(travelBox);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelBoxExists(int id)
        {
            return _context.Travels.Any(e => e.Id == id);
        }
    }
}
