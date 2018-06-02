using System;
using System.Collections.Generic;
using System.Linq;
using PrenditiDaBere.Data.Models;
using System.Threading.Tasks;

namespace PrenditiDaBere.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }
}
