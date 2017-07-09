using System;
using System.ComponentModel.DataAnnotations;

namespace LCA_WEB.Models
{
    public class HomeViewModel
    {

    }

    public class ProduktViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Menge { get; set; }
        public double? Nutzungsdauer_in_Jahre { get; set; }
    }
}