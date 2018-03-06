using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce01.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Questo campo {0} è necessario!")]
        [MaxLength(50, ErrorMessage = "Questo campo {0} deve essere lungo {1} caratteri!")]
        [Display(Name = "Stato Raggiunto")]
        [Index("State_Description_Index", 2, IsUnique = true)]
        //[Index("State_Description_Index", IsUnique = true)]
        public string Description { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }

    }
}