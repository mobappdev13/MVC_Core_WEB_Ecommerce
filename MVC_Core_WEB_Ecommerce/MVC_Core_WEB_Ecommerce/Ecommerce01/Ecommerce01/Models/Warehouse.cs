using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Index("Warehouse_CompanyId_Name_Index", 1,IsUnique = true)]
        [Display(Name = "Azienda")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Index("Warehouse_CompanyId_Name_Index", 2, IsUnique = true)]
        [Display(Name = "Magazzino")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [StringLength(20, MinimumLength = 10)]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
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
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Provincia")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare un {0}")]
        [Display(Name = "Comune")]
        public int CityId { get; set; }

        public virtual Departament Departament { get; set; }

        public virtual Province Province { get; set; }

        public virtual City City { get; set; }

        public virtual Company Company { get; set; }

    }
}