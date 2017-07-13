using System.Collections.Generic;

namespace NotenverwaltungBackend.Model
{
    public class Klasse
    {
        public int KlasseID { get; set; }
        public string Name { get; set; }
        public int JahrgangID { get; set; }

        public virtual Jahrgang Jahrgang { get; set; }
        public virtual ICollection<KlasseSchueler> KlasseSchueler { get; set; }
        public virtual ICollection<KlasseLehrer> KlasseLehrer { get; set; }
    }
}