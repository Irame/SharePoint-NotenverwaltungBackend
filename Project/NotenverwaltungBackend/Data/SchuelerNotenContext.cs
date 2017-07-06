using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotenverwaltungBackend.Model;

namespace NotenverwaltungBackend.Models
{
    public class SchuelerNotenContext : DbContext
    {
        public SchuelerNotenContext (DbContextOptions<SchuelerNotenContext> options)
            : base(options)
        {
        }

        public DbSet<NotenverwaltungBackend.Model.SchuelerNoten> SchuelerNoten { get; set; }
    }
}
