using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Models;

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "User")]
    public class AttributeOptsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        //private List<AttributeOpt> attributteOpt_list;
        private User user;


        // GET: AttributeOpts
        public ActionResult Index()
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var userCompany = db.Users
                 .Include(u => u.Company)
                 .Where(u => u.UserName == User.Identity.Name).ToList();


            userCompany = userCompany.Where(pr => userCompany.Any(p => pr.Company.Name == p.Company.Name)).ToList();  

            ViewData["Azienda"] = userCompany[0].Company.Name;

            var attributeOpts = db.AttributeOpts
                .Include(a => a.Company)
                .Where(a => a.CompanyId == user.CompanyId)
                .OrderBy(a => a.Description);


            //ViewData["Azienda"]

            //attributteOpt_list = attributeOpts.ToList();
            //foreach (var item in attributteOpt_list)
            //{
            //    TempData["Descriptions"] = item.Description;
            //}
            //ViewData["Azienda"] = "QUESTA Azienda";

            return View(attributeOpts
                .OrderBy(a => a.Description)
                .ToList());
        }

        // GET: AttributeOpts/Details/5
        public ActionResult Details(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var userCompany = db.Users
                .Include(u => u.Company)
                .Where(u => u.UserName == User.Identity.Name).ToList();


            userCompany = userCompany.Where(pr => userCompany.Any(p => pr.Company.Name == p.Company.Name)).ToList();

            ViewData["Azienda"] = userCompany[0].Company.Name;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeOpt attributeOpt = db.AttributeOpts.Find(id);

            if (attributeOpt == null)
            {
                return HttpNotFound();
            }
            ViewData["Attributo"] = attributeOpt.Description;
            return View(attributeOpt);
        }

        // GET: AttributeOpts/Create
        public ActionResult Create()
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            return View();
        }

        // POST: AttributeOpts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttributeOptId,CompanyId,Description")] AttributeOpt attributeOpt)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (ModelState.IsValid)
            {
                db.AttributeOpts.Add(attributeOpt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOpt.CompanyId);
            return View(attributeOpt);
        }

        // GET: AttributeOpts/Edit/5
        public ActionResult Edit(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //last add
            var userCompany = db.Users
                .Include(u => u.Company)
                .Where(u => u.UserName == User.Identity.Name).ToList();

            userCompany = userCompany.Where(pr => userCompany.Any(p => pr.Company.Name == p.Company.Name)).ToList();
            ViewData["Azienda"] = userCompany[0].Company.Name;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeOpt attributeOpt = db.AttributeOpts.Find(id);
            if (attributeOpt == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOpt.CompanyId);
            return View(attributeOpt);
        }

        // POST: AttributeOpts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttributeOptId,CompanyId,Description")] AttributeOpt attributeOpt)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //,ValueAttribute
            if (ModelState.IsValid)
            {
                db.Entry(attributeOpt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOpt.CompanyId);
            return View(attributeOpt);
        }

        // GET: AttributeOpts/Delete/5
        public ActionResult Delete(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeOpt attributeOpt = db.AttributeOpts.Find(id);
            if (attributeOpt == null)
            {
                return HttpNotFound();
            }
            return View(attributeOpt);
        }

        // POST: AttributeOpts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            AttributeOpt attributeOpt = db.AttributeOpts.Find(id);
            db.AttributeOpts.Remove(attributeOpt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult SetAttribute(int id)
        //{
        //    var controller = DependencyResolver.Current.GetService<AttributeOptValuesController>();
        //    controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

        //    //return RedirectToAction("Index");
        //    return RedirectToAction("AttributeOptValues");
        //    //            "FileUploadMsgView",
        //    //            new { FileUploadMsg = "File uploaded successfully" });
        //    //return (View);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
