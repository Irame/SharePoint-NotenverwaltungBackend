using System.Collections.Generic;

namespace NotenverwaltungBackend.Model
{
    public class Lehrer
    {
        public int LehrerID { get; set; }
        public int PersonID { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<KlasseLehrer> KlasseLehrer { get; set; }
        public virtual ICollection<FachLehrer> FachLehrer { get; set; }
    }
}