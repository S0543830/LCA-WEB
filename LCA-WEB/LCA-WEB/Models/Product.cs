using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCA_WEB.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ProductType Typ { get; set; }

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

    public class ProductType
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Typ { get; set; }

    }

    public class Umweltindikator
    {
        public int ID;
        public String Beschreibung;
        public int ScopeNummer;
    }

    public class UmweltindikatorProduct
    {
        public int ProduktId(Product gProduct)
        {
            return gProduct.Id;
        }

        public int UmweltindikatorId (Umweltindikator gUmwelt)
        {
            return gUmwelt.ID;
        }

        public String Phase { get; set; }
        public int Rohstoff(Rohstoff gRoh)
        {
            return gRoh.Id;    
        }
        public double Menge { get; set; }
        public double Instandhaltungsinterval { get; set; }
        public double Instandhaltungskosten { get; set; }

       //Optional: Nur für die letzte Phase  
        public String Umweltwirkung { get; set; }
    }

    public class Product_Rohstoffbestandteile
    {
        public int ProductID(Product gpro)
        {
            return gpro.Id;
        }

        public int RohstoffID(Rohstoff gRoh)
        {
            return gRoh.Id;
        }

        public double RohstoffMenge
        { get; set; }

        public double RecyclingAnteil
        { get; set; }

        public double RecyclingMenge
        { get; set;}
        public double Entsorgungskosten { get; set; }
        public double Recyclinglöse { get; set; }
    }

    public class Rohstoff
    {
        public int Id {get;set;}
        public String Descrption {get;set;}
    }

    public class Product_Rohstoff
    {
        public Product product;
        public List<Rohstoff> lRohStoff;
        public int Menge;
        public String Einheit;
        public DateTime AnlageDatum;
        public DateTime Änderungsdatum;
        public String AngelegtVon;
        public String GeändertVon;
    }
}