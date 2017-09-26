

using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using LCA_WEB.Models;

namespace LCA_WEB.Models
{
    public class ProduktDataList
    {
        public List<Produkt_Typ_Rohstoff_Indikator> _ListProduct { get; set; }

    }
    public class Produkt_Typ_Rohstoff_Indikator
    {
        public List<ProduktTyp> _Typ_Id { get; set; }
        public  ProduktTyp _ProduktTyp { get; set; }
        public Produkt _Produkt { get; set; }
        public List<Rohstoffe> _Rohstoffe { get; set; }
        public Rohstoff _Rohstoff { get; set; }
        public List<Rohstoff> _LRohstoff { get; set; }
        public List<Umweltindikator> _LIndikator { get; set; }
        public Umweltindikator _Indikator { get; set; }
        public EndOfLifeData _EndOfLifeData { get; set; }
        public Umweltindikatorwert _Umweltindikatorwert { get; set; }
        public List<Umweltindikatorwert> _LUmweltindikatorwert { get; set; }
        public int _RowRohstoff { get; set; }
        public int _RowIndikator { get; set; }
        public List<ProduktRohstoff> ProduktRohstoff { get; set; }
    }

    public class ProduktRohstoff
    {
        public Rohstoff Rohstoff { get; set; }
        public List<Umweltindikatorwert> LRohstoffe { get; set; }
    }

    

    public class RohstoffIndikatorBeziehung
    {
        public int AnzahlIndikatorProRohstoff { get; set; }
    }

    public class ProduktTypViewViewModel
    {
        public ProduktTyp _ProduktTyp { get; set; }
        public string _View { get; set; }
    }

    public class IndikatorViewViewModel
    {
        public Umweltindikator _Umweltindikator { get; set; }
        public string _View { get; set; }
    }

    public class RohstoffViewViewModel
    {
        public Rohstoffe _Rohstoffe { get; set; }
        public string _View { get; set; }
    }

    // Model for the diagrams
    //DataContract for Serializing Data - required to serve in JSON format
    [DataContract]
    public class DataPoint
    {
        public DataPoint(string label, int? y)
        {
            this.Y = y;
            this.Label = label;
        }

        //Explicitly setting the name to be used while serializing to JSON. 
        [DataMember(Name = "label")]
        public string Label = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<int> Y = null;

    }
}

