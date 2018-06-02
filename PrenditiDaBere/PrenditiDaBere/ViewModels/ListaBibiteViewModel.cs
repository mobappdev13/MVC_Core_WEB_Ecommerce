using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.ViewModels
{
    public class ListaBibiteViewModel
    {
        public IEnumerable<Bibita> Bibite { get; set; }
        public string CategoriaCorrente { get; set; }
    }
}
