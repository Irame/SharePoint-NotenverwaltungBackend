using System.Collections.Generic;

namespace NotenverwaltungBackend.Model
{
    public class Fach
    {
        public int FachID { get; set; }
        public string Name { get; set; }
        public int JahrgangID { get; set; }

        public virtual Jahrgang Jahrgang { get; set; }
        public virtual ICollection<FachLehrer> FachLehrer { get; set; }
        public virtual ICollection<KlasseFach> KlasseFach { get; set; }
    }
}