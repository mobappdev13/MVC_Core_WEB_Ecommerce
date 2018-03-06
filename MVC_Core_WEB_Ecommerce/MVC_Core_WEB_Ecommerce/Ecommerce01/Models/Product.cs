using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Devi selezionare una {0}")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_BarCode_Index", 1, IsUnique = true)]
        [Display(Name = "Azienda")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Selezionare una {0}")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Prodotto")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(13, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Index("Product_CompanyId_BarCode_Index", 2, IsUnique = true)]
        [Display(Name = "Codice a Barre")]
        public string BarCode { get; set; }

        //[Required(ErrorMessage = "Questo campo {0} è necessario!")]
        //[Range(1, double.MaxValue, ErrorMessage = "Selezionare una {0}")]
        //[Display(Name = "Fornitore")]
        //public int SupplierId { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Selezionare un {0} fra {1} e {2}")]
        [Display(Name = "PrezzoUnitario Acquisto")]
        public decimal VendorPrice { get; set; }

       
        //[Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(13, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Supplier Code")]
        public string VendorProductCode { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Range(0, double.MaxValue, ErrorMessage = "Selezionare un {0} fra {1} e {2}")]
        [Display(Name = "Prezzo Vendita")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [Range(1, double.MaxValue, ErrorMessage = "Selezionare una {0}")]
        [Display(Name = "Tassa")]
        public int TaxId { get; set; }
        
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public HttpPostedFileBase ImageFile { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Altro")]
        public string Remarks { get; set; }

        //last add
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Stock")]
        public double Stock
        {
            get
            {
                return Inventories?.Sum(i => i.Stock) ?? 0;
                //return 0;
            }
        }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Punto di riordino")]
        public double ReorderPoint { get; set; }

        public virtual Company Company { get; set; }

        public virtual Category Category { get; set; }

        public virtual Tax Tax { get; set; }

       

        //era questo
        public virtual ICollection<Inventory> Inventories { get; set; }

        //public virtual Supplier Supplier { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        //public virtual ICollection<OrderDetailTmp> OrderDetailTmps { get; set; }

    }
}

