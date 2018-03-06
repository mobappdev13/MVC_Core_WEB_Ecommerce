using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    //da controllRE

    public class Supplier
    {

        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(256, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Cognome")]
        public string LastName { get; set; }

        [Display(Name = "Fornitore")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Display(Name = "Data Creazione")]
        //[Range(typeof(DateTime), "01/01/1910", "01/12/2050")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated.HasValue
                   ? this.dateCreated.Value
                   : DateTime.Now;
            }

            set { this.dateCreated = value; }
        }

        private DateTime? dateCreated = null;

        [Display(Name = "Preferito")]
        public bool IsfavoriteSupplier { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(20, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(100, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Indirizzo")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Regione")]
        public int DepartamentId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Provincia")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Città")]
        public int CityId { get; set; }

        public virtual Departament Departament { get; set; }

        public virtual Province Province { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<CompanySupplier> CompanySuppliers { get; set; }


        //public virtual ICollection<Product> Products { get; set; }

        //public virtual ICollection<Inventory> Inventories { get; set; }
    }
}