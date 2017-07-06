using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Model;
using NotenverwaltungBackend.Models;

namespace NotenverwaltungBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/SchuelerNotens")]
    public class SchuelerNotensController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public SchuelerNotensController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/SchuelerNotens
        [HttpGet]
        public IEnumerable<SchuelerNoten> GetSchuelerNoten()
        {
            return _context.SchuelerNoten;
        }

        // GET: api/SchuelerNotens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchuelerNoten([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schuelerNoten = await _context.SchuelerNoten.SingleOrDefaultAsync(m => m.Benutzername == id);

            if (schuelerNoten == null)
            {
                return NotFound();
            }

            return Ok(schuelerNoten);
        }

        // PUT: api/SchuelerNotens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchuelerNoten([FromRoute] string id, [FromBody] SchuelerNoten schuelerNoten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schuelerNoten.Benutzername)
            {
                return BadRequest();
            }

            _context.Entry(schuelerNoten).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchuelerNotenExists(id))
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

        // POST: api/SchuelerNotens
        [HttpPost]
        public async Task<IActionResult> PostSchuelerNoten([FromBody] SchuelerNoten schuelerNoten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SchuelerNoten.Add(schuelerNoten);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchuelerNoten", new { id = schuelerNoten.Benutzername }, schuelerNoten);
        }

        // DELETE: api/SchuelerNotens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchuelerNoten([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schuelerNoten = await _context.SchuelerNoten.SingleOrDefaultAsync(m => m.Benutzername == id);
            if (schuelerNoten == null)
            {
                return NotFound();
            }

            _context.SchuelerNoten.Remove(schuelerNoten);
            await _context.SaveChangesAsync();

            return Ok(schuelerNoten);
        }

        private bool SchuelerNotenExists(string id)
        {
            return _context.SchuelerNoten.Any(e => e.Benutzername == id);
        }
    }
}