using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotenverwaltungBackend.Model
{
    public class SchuelerNoten
    {
        [Key]
        public string Benutzername { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Klasse { get; set; }
        public string Halbjahrenszeugnis { get; set; }
        public string Jahreszeugnis { get; set; }
        public double Notendurchschnitt { get; }
        public List<Fach> Feacher { get; set; }
    }

    public class Fach
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Notendurchschnitt { get; }
        public List<Leistungserhebung> Noten { get; set; }
    }

    public class Leistungserhebung
    {
        [Key]
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public NotenTyp Typ { get; set; }
        public double Note { get; set; }
    }

    public enum NotenTyp
    {
        Stehgreifaufgabe, Schulaufgabe
    }
}
