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
    
    public partial class Umweltindikator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Umweltindikator()
        {
            this.Umweltindikatorwerts = new HashSet<Umweltindikatorwert>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Scope { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Umweltindikatorwert> Umweltindikatorwerts { get; set; }
    }
}
