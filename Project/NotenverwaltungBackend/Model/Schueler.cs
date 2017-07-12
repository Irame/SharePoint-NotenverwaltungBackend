using System.Collections.Generic;

namespace NotenverwaltungBackend.Model
{
    public class Schueler
    {
        public int SchuelerID { get; set; }
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<KlasseSchueler> KlasseSchueler { get; set; }
        public virtual ICollection<Notenerhebung> Noten { get; set; }
    }
}