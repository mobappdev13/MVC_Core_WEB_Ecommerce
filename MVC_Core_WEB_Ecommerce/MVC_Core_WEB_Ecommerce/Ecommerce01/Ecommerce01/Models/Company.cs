using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Company
    { 
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(60, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "RagioneSociale-Azienda")]
        [Index("City_Name_Index", 2, IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [StringLength(20, MinimumLength = 10)]
        [Display(Name = "Telefono")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Telefono deve ser numerico")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(200, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "SedeOperativa ")]
        public string AddressO { get; set; }

        //[Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(200, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "SedeLegale")]
        public string AddressL { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Regione")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Index("City_Name_Index", 1, IsUnique = true)]
        public int DepartamentId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Provincia")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "Città-Comune")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        public int CityId { get; set; }

        //[Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(100, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Località")]
        public string Locality { get; set; }

        [MaxLength(15, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Partita Iva")]
        public string PartitaIva { get; set; }

        [MaxLength(15, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Cod.Fiscale")]
        public string CodiceFiscale { get; set; }

        //[Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [StringLength(20, MinimumLength = 10)]
        [Display(Name = "Cellulare")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Telefono deve ser numerico")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneMobil { get; set; }

        public string Fax { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Display(Name = "E-mail")]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})$", ErrorMessage = "Indirizzo e-amail deve essere in formatto corretto !")]
        public string Email { get; set; }

        public string http { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        public virtual City City { get; set; }

        public virtual Province Province { get; set; }

        public virtual Departament Departament { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<CompanyCustomer> CompanyCustomers { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }

    }
}
