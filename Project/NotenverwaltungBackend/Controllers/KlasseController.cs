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
    [Route("api/Klasse")]
    public class KlasseController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public KlasseController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Klasse
        [HttpGet]
        public IEnumerable<Klasse> GetKlasse()
        {
            return _context.Klasse;
        }

        // GET: api/Klasse/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKlasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var klasse = await _context.Klasse.SingleOrDefaultAsync(m => m.KlasseID == id);

            if (klasse == null)
            {
                return NotFound();
            }

            return Ok(klasse);
        }

        // PUT: api/Klasse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKlasse([FromRoute] int id, [FromBody] Klasse klasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != klasse.KlasseID)
            {
                return BadRequest();
            }

            _context.Entry(klasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlasseExists(id))
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

        // POST: api/Klasse
        [HttpPost]
        public async Task<IActionResult> PostKlasse([FromBody] Klasse klasse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Klasse.Add(klasse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKlasse", new { id = klasse.KlasseID }, klasse);
        }

        // DELETE: api/Klasse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlasse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var klasse = await _context.Klasse.SingleOrDefaultAsync(m => m.KlasseID == id);
            if (klasse == null)
            {
                return NotFound();
            }

            _context.Klasse.Remove(klasse);
            await _context.SaveChangesAsync();

            return Ok(klasse);
        }

        private bool KlasseExists(int id)
        {
            return _context.Klasse.Any(e => e.KlasseID == id);
        }
    }
}