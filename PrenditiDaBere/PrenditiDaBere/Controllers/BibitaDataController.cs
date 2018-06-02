using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.ViewModels;

namespace PrenditiDaBere.Controllers
{
    [Route("api/[controller]")]
    public class BibitaDataController : Controller
    {
        private readonly IBibitaRepository _bibitaRepository;

        public BibitaDataController(IBibitaRepository bibitaRepository)
        {
            _bibitaRepository = bibitaRepository;
        }

        [HttpGet]
        public IEnumerable<BibitaViewModel> CaricaPiuBibite()
        {
            IEnumerable<Bibita> dbBibite = null;

            dbBibite = _bibitaRepository.Bibite.OrderBy(p => p.BibitaId).Take(10);

            List<BibitaViewModel> bibite = new List<BibitaViewModel>();

            foreach (var dbBibita in dbBibite)
            {
                bibite.Add(MapDbBibitaToBibitaViewModel(dbBibita));
            }
            return bibite;
        }

        private BibitaViewModel MapDbBibitaToBibitaViewModel(Bibita dbBibita) => new BibitaViewModel()
        {
            BibitaId = dbBibita.BibitaId,
            Nome = dbBibita.Nome,
            Prezzo = dbBibita.Prezzo,
            PiccolaDescrizione = dbBibita.PiccolaDescrizione,
            MiniaturaImmagine = dbBibita.MiniaturaImmagine
        };
    }
}