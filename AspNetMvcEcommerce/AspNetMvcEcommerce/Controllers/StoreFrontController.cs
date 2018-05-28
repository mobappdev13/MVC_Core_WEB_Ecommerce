using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMvcEcommerce.DAL;
using AspNetMvcEcommerce.Models;

namespace AspNetMvcEcommerce.Controllers
{
    public class StoreFrontController : Controller
    {

        private EcommerceContext db = new EcommerceContext();
        // GET: StoreFront
        public ActionResult Index(int? id)
        {
            var mid = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
    }
}