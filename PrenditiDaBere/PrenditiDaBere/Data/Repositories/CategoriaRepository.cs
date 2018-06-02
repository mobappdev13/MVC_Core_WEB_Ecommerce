using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Categoria> Categorie => _appDbContext.Categorie;
    }

}
