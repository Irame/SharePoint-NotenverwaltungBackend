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
    [Route("api/KlasseLehrer")]
    public class KlasseLehrerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public KlasseLehrerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // PUT: api/KlasseLehrer/5/1
        [HttpPost("Add/{klasseId}/{lehrerId}")]
        public async Task<IActionResult> PutKlasseLehrer([FromRoute] int klasseId, [FromRoute] int lehrerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(new KlasseLehrer{ KlasseID = klasseId, LehrerID = lehrerId }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlasseLehrerExists(klasseId, lehrerId))
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

        // DELETE: api/KlasseLehrer/5
        [HttpDelete("Remove/{klasseId}/{lehrerId}")]
        public async Task<IActionResult> DeleteKlasseLehrer([FromRoute] int klasseId, [FromRoute] int lehrerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var klasseLehrer = await _context.KlasseLehrer.SingleOrDefaultAsync(m => m.KlasseID == klasseId && m.LehrerID == lehrerId);
            if (klasseLehrer == null)
            {
                return NotFound();
            }

            _context.KlasseLehrer.Remove(klasseLehrer);
            await _context.SaveChangesAsync();

            return Ok(klasseLehrer);
        }

        private bool KlasseLehrerExists(int klasseId, int lehrerId)
        {
            return _context.KlasseLehrer.Any(e => e.KlasseID == klasseId && e.LehrerID == lehrerId);
        }
    }
}