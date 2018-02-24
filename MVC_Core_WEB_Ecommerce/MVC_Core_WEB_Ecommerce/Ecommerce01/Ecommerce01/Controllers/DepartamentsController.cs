using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Models;
using PagedList;
using PagedList.Mvc;

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartamentsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 4;
        // GET: Departaments
        public ActionResult Index(int ? page = null)
        {
            page = (page ?? 1);
            var departaments = db.Departaments.OrderBy(d => d.Name);
            return View(departaments.ToPagedList((int)page, itemsonPage));
        }

        // GET: Departaments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // GET: Departaments/Create
        public ActionResult Create()
        {
            return View();
        }


       
        // POST: Departaments/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartamentId,Name,Latitud,Longitud")] Departament departament)
        {
            if (ModelState.IsValid)
            {
                //db.Departaments.Add(departament);
                //verifica dupplicati
                // if (db.Departaments.Any(d => d.Name.Equals(departament.Name)))
                //var singleUser = users.SingleOrDefault(); 
                //var singleDepartamentName = db.Departaments.SingleOrDefault().Name;

                if (db.Departaments.Any(d => d.Name.Equals(departament.Name)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    // ??
                    db.Departaments.Add(departament);
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        //if (exception.InnerException?.InnerException != null && exception.InnerException.InnerException.Message.Contains("_Index"))
                        //{
                        //    ModelState.AddModelError(string.Empty, "Esiste gia un Registro con lo stesso valore");
                        //}
                        //else
                        //{
                            ModelState.AddModelError(string.Empty, exception.Message);
                        //}
                    }
                }
            }
            return View(departament);
        }

        // GET: Departaments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // POST: Departaments/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartamentId,Name,Latitud,Longitud")] Departament departament)
        {
            if (ModelState.IsValid)
            {
                ///
                if (db.Departaments.Any(d => d.Name.Equals(departament.Name)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                { 
                    ////
                    db.Entry(departament).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        if (exception.InnerException?.InnerException != null && exception.InnerException.InnerException.Message.Contains("_Index"))
                        {
                            ModelState.AddModelError(string.Empty, "There are a record with the same value");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, exception.Message);
                        }
                        return View(departament);
                    }
                }
            }
            return View(departament);
        }

        // GET: Departaments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return HttpNotFound();
            }
            return View(departament);
        }

        // POST: Departaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departament departament = db.Departaments.Find(id);
            db.Departaments.Remove(departament);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                if (exception.InnerException?.InnerException != null && exception.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "Il Registro non puo essere eliminato perche esso ha degli altri registri collegati!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
                return View(departament);
            }
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
