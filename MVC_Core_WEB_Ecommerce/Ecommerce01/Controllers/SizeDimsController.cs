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
    public class SizeDimsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        // GET: SizeDims
        public ActionResult Index()
        {
            return View(db.SizeDims.ToList());
        }

        // GET: SizeDims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeDim sizeDim = db.SizeDims.Find(id);
            if (sizeDim == null)
            {
                return HttpNotFound();
            }
            return View(sizeDim);
        }

        // GET: SizeDims/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeDims/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SizeDimId,CountryLanguage,Description")] SizeDim sizeDim)
        {
            if (ModelState.IsValid)
            {
                db.SizeDims.Add(sizeDim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sizeDim);
        }

        // GET: SizeDims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeDim sizeDim = db.SizeDims.Find(id);
            if (sizeDim == null)
            {
                return HttpNotFound();
            }
            return View(sizeDim);
        }

        // POST: SizeDims/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SizeDimId,CountryLanguage,Description")] SizeDim sizeDim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeDim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizeDim);
        }

        // GET: SizeDims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeDim sizeDim = db.SizeDims.Find(id);
            if (sizeDim == null)
            {
                return HttpNotFound();
            }
            return View(sizeDim);
        }

        // POST: SizeDims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SizeDim sizeDim = db.SizeDims.Find(id);
            db.SizeDims.Remove(sizeDim);
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
