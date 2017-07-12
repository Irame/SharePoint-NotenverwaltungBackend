namespace NotenverwaltungBackend.Model
{
    public class FachSchueler
    {
        public int FachID { get; set; }
        public int SchuelerID { get; set; }

        public virtual Fach Fach { get; set; }
        public virtual Schueler Schueler { get; set; }
    }
}