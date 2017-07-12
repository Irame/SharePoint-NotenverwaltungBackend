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
    [Route("api/KlasseSchueler")]
    public class KlasseSchuelerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public KlasseSchuelerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // PUT: api/KlasseSchueler/5/1
        [HttpPost("Add/{klasseId}/{schuelerId}")]
        public async Task<IActionResult> PutKlasseSchueler([FromRoute] int klasseId, [FromRoute] int schuelerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(new KlasseSchueler{ KlasseID = klasseId, SchuelerID = schuelerId }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlasseSchuelerExists(klasseId, schuelerId))
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

        // DELETE: api/KlasseSchueler/5
        [HttpDelete("Remove/{klasseId}/{schuelerId}")]
        public async Task<IActionResult> DeleteKlasseSchueler([FromRoute] int klasseId, [FromRoute] int schuelerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var klasseSchueler = await _context.KlasseSchueler.SingleOrDefaultAsync(m => m.KlasseID == klasseId && m.SchuelerID == schuelerId);
            if (klasseSchueler == null)
            {
                return NotFound();
            }

            _context.KlasseSchueler.Remove(klasseSchueler);
            await _context.SaveChangesAsync();

            return Ok(klasseSchueler);
        }

        private bool KlasseSchuelerExists(int klasseId, int schuelerId)
        {
            return _context.KlasseSchueler.Any(e => e.KlasseID == klasseId && e.SchuelerID == schuelerId);
        }
    }
}