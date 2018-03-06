using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Index("Inventory_WarehouseId_ProductId_Index", 1, IsUnique = true)]
        public int WarehouseId { get; set; }

        [Index("Inventory_WarehouseId_ProductId_Index", 2, IsUnique = true)]
        public int ProductId { get; set; }

        //[Required]
        //public int SupplierId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public virtual Warehouse Warehouse { get; set; }

        public virtual Product Product { get; set; }

        
        //public virtual Supplier Supplier { get; set; }

    }
}
