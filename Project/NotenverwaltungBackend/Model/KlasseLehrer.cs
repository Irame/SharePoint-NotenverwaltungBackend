namespace NotenverwaltungBackend.Model
{
    public class KlasseLehrer
    {
        public int KlasseID { get; set; }
        public int LehrerID { get; set; }

        public virtual Klasse Klasse { get; set; }
        public virtual Lehrer Lehrer { get; set; }
    }
}
