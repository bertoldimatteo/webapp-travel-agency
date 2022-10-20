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

        // GET: api/TravelApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travel>>> GetTravel()
        {
            return await _context.Travels.ToListAsync();
        }

        // GET: api/TravelApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Travel>> GetTravel(int id)
        {
            var travel = await _context.Travels.FindAsync(id);

            if (travel == null)
            {
                return NotFound();
            }

            return travel;
        }

        // PUT: api/TravelApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravel(int id, Travel travel)
        {
            if (id != travel.Id)
            {
                return BadRequest();
            }

            _context.Entry(travel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelExists(id))
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

        // POST: api/TravelApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travel>> PostTravel(Travel travel)
        {
            _context.Travels.Add(travel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTravelBox", new { id = travel.Id }, travel);
        }

        // DELETE: api/TravelBoxesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel(int id)
        {
            var travel = await _context.Travels.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }

            _context.Travels.Remove(travel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelExists(int id)
        {
            return _context.Travels.Any(e => e.Id == id);
        }
    }
}
