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
    public class SupplierClassesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        // GET: SupplierClasses
        public ActionResult Index()
        {
            return View(db.SupplierClasses.ToList());
        }

        // GET: SupplierClasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierClasse supplierClasse = db.SupplierClasses.Find(id);
            if (supplierClasse == null)
            {
                return HttpNotFound();
            }
            return View(supplierClasse);
        }

        // GET: SupplierClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierClasses/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierClasseId,Description")] SupplierClasse supplierClasse)
        {
            if (ModelState.IsValid)
            {
                db.SupplierClasses.Add(supplierClasse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supplierClasse);
        }

        // GET: SupplierClasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierClasse supplierClasse = db.SupplierClasses.Find(id);
            if (supplierClasse == null)
            {
                return HttpNotFound();
            }
            return View(supplierClasse);
        }

        // POST: SupplierClasses/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierClasseId,Description")] SupplierClasse supplierClasse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierClasse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierClasse);
        }

        // GET: SupplierClasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierClasse supplierClasse = db.SupplierClasses.Find(id);
            if (supplierClasse == null)
            {
                return HttpNotFound();
            }
            return View(supplierClasse);
        }

        // POST: SupplierClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierClasse supplierClasse = db.SupplierClasses.Find(id);
            db.SupplierClasses.Remove(supplierClasse);
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
