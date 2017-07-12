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
    [Route("api/Notenerhebung")]
    public class NotenerhebungController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public NotenerhebungController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Notenerhebung
        [HttpGet]
        public IEnumerable<Notenerhebung> GetNotenerhebung()
        {
            return _context.Notenerhebung;
        }

        // GET: api/Notenerhebung/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotenerhebung([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notenerhebung = await _context.Notenerhebung.SingleOrDefaultAsync(m => m.NotenerhebungID == id);

            if (notenerhebung == null)
            {
                return NotFound();
            }

            return Ok(notenerhebung);
        }

        // PUT: api/Notenerhebung/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotenerhebung([FromRoute] int id, [FromBody] Notenerhebung notenerhebung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notenerhebung.NotenerhebungID)
            {
                return BadRequest();
            }

            _context.Entry(notenerhebung).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotenerhebungExists(id))
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

        // POST: api/Notenerhebung
        [HttpPost]
        public async Task<IActionResult> PostNotenerhebung([FromBody] Notenerhebung notenerhebung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notenerhebung.Add(notenerhebung);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotenerhebung", new { id = notenerhebung.NotenerhebungID }, notenerhebung);
        }

        // DELETE: api/Notenerhebung/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotenerhebung([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notenerhebung = await _context.Notenerhebung.SingleOrDefaultAsync(m => m.NotenerhebungID == id);
            if (notenerhebung == null)
            {
                return NotFound();
            }

            _context.Notenerhebung.Remove(notenerhebung);
            await _context.SaveChangesAsync();

            return Ok(notenerhebung);
        }

        private bool NotenerhebungExists(int id)
        {
            return _context.Notenerhebung.Any(e => e.NotenerhebungID == id);
        }
    }
}