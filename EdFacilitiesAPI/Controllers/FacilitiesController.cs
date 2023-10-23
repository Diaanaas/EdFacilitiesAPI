using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EdFacilitiesAPI.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace EdFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly EducationalDbContext _context;

        public FacilitiesController(EducationalDbContext context)
        {
            _context = context;
        }

        // GET: api/Facilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facility>>> GetFacilities([FromQuery] Parameters parameters)
        {
            if (_context.Facilities == null)
            {
                return NotFound();
            }
            var facilites = await _context.Facilities
                       .OrderBy(on => on.Id)
                       .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                       .Take(parameters.PageSize)
                       .ToListAsync();

            var link = new Uri(Request.GetDisplayUrl()).GetLeftPart(UriPartial.Authority);
            var response = new
            {
                nextLink = link +
                "/api/Facilities?PageNumber=" +
                (parameters.PageNumber + 1) +
                "&PageSize=" +
                parameters.PageSize,

                values = facilites
            };

            return Ok(response);
        }

        // GET: api/Facilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facility>> GetFacility(int id)
        {
            if (_context.Facilities == null)
            {
                return NotFound();
            }
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                return NotFound();
            }

            return facility;
        }

        // PUT: api/Facilities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacility(int id, Facility facility)
        {
            if (id != facility.Id)
            {
                return BadRequest();
            }

            _context.Entry(facility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacilityExists(id))
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

        // POST: api/Facilities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facility>> PostFacility(Facility facility)
        {
            if (_context.Facilities == null)
            {
                return Problem("Entity set 'EducationalDbContext.Facilities'  is null.");
            }
            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacility", new { id = facility.Id }, facility);
        }

        // DELETE: api/Facilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            if (_context.Facilities == null)
            {
                return NotFound();
            }
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }

            _context.Facilities.Remove(facility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacilityExists(int id)
        {
            return (_context.Facilities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
