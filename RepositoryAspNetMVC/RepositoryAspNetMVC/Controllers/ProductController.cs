using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryAspNetMVC.Models;

namespace RepositoryAspNetMVC.Controllers
{
    public class ProductController : Controller
    {
        private ICRUDRepository<Product> productCRUDRepository;
        // GET: Product

        public ProductController()
        {
            productCRUDRepository = new CRUDRepository<Product>();
        }
        public ActionResult Index()
        {
            ViewBag.products = productCRUDRepository.findAll()
            .OrderBy(p => p.ProductId)
            .ThenBy(p => p.CategoryId)
            .ThenBy(p => p.Description);

            return View();
        }
    }
}