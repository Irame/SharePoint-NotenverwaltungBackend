using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Data;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Lehrer")]
    public class LehrerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public LehrerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Lehrer
        [HttpGet]
        public IEnumerable<Lehrer> GetLehrer()
        {
            return _context.Lehrer;
        }

        // GET: api/Lehrer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLehrer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lehrer = await _context.Lehrer.SingleOrDefaultAsync(m => m.LehrerID == id);

            if (lehrer == null)
            {
                return NotFound();
            }

            return Ok(lehrer);
        }

        // PUT: api/Lehrer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLehrer([FromRoute] int id, [FromBody] Lehrer lehrer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lehrer.LehrerID)
            {
                return BadRequest();
            }

            _context.Entry(lehrer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LehrerExists(id))
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

        // POST: api/Lehrer
        [HttpPost]
        public async Task<IActionResult> PostLehrer([FromBody] Lehrer lehrer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Lehrer.Add(lehrer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLehrer", new { id = lehrer.LehrerID }, lehrer);
        }

        // DELETE: api/Lehrer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLehrer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lehrer = await _context.Lehrer.SingleOrDefaultAsync(m => m.LehrerID == id);
            if (lehrer == null)
            {
                return NotFound();
            }

            _context.Lehrer.Remove(lehrer);
            await _context.SaveChangesAsync();

            return Ok(lehrer);
        }

        private bool LehrerExists(int id)
        {
            return _context.Lehrer.Any(e => e.LehrerID == id);
        }
    }
}