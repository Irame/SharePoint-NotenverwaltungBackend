using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotenverwaltungBackend.Model
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Benutzername { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }

    public class Schueler
    {
        public int SchuelerID { get; set; }
        public Person Person { get; set; }
        public virtual ICollection<Klasse> Klassen { get; set; }
        public virtual ICollection<Notenerhebung> Noten { get; set; }
    }

    public class Klasse
    {
        public int KlasseID { get; set; }
        public string Name { get; set; }
        public Jahrgang Jahrgang { get; set; }
        public virtual ICollection<Schueler> Schueler { get; set; }
        public virtual ICollection<Fach> Faecher { get; set; }
        public virtual ICollection<Lehrer> Lehrer { get; set; }
    }

    public class Notenerhebung
    {
        public int NotenerhebungID { get; set; }
        public Fach Fach { get; set; }
        public DateTime Datum { get; set; }
        public string Typ { get; set; }
        public int Note { get; set; }
        public Schueler Schueler { get; set; }
        public string Bemerkung { get; set; }
    }

    public class Jahrgang
    {
        public int JahrgangID { get; set; }
        public string Name { get; set; }
    }

    public class Fach
    {
        public int FachID { get; set; }
        public string Name { get; set; }
        public Jahrgang Jahrgang { get; set; }
        public virtual ICollection<FachLehrer> FachLehrer { get; set; }
        public virtual ICollection<Klasse> Klassen { get; set; }
    }

    public class Lehrer
    {
        public int LehrerID { get; set; }
        public Person Person { get; set; }
        public virtual ICollection<Klasse> Klassen { get; set; }
        public virtual ICollection<FachLehrer> FachLehrer { get; set; }
    }

    public class FachLehrer
    {
        public int FachID { get; set; }
        public int LehrerID { get; set; }

        public virtual Fach Fach { get; set; }
        public virtual Lehrer Lehrer { get; set; }
    }

    public class FachSchueler
    {
        
    }

    public class KlasseSchueler
    {
        
    }

    public class KlasseLehrer
    {
        
    }
}
