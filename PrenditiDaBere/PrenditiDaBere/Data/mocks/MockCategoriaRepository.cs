using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Data.mocks
{
    public class MockCategoriaRepository : ICategoriaRepository
    {
        public IEnumerable<Categoria> Categorie {
            get
            { 
                return new List<Categoria>
            {
                new Categoria { NomeCategoria = "Alcoliche", Descrizione = "Tutte le Bibite Alcoliche" },
                new Categoria { NomeCategoria = "Analcoliche", Descrizione = "Tutte le Bibite Analcoliche" }
            };
           }
        }
    }
}
