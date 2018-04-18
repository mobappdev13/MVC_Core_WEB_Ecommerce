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
    public class AttributeOptValuesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private User user;

        // GET: AttributeOptValues
        //int? attributeOptId
        public ActionResult Index()
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            var attributeOptValues = db.AttributeOptValues
                .Include(a => a.AttributeOpt)
                .Include(a => a.Company)
                .Where(a => a.CompanyId == user.CompanyId);
          
            return View(attributeOptValues
                .OrderBy(a => a.AttributeOptId)
                .ThenBy (a => a.ValueAttribute)
                .ToList());
        }

        // GET: AttributeOptValues/Details/5
        public ActionResult Details(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeOptValue attributeOptValue = db.AttributeOptValues.Find(id);
            if (attributeOptValue == null)
            {
                return HttpNotFound();
            }
            return View(attributeOptValue);
        }

        // GET: AttributeOptValues/Create
        //
        public ActionResult Create(int? a, string desc)
        {
            //var mia = desc;
            if (String.IsNullOrEmpty(desc))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AttributeOptId

            var attributeOPT = db.AttributeOpts.FirstOrDefault(ao =>ao.Description == desc);
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var company = user.CompanyId;

           // TextAttribute = "TEST",
            var attributeOptValue = new AttributeOptValue {
                AttributeOptId = attributeOPT.AttributeOptId,
                TextAttribute = attributeOPT.Description, //"TEST",
                CompanyId = user.CompanyId
            };

            
            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts.Where(ao => ao.AttributeOptId == attributeOptValue.AttributeOptId), "AttributeOptId", "Description");
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name");
            return View(attributeOptValue);
        }

        // POST: AttributeOptValues/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttributeOptValueId,CompanyId,AttributeOptId,TextAttribute,ValueAttribute")] AttributeOptValue attributeOptValue, int? a, string desc)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            //var attributeOPT = db.AttributeOpts.FirstOrDefault(ao => ao.Description == desc);
            //attributeOptValue.CompanyId = user.CompanyId;
            //attributeOptValue.AttributeOptId = attributeOPT.AttributeOptId;
            if (ModelState.IsValid)
            {
                var valueExist = db.AttributeOptValues
                    .Any(
                    aov => aov.CompanyId == attributeOptValue.CompanyId && 
                    aov.AttributeOptId == attributeOptValue.AttributeOptId && 
                    aov.ValueAttribute == attributeOptValue.ValueAttribute);

                //bool valueExist = db.AttributeOptValues
                //    .Any(
                //    aov => aov.CompanyId == attributeOptValue.CompanyId &&
                //    aov.AttributeOptId == attributeOptValue.AttributeOptId &&
                //    aov.ValueAttribute == attributeOptValue.TextAttribute);

                var resultAny = valueExist;
                if (resultAny)
                {
                    //TODO E.g. ModelState.AddModelError
                    TempData["Message"] = "Record dupplicato";
                    ////vediamo
                    //return View(attributeOptValue);

                }
                else
                {
                    TempData["Message"] = string.Empty;
                    //db.A.Add(a);
                    db.AttributeOptValues.Add(attributeOptValue);

                    //db.SaveChange();
                
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        TempData["Message"] = "Record is Saved !";
                    }
                    else
                    {
                        TempData["Message"] = "Record  Not saved !";
                    }
                    //TempData["Message"] = "Thank you for your input.  If requested, we will contact you soon.";
                    // return RedirectToAction("Index");

                }//questo
            }
            //ViewBag.AttributeOptId = new SelectList(db.AttributeOpts, "AttributeOptId", "Description", attributeOptValue.AttributeOptId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", attributeOptValue.CompanyId);
            //return RedirectToAction("Index","home");

            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts.Where(ao => ao.AttributeOptId == attributeOptValue.AttributeOptId), "AttributeOptId", "Description", attributeOptValue.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOptValue.CompanyId);
            //return View(attributeOptValue); rimane in questa pagina no scrive record saved
            return View();
            //return RedirectToAction("Index"); ok

        }

        // GET: AttributeOptValues/Edit/5
        public ActionResult Edit(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AttributeOptValue attributeOptValue = db.AttributeOptValues.Find(id);

            if (attributeOptValue == null)
            {
                return HttpNotFound();
            }
            //
            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts.Where(ao => ao.AttributeOptId == attributeOptValue.AttributeOptId), "AttributeOptId", "Description", attributeOptValue.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOptValue.CompanyId);
            //return View(attributeOptValue); rimane in questa pagina no scrive record saved
            //return View();
            //
            return View(attributeOptValue);
        }

        // POST: AttributeOptValues/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttributeOptValueId,CompanyId,AttributeOptId,TextAttribute,ValueAttribute")] AttributeOptValue attributeOptValue)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (ModelState.IsValid)
            {
                db.Entry(attributeOptValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts.Where(ao => ao.AttributeOptId == attributeOptValue.AttributeOptId), "AttributeOptId", "Description", attributeOptValue.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies.Where(c => c.CompanyId == user.CompanyId), "CompanyId", "Name", attributeOptValue.CompanyId);
            return View(attributeOptValue);
        }

        // GET: AttributeOptValues/Delete/5
        public ActionResult Delete(int? id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttributeOptValue attributeOptValue = db.AttributeOptValues.Find(id);
            if (attributeOptValue == null)
            {
                return HttpNotFound();
            }
            // return View(attributeOptValue);
            return View(attributeOptValue);
        }

        // POST: AttributeOptValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            AttributeOptValue attributeOptValue = db.AttributeOptValues.Find(id);
            db.AttributeOptValues.Remove(attributeOptValue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
