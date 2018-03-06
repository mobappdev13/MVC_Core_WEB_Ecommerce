using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class CompanySupplier
    {
        [Key]
        public int CompanySupplierId { get; set; }

        public int CompanyId { get; set; }

        public int SupplierId { get; set; }

        public virtual Company Company { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}