using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Departament
    {
        [Key]
        public int DepartamentId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Regione")]
        [Index("Department_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        [Range(1, double.MaxValue, ErrorMessage = "Entrare un valore {0} fra {1} e {2}")]
        [Display(Name = "Latitudine")]
        public decimal? Latitud { get; set; }


        [DisplayFormat(DataFormatString = "{0:N6}", ApplyFormatInEditMode = true)]
        [Range(1, double.MaxValue, ErrorMessage = "Entrare un valore {0} fra {1} e {2}")]
        [Display(Name = "Longitudine")]
        public decimal? Longitud { get; set; }

       
        //side one to many
        public virtual ICollection<Province> Provinces { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}