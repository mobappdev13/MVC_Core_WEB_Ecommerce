using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Index("Tax_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Tassa")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [DisplayFormat(ApplyFormatInEditMode = false,
        ConvertEmptyStringToNull = false, DataFormatString = "{0:P2}")]
        [Display(Name = "Aliquota")]
        public double Rate { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Azienda")]
        [Range(1, double.MaxValue, ErrorMessage = "Selezionare una {0}")]
        [Index("Tax_CompanyId_Description_Index", 1, IsUnique = true)]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
