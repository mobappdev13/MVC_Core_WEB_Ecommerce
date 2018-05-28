using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppEcommerce.DAL;
using WebAppEcommerce.Models;

namespace WebAppEcommerce.Controllers
{
    public class StoreFrontController : Controller
    {
        WebAppEcommerceContext db = new WebAppEcommerceContext();
        // GET: StoreFront
        public ActionResult Index(int? id)
        {
            var myId = id;
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