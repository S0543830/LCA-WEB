using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LCA_WEB.Models
{
    public class ProduktionsmittelViewModel
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }

        [StringLength(20, MinimumLength = 4, ErrorMessage = "Must be at least 4 characters long.")]

        [Display(Name = "Produktionsmittel")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please write your LastName")]
        [Display(Name = "Typ_1")]
        public RohstoffViewModel RohstoffTyp_1 { get; set; }
        [Display(Name = "Typ_2")]
        public RohstoffViewModel RohstoffTyp_2 { get; set; }
        [Display(Name = "Typ_3")]
        public RohstoffViewModel RohstoffTyp_3 { get; set; }
        [Display(Name = "Typ_4")]
        public RohstoffViewModel RohstoffTyp_4 { get; set; }
    }
}
