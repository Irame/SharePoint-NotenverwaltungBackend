using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Data;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/SchuelerSicht")]
    public class SchuelerSichtController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public SchuelerSichtController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }
        
        [HttpGet("{benutzername}")]
        public async Task<IActionResult> GetSchueler([FromRoute] string benutzername)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schueler = await _context.Schueler.Include(s => s.Person).SingleOrDefaultAsync(s => s.Person.Benutzername == benutzername);
            
            if (schueler == null)
            {
                return NotFound();
            }

            var result = new SchuelerSicht {Id = schueler.SchuelerID};

            var klassenQuery = _context.Schueler.Include(s => s.Person)
                .Where(s => s.Person.Benutzername == benutzername)
                .Include(s => s.KlasseSchueler).ThenInclude(ks => ks.Klasse).SelectMany(x => x.KlasseSchueler)
                .Select(x => x.Klasse);
            foreach (var jahrgang in _context.Jahrgang)
            {
                var klasse = await klassenQuery.Where(x => x.Jahrgang == jahrgang).Include(x => x.KlasseFach).ThenInclude(x => x.Fach).SingleOrDefaultAsync();
                var klasseSicht = new KlasseSicht { Jahrgang = jahrgang.Name, Klasse = klasse.Name };

                var feacher = klasse.KlasseFach.Select(x => x.Fach).ToList();
                var noten = _context.Notenerhebung.Where(x => x.SchuelerID == schueler.SchuelerID).ToList();

                foreach (var fach in feacher)
                {
                    klasseSicht.Faecher
                        .Add(new FachSicht
                        {
                            Name = fach.Name,
                            Noten = noten
                                .Where(x => x.FachID == fach.FachID)
                                .Select(x => new NoteSicht {Datum = x.Datum, Note = x.Note, Typ = x.Typ})
                                .ToList()
                        });
                }
                result.Klassen.Add(klasseSicht);
            }

            return Ok(result);
        }

        public class SchuelerSicht
        {
            public int Id { get; set; }
            public List<KlasseSicht> Klassen { get; set; } = new List<KlasseSicht>();
        }

        public class KlasseSicht
        {
            public string Jahrgang { get; set; }
            public string Klasse { get; set; }
            public List<FachSicht> Faecher { get; set; } = new List<FachSicht>();
        }

        public class FachSicht
        {
            public string Name { get; set; }
            public List<NoteSicht> Noten { get; set; } = new List<NoteSicht>();
        }

        public class NoteSicht
        {
            public int Note;
            public DateTime Datum;
            public string Typ;
        }
    }
}
