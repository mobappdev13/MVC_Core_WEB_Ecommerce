using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Models;

namespace Ecommerce01.Controllers
{
    
    public class HomeController : Controller
    {
        //last add
        private Ecommerce01Context db = new Ecommerce01Context();

        public ActionResult Index()
        {
            //usuario loggeado User.Identity.Name
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            return View(user);
            //var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            //return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Pagina che descrive la propria applicazione.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pagine dei contatti.";

            return View();
        }
    }
}