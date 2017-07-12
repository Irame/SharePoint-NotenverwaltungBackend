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
    [Route("api/FachSchueler")]
    public class FachSchuelerController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public FachSchuelerController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // PUT: api/FachSchueler/5/1
        [HttpPost("Add/{fachId}/{schuelerId}")]
        public async Task<IActionResult> PutFachSchueler([FromRoute] int fachId, [FromRoute] int schuelerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = new FachSchueler { FachID = fachId, SchuelerID = schuelerId };
            _context.FachSchueler.Add(result);
            await _context.SaveChangesAsync();

            return Created("", result);
        }

        // DELETE: api/FachSchueler/5
        [HttpDelete("Remove/{fachId}/{schuelerId}")]
        public async Task<IActionResult> DeleteFachSchueler([FromRoute] int fachId, [FromRoute] int schuelerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fachSchueler = await _context.FachSchueler.SingleOrDefaultAsync(m => m.FachID == fachId && m.SchuelerID == schuelerId);
            if (fachSchueler == null)
            {
                return NotFound();
            }

            _context.FachSchueler.Remove(fachSchueler);
            await _context.SaveChangesAsync();

            return Ok(fachSchueler);
        }

        private bool FachSchuelerExists(int fachId, int schuelerId)
        {
            return _context.FachSchueler.Any(e => e.FachID == fachId && e.SchuelerID == schuelerId);
        }
    }
}