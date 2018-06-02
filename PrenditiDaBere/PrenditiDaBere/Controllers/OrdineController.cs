using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrenditiDaBere.Data.Interfaces;
using PrenditiDaBere.Data.Models;

namespace PrenditiDaBere.Controllers
{
    public class OrdineController : Controller
    {
        private readonly IOrdineRepository _ordineRepository;
        private readonly ShoppingCart _shoppingCart;

        public OrdineController(IOrdineRepository ordineRepository, ShoppingCart shoppingCart)
        {
            _ordineRepository = ordineRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Ordine ordine)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "La Carta è vuota, prima addizione alcune bibite");
            }

            if (ModelState.IsValid)
            {
                _ordineRepository.CreaOrdine(ordine);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(ordine);
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Grazie per la sua Ordine! :) ";
            return View();
        }
    }
}