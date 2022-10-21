using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TravelApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TravelApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //get for search
        [HttpGet]
        public IActionResult Get(string? name)
        {
            IQueryable<Travel> travels;

            if (name != null)
            {
                travels = _context.Travels.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            else
            {
                travels = _context.Travels;
            }

            return Ok(travels.ToList<Travel>());
        }

        [HttpGet("{id}")]
        public IActionResult GetTravelId(int id)
        {
            Travel travel = _context.Travels.Where(p => p.Id == id).FirstOrDefault();

            return Ok(travel);
        }


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


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travel>> PostSmartBox(Travel travel)
        {
            if (_context.Travels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.smartBoxes'  is null.");
            }
            _context.Travels.Add(travel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTravel", new { id = travel.Id }, travel);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartBox(int id)
        {
            if (_context.Travels == null)
            {
                return NotFound();
            }
            var smartBox = await _context.Travels.FindAsync(id);
            if (smartBox == null)
            {
                return NotFound();
            }

            _context.Travels.Remove(smartBox);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelExists(int id)
        {
            return (_context.Travels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
