using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Models;
using PrenditiDaBere.ViewModels;

namespace PrenditiDaBere.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBibitaRepository _bibitaRepository;

        public HomeController(IBibitaRepository bibitaRepository)
        {
            _bibitaRepository = bibitaRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                BibitePreferite = _bibitaRepository.BibitePreferite
            };
            return View(homeViewModel);
        }
    }
}
