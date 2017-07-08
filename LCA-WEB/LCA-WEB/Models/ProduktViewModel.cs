using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCA_WEB.Models
{
    public class ProduktViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Durability { get; set; }
        public double TotalWeight { get; set; }
        public System.DateTime DateOfCreation { get; set; }
        public System.DateTime DateOfChanging { get; set; }
        public string CreatedBy { get; set; }
        public string ChangeBy { get; set; }
        public Nullable<int> Typ_Id { get; set; }
    }
}