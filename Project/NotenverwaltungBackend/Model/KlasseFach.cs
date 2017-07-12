namespace NotenverwaltungBackend.Model
{
    public class KlasseFach
    {
        public int KlasseID { get; set; }
        public int FachID { get; set; }

        public virtual Klasse Klasse { get; set; }
        public virtual Fach Fach { get; set; }
    }
}
