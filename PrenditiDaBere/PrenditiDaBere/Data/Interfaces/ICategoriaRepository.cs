using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Data.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorie { get; }       
    }
}
