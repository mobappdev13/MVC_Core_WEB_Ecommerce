using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Città-Comune")]
        [Index("City_Name_Index", 2, IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [StringLength(5, MinimumLength = 5)]
        [Display(Name = "CAP")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cap deve ser numerico")]
        public string SigCap { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Regione")]
        [Range(1, double.MaxValue, ErrorMessage = "Selezionare un {0}")]
        [Index("City_Name_Index", 1, IsUnique = true)]
        public int DepartamentId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Provincia")]
        [Range(1, double.MaxValue, ErrorMessage = "Selezionare  un {0}")]
        public int ProvinceId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        [Range(1, double.MaxValue, ErrorMessage = "Entrare un valore {0} fra {1} e {2}")]
        [Display(Name = "Latitudine")]
        public decimal? Latitud { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        [Range(1, double.MaxValue, ErrorMessage = "Entrare un valore {0} fra {1} e {2}")]
        [Display(Name = "Longitudine")]
        public decimal? Longitud { get; set; }


        public virtual Province Province { get; set; }

        public virtual Departament Departament { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }

    }
}