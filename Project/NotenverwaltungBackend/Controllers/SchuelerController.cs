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
    [Route("api/Schueler")]
    public class SchuelerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public SchuelerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Schueler
        [HttpGet]
        public IEnumerable<Schueler> GetSchueler()
        {
            return _context.Schueler;
        }

        // GET: api/Schueler/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchueler([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schueler = await _context.Schueler.SingleOrDefaultAsync(m => m.SchuelerID == id);

            if (schueler == null)
            {
                return NotFound();
            }

            return Ok(schueler);
        }

        // PUT: api/Schueler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchueler([FromRoute] int id, [FromBody] Schueler schueler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schueler.SchuelerID)
            {
                return BadRequest();
            }

            _context.Entry(schueler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchuelerExists(id))
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

        // POST: api/Schueler
        [HttpPost]
        public async Task<IActionResult> PostSchueler([FromBody] Schueler schueler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Schueler.Add(schueler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchueler", new { id = schueler.SchuelerID }, schueler);
        }

        // DELETE: api/Schueler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchueler([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schueler = await _context.Schueler.SingleOrDefaultAsync(m => m.SchuelerID == id);
            if (schueler == null)
            {
                return NotFound();
            }

            _context.Schueler.Remove(schueler);
            await _context.SaveChangesAsync();

            return Ok(schueler);
        }

        private bool SchuelerExists(int id)
        {
            return _context.Schueler.Any(e => e.SchuelerID == id);
        }
    }
}