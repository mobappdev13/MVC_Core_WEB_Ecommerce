using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Data.Interfaces
{
    public interface IBibitaRepository
    {
        IEnumerable<Bibita> Bibite { get; }

        IEnumerable<Bibita> BibitePreferite { get; }

        Bibita GetBibitaById(int bibtaId);
    }
}
