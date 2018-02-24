using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(256, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "E-Mail")]
        [Index("User_UserName_Index", IsUnique = true)]
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

       
        [Display(Name = "Data di Nascita")]
        //[Range(typeof(DateTime), "01/01/1910", "01/12/2050")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateBirth { get; set; }


        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [StringLength(20, MinimumLength = 10)]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(100, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Indirizzo")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Regione")]
        public int DepartamentId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Provincia")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Comune")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Azienda")]
        public int CompanyId { get; set; }

        [Display(Name = "Utente")]
        public string FullName { get {return string.Format("{0} {1}", FirstName, LastName); } }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        public virtual Departament Departament { get; set; }

        public virtual Province Province { get; set; }

        public virtual City City { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}