namespace NotenverwaltungBackend.Model
{
    public class KlasseSchueler
    {
        public int KlasseID { get; set; }
        public int SchuelerID { get; set; }

        public virtual Klasse Klasse { get; set; }
        public virtual Schueler Schueler { get; set; }
    }
}