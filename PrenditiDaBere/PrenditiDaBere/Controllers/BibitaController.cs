using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrenditiDaBere.Controllers
{
    public class BibitaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IBibitaRepository _bibitaRepository;


        public BibitaController(ICategoriaRepository categoriaRepository, IBibitaRepository bibitaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _bibitaRepository = bibitaRepository;

        }
        // GET: /<controller>/
        public ViewResult List(string categoria)
        {
            string _categoria = categoria;
            IEnumerable<Bibita> bibite;
            string categoriaCorrente = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                bibite = _bibitaRepository.Bibite.OrderBy(p => p.BibitaId);
                categoriaCorrente = "Tutte le Bibite";
            }
            else
            {
                if (string.Equals("Alcolica", _categoria, StringComparison.OrdinalIgnoreCase))
                    bibite = _bibitaRepository.Bibite.Where(p => p.Categoria.NomeCategoria.Equals("Alcolica")).OrderBy(p => p.Nome);
                else
                    bibite = _bibitaRepository.Bibite.Where(p => p.Categoria.NomeCategoria.Equals("Analcolica")).OrderBy(p => p.Nome);


                categoriaCorrente = _categoria;
            }

            return View(new ListaBibiteViewModel
            {
                Bibite = bibite,
                CategoriaCorrente = categoriaCorrente
            });
        }

        //search
        public ViewResult Search(string ricercaString)
        {
            string _ricercaString = ricercaString;
            IEnumerable<Bibita> bibite;
            string categoriaCorrente = string.Empty;

            if (string.IsNullOrEmpty(_ricercaString))
            {
                bibite = _bibitaRepository.Bibite.OrderBy(p => p.BibitaId);
            }
            else
            {
                bibite = _bibitaRepository.Bibite.Where(p => p.Nome.ToLower().Contains(_ricercaString.ToLower()));
            }

            return View("~/Views/Bibita/List.cshtml", new ListaBibiteViewModel { Bibite = bibite, CategoriaCorrente = "Tutte le Bibite" });
          //return View(viewName: "~/Views/")
        }

        //dettagli
        public ViewResult Details(int bibitaId)
        {
            var bibita = _bibitaRepository.Bibite.FirstOrDefault(d => d.BibitaId == bibitaId);
            if (bibita == null)
            {
                return View("~/Views/Error/Error.cshtml");
            }
            return View(bibita);
        }
    }
}
