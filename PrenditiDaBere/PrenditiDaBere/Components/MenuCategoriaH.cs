using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Interfaces;

namespace PrenditiDaBere.Components
{
    public class MenuCategoriaH : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public MenuCategoriaH(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorie = _categoriaRepository.Categorie.OrderBy(c => c.NomeCategoria);
            return View(categorie);
        }
    }
}
