using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrenditiDaBere.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Models;
using PrenditiDaBere.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace PrenditiDaBere.Controllers
{
    public class ShoppingCartController : Controller
    {
            private readonly IBibitaRepository _bibitaRepository;
            private readonly ShoppingCart _shoppingCart;

            public ShoppingCartController(IBibitaRepository bibitaRepository, ShoppingCart shoppingCart)
            {
                _bibitaRepository = bibitaRepository;
                _shoppingCart = shoppingCart;
            }

            [Authorize]
            public ViewResult Index()
            {
                var items = _shoppingCart.GetShoppingCartItems();
                _shoppingCart.ShoppingCartItems = items;

                var shoppingCartViewModel = new ShoppingCartViewModel
                {
                    ShoppingCart = _shoppingCart,
                    ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
                };
                return View(shoppingCartViewModel);
            }

            [Authorize]
            public RedirectToActionResult AddToShoppingCart(int bibitaId)
            {
                var selectedBibita = _bibitaRepository.Bibite.FirstOrDefault(b => b.BibitaId == bibitaId);
                if (selectedBibita != null)
                {
                    _shoppingCart.AddToCart(selectedBibita, 1);
                }
                return RedirectToAction("Index");
            }

            public RedirectToActionResult RemoveFromShoppingCart(int bibitaId)
            {
                var selectedBibita = _bibitaRepository.Bibite.FirstOrDefault(b => b.BibitaId == bibitaId);
                if (selectedBibita != null)
                {
                    _shoppingCart.RemoveFromCart(selectedBibita);
                }
                return RedirectToAction("Index");
            }

        }
    }