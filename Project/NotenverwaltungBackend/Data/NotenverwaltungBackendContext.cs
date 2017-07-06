using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Data
{
    public class NotenverwaltungBackendContext : DbContext
    {
        public NotenverwaltungBackendContext (DbContextOptions<NotenverwaltungBackendContext> options)
            : base(options)
        {
        }

        public DbSet<NotenverwaltungBackend.Model.Notenerhebung> Notenerhebung { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Fach> Fach { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Klasse> Klasse { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Schueler> Schueler { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Person> Person { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Jahrgang> Jahrgang { get; set; }
        public DbSet<NotenverwaltungBackend.Model.Lehrer> Lehrer { get; set; }
    }
}
