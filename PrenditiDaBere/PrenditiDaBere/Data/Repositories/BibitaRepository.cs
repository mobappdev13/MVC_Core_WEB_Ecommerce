using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.Data.Repositories;

namespace PrenditiDaBere.Data.Repositories
{
    public class BibitaRepository : IBibitaRepository
    {
        private readonly AppDbContext _appDbContext;

        public BibitaRepository(AppDbContext appDbContext)
        {
         _appDbContext = appDbContext;
        }
      
        public IEnumerable<Bibita> Bibite  => _appDbContext.Bibite.Include(c => c.Categoria); 
        public IEnumerable<Bibita> BibitePreferite => _appDbContext.Bibite
            .Where(p => p.BibitaPreferita)
            .Include(c => c.Categoria);

        
        public Bibita GetBibitaById(int bibtaId) => _appDbContext.Bibite
            .FirstOrDefault(p => p.BibitaId == bibtaId);
        
    }
}
