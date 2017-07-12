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
    [Route("api/Jahrgang")]
    public class JahrgangController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public JahrgangController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }

        // GET: api/Jahrgang
        [HttpGet]
        public IEnumerable<Jahrgang> GetJahrgang()
        {
            return _context.Jahrgang;
        }

        // GET: api/Jahrgang/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJahrgang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jahrgang = await _context.Jahrgang.SingleOrDefaultAsync(m => m.JahrgangID == id);

            if (jahrgang == null)
            {
                return NotFound();
            }

            return Ok(jahrgang);
        }

        // PUT: api/Jahrgang/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJahrgang([FromRoute] int id, [FromBody] Jahrgang jahrgang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jahrgang.JahrgangID)
            {
                return BadRequest();
            }

            _context.Entry(jahrgang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JahrgangExists(id))
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

        // POST: api/Jahrgang
        [HttpPost]
        public async Task<IActionResult> PostJahrgang([FromBody] Jahrgang jahrgang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Jahrgang.Add(jahrgang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJahrgang", new { id = jahrgang.JahrgangID }, jahrgang);
        }

        // DELETE: api/Jahrgang/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJahrgang([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jahrgang = await _context.Jahrgang.SingleOrDefaultAsync(m => m.JahrgangID == id);
            if (jahrgang == null)
            {
                return NotFound();
            }

            _context.Jahrgang.Remove(jahrgang);
            await _context.SaveChangesAsync();

            return Ok(jahrgang);
        }

        private bool JahrgangExists(int id)
        {
            return _context.Jahrgang.Any(e => e.JahrgangID == id);
        }
    }
}