using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCA_WEB.Models
{
    public class Produkt
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ProduktType Typ { get; set; }

        //Nutzungsdaeuer
        public double Durability { get; set; }

        //Gesamtgewicht
        public double TotalWeight { get; set; }

        //Anlagedatum
        public DateTime DateOfCreation { get; set; }

        //Änderungsdatum
        public DateTime DateOfChanging { get; set; }

        //Angelegt von
        public String CreatedBy { get; set; }

        //Geändert von
        public String ChangeBy { get; set; }

    }

    public class ProduktType
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Typ { get; set; }

    }
}