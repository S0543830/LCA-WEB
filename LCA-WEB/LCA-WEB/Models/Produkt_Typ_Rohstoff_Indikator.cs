

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using LCA_WEB.Models;

namespace LCA_WEB.Models
{
    public class Produkt_Typ_Rohstoff_Indikator
    {
        public List<ProduktTyp> _Typ_Id { get; set; }
        public  ProduktTyp _ProduktTyp { get; set; }
        public Produkt _Produkt { get; set; }
        public List<Rohstoffe> _Rohstoffe { get; set; }
        public Rohstoff _Rohstoff { get; set; }
        public List<Umweltindikator> _LIndikator { get; set; }
        public Umweltindikator _Indikator { get; set; }
        public EndOfLifeData _EndOfLifeData { get; set; }
        public Umweltindikatorwert _Umweltindikatorwert { get; set; }
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
}

