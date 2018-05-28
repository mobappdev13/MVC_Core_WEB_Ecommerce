using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppEcommerce.DAL;
using WebAppEcommerce.Models;
using WebAppEcommerce.ViewModels;

namespace WebAppEcommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        private WebAppEcommerceContext db = new WebAppEcommerceContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
           // return View(db.ShoppingCarts.ToList());
        }

        //add
        public ActionResult AddToCart(int id)
        {
            var addedProduct = db.Products.Single(product => product.Id == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedProduct);

            return RedirectToAction("Index");
        }

        //remove
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string productName = db.Carts.FirstOrDefault(item => item.ProductId == id).Product.Name;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) + " has been removed from your shopping cart",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }
           
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }

}
