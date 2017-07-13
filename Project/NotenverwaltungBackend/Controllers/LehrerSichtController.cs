using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Data;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/LehrerSicht")]
    public class LehrerSichtController : Controller
    {
        private readonly NotenverwaltungBackendContext _context;

        public LehrerSichtController(NotenverwaltungBackendContext context)
        {
            _context = context;
        }
        
        [HttpGet("{benutzername}")]
        public async Task<IActionResult> GetLehrerSicht([FromRoute] string benutzername)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lehrer = await _context.Lehrer
                .Include(x => x.Person)
                .Where(x => x.Person.Benutzername == benutzername)
                .Include(x => x.KlasseLehrer).ThenInclude(x => x.Klasse.Jahrgang)
                .Include(x => x.FachLehrer).ThenInclude(x => x.Fach).ThenInclude(x => x.Jahrgang)
                .Include(x => x.FachLehrer).ThenInclude(x => x.Fach.FachSchueler).ThenInclude(x => x.Schueler).ThenInclude(x => x.Person)
                .SingleOrDefaultAsync();
            
            if (lehrer == null)
            {
                return NotFound();
            }

            var result = new LehrerSicht { Id = lehrer.LehrerID};
            foreach (var klasse in lehrer.KlasseLehrer.Select(x => x.Klasse))
            {
                if (!result.Jahrgaenge.TryGetValue(klasse.Jahrgang.Name, out List<KlasseSicht> jahrgangKlassen))
                    result.Jahrgaenge.Add(klasse.Jahrgang.Name, (jahrgangKlassen = new List<KlasseSicht>()));

                var klasseSicht = new KlasseSicht {Klasse = klasse.Name};
                foreach (var fach in lehrer.FachLehrer.Select(x => x.Fach))
                {
                    klasseSicht.Faecher.Add(new FachSicht
                    {
                        Id = fach.FachID,
                        Name = fach.Name,
                        Schueler = fach.FachSchueler
                            .Select(x => new SchuelerSicht
                            {
                                Id = x.SchuelerID,
                                Name = $"{x.Schueler.Person.Vorname} {x.Schueler.Person.Nachname}",
                                Noten = _context.Notenerhebung
                                            .Where(y => y.SchuelerID == x.SchuelerID && y.FachID == x.FachID)
                                            .Select(n => new NoteSicht {Note = n.Note, Datum = n.Datum, Typ = n.Typ}).ToList()
                            }).ToList()
                    });
                }

                jahrgangKlassen.Add(klasseSicht);
            }

            return Ok(result);
        }

        public class LehrerSicht
        {
            public int Id { get; set; }
            public Dictionary<string,List<KlasseSicht>> Jahrgaenge { get; set; } = new Dictionary<string, List<KlasseSicht>>();
        }

        public class KlasseSicht
        {
            public string Klasse { get; set; }
            public List<FachSicht> Faecher { get; set; } = new List<FachSicht>();
        }

        public class FachSicht
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<SchuelerSicht> Schueler { get; set; } = new List<SchuelerSicht>();
        }

        public class SchuelerSicht
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<NoteSicht> Noten { get; set; }
        }

        public class NoteSicht
        {
            public int Note;
            public DateTime Datum;
            public string Typ;
        }
    }
}
