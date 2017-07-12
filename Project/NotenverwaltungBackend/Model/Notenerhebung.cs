using System;

namespace NotenverwaltungBackend.Model
{
    public class Notenerhebung
    {
        public int NotenerhebungID { get; set; }
        public int FachID { get; set; }
        public DateTime Datum { get; set; }
        public string Typ { get; set; }
        public int Note { get; set; }
        public int SchuelerID { get; set; }
        public string Bemerkung { get; set; }

        public virtual Schueler Schueler { get; set; }
        public virtual Fach Fach { get; set; }
    }
}