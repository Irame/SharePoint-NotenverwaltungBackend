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
    [Route("api/FachLehrer")]
    public class FachLehrerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public FachLehrerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }
        
        // PUT: api/FachLehrer/5/1
        [HttpPost("Add/{fachId}/{lehrerId}")]
        public async Task<IActionResult> PutFachLehrer([FromRoute] int fachId, [FromRoute] int lehrerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(new FachLehrer{ FachID = fachId, LehrerID = lehrerId }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FachLehrerExists(fachId, lehrerId))
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

        // DELETE: api/FachLehrer/5
        [HttpDelete("Remove/{fachId}/{lehrerId}")]
        public async Task<IActionResult> DeleteFachLehrer([FromRoute] int fachId, [FromRoute] int lehrerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fachLehrer = await _context.FachLehrer.SingleOrDefaultAsync(m => m.FachID == fachId && m.LehrerID == lehrerId);
            if (fachLehrer == null)
            {
                return NotFound();
            }

            _context.FachLehrer.Remove(fachLehrer);
            await _context.SaveChangesAsync();

            return Ok(fachLehrer);
        }

        private bool FachLehrerExists(int fachId, int lehrerId)
        {
            return _context.FachLehrer.Any(e => e.FachID == fachId && e.LehrerID == lehrerId);
        }
    }
}