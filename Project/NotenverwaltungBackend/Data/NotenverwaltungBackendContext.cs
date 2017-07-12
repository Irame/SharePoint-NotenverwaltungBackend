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
        public DbSet<NotenverwaltungBackend.Model.FachLehrer> FachLehrer { get; set; }
        public DbSet<NotenverwaltungBackend.Model.FachSchueler> FachSchueler { get; set; }
        public DbSet<NotenverwaltungBackend.Model.KlasseLehrer> KlasseLehrer { get; set; }
        public DbSet<NotenverwaltungBackend.Model.KlasseSchueler> KlasseSchueler { get; set; }
        public DbSet<NotenverwaltungBackend.Model.KlasseFach> KlasseFach { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FachLehrer>()
                .HasKey(e => new {e.FachID, e.LehrerID});
            modelBuilder.Entity<FachSchueler>()
                .HasKey(e => new { e.FachID, e.SchuelerID });
            modelBuilder.Entity<KlasseLehrer>()
                .HasKey(e => new { e.KlasseID, e.LehrerID });
            modelBuilder.Entity<KlasseSchueler>()
                .HasKey(e => new { e.KlasseID, e.SchuelerID });
            modelBuilder.Entity<KlasseFach>()
                .HasKey(e => new { e.KlasseID, e.FachID });
        }
    }
}
