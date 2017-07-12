namespace NotenverwaltungBackend.Model
{
    public class FachLehrer
    {
        public int FachID { get; set; }
        public int LehrerID { get; set; }

        public virtual Fach Fach { get; set; }
        public virtual Lehrer Lehrer { get; set; }
    }
}