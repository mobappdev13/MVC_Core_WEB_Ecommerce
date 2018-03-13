using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryAspNetMVC.Models;

namespace RepositoryAspNetMVC.Controllers
{
    public class CategoryController : Controller
    {
        private ICRUDRepository<Category> categoryCRUDRepository;

        public CategoryController()
        {
            categoryCRUDRepository = new CRUDRepository<Category>();
        }

        // GET: Category
        public ActionResult Index()
        {
            ViewBag.categories = categoryCRUDRepository.findAll()
            .OrderBy(c => c.CategoryId)
            .ThenBy(c => c.Description);

            return View();
        }
    }
}