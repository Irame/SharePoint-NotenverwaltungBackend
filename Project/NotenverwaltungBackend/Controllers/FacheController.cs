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
    [Route("api/Fache")]
    public class FacheController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public FacheController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Fache
        [HttpGet]
        public IEnumerable<Fach> GetFach()
        {
            return _context.Fach;
        }

        // GET: api/Fache/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFach([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fach = await _context.Fach.SingleOrDefaultAsync(m => m.FachID == id);

            if (fach == null)
            {
                return NotFound();
            }

            return Ok(fach);
        }

        // PUT: api/Fache/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFach([FromRoute] int id, [FromBody] Fach fach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fach.FachID)
            {
                return BadRequest();
            }

            _context.Entry(fach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FachExists(id))
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

        // POST: api/Fache
        [HttpPost]
        public async Task<IActionResult> PostFach([FromBody] Fach fach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Fach.Add(fach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFach", new { id = fach.FachID }, fach);
        }

        // DELETE: api/Fache/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFach([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fach = await _context.Fach.SingleOrDefaultAsync(m => m.FachID == id);
            if (fach == null)
            {
                return NotFound();
            }

            _context.Fach.Remove(fach);
            await _context.SaveChangesAsync();

            return Ok(fach);
        }

        private bool FachExists(int id)
        {
            return _context.Fach.Any(e => e.FachID == id);
        }
    }
}