namespace LCA_WEB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Durability { get; set; }

        public double TotalWeight { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DateOfChanging { get; set; }

        public string CreatedBy { get; set; }

        public string ChangeBy { get; set; }

        public int? Typ_Id { get; set; }

      //  public virtual ProductType ProductType { get; set; }
    }
}
