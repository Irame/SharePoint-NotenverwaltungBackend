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
            
            var schueler = await _context.Schueler
                .Include(s => s.Person)
                .Where(s => s.Person.Benutzername == benutzername)
                .Include(x => x.FachSchueler).ThenInclude(x => x.Fach)
                .Include(x => x.KlasseSchueler).ThenInclude(x => x.Klasse).ThenInclude(x => x.Jahrgang)
                .Include(x => x.Noten)
                .SingleOrDefaultAsync();
            
            if (schueler == null)
            {
                return NotFound();
            }

            var result = new SchuelerSicht {Id = schueler.SchuelerID};

            foreach (var klasse in schueler.KlasseSchueler.Select(x => x.Klasse))
            {
                var klasseSicht = new KlasseSicht { Jahrgang = klasse.Jahrgang.Name, Klasse = klasse.Name };

                var feacher = schueler.FachSchueler.Select(x => x.Fach);
                var noten = schueler.Noten;

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
                double feacherDurchschnittSumme = 0;
                foreach (var fachSicht in klasseSicht.Faecher)
                {
                    fachSicht.Durchschnitt = (double) fachSicht.Noten.Sum(x => x.Typ == "Schulaufgabe" ? x.Note * 2 : x.Note) / fachSicht.Noten.Sum(x => x.Typ == "Schulaufgabe" ? 2 : 1);
                    feacherDurchschnittSumme += fachSicht.Durchschnitt;
                }
                klasseSicht.Durchschnitt = feacherDurchschnittSumme / klasseSicht.Faecher.Count;
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
            public double Durchschnitt { get; set; }
            public List<FachSicht> Faecher { get; set; } = new List<FachSicht>();
        }

        public class FachSicht
        {
            public string Name { get; set; }
            public double Durchschnitt { get; set; }
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
