//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LCA_WEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EndOfLifeData
    {
        public int Produkt_Id { get; set; }
        public int Rohstoff_Id { get; set; }
        public Nullable<decimal> Recyclingfaehig { get; set; }
        public Nullable<decimal> Entsorgungskosten { get; set; }
        public Nullable<decimal> Recyclingerloese { get; set; }
        public Nullable<decimal> WeiterverkaufPM { get; set; }
    
        public virtual Produkt Produkt { get; set; }
        public virtual Rohstoff Rohstoff { get; set; }
    }
}
