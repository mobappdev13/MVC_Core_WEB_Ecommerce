using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Classes;
using Ecommerce01.Models;
using PagedList;

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProvincesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 4;
        // GET: Provinces
        public ActionResult Index(int ? page = null)
        {
            page = (page ?? 1);
            var provinces = db.Provinces.Include(p => p.Departament)
                .OrderBy(p => p.Name);
            return View(provinces.ToPagedList((int)page, itemsonPage));
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: Provinces/Create
        public ActionResult Create()
        {
            //add order by "combobox
            ViewBag.DepartamentId = new SelectList(
                DropDownHelper.GetDepartaments(),
                "DepartamentId", 
                "Name");
            return View();
        }

        // POST: Provinces/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProvinceId,Name,SigCap,TwoInitial,Latitud,Longitud,DepartamentId")] Province province)
        {
            if (ModelState.IsValid)
            {
                if (db.Provinces.Any(d => d.Name.Equals(province.Name)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    // ??
                    db.Provinces.Add(province);
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

            ViewBag.DepartamentId = new SelectList(
                DropDownHelper.GetDepartaments(),
                "DepartamentId",
                "Name",
                province.DepartamentId);
            return View(province);
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }


            ViewBag.DepartamentId = new SelectList(
               DropDownHelper.GetDepartaments(),
                "DepartamentId", "" +
                "Name", 
                province.DepartamentId);
            return View(province);
        }

        // POST: Provinces/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProvinceId,Name,SigCap,TwoInitial,Latitud,Longitud,DepartamentId")] Province province)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(province).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                ///
                if (db.Provinces.Any(d => d.Name.Equals(province.Name)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    ////
                    db.Entry(province).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exception)
                    {
                        //if (exception.InnerException?.InnerException != null && exception.InnerException.InnerException.Message.Contains("_Index"))
                        //{
                        //    ModelState.AddModelError(string.Empty," 2 Esiste già un Registro con lo stesso valore");
                        //}
                        //else
                        //{
                            ModelState.AddModelError(string.Empty, exception.Message);
                        //}
                        return View(province);
                    }
                }
            }
            
            ViewBag.DepartamentId = new SelectList(
                DropDownHelper.GetDepartaments(),
                "DepartamentId", 
                "Name", 
                province.DepartamentId);
            return View(province);
        }

        // GET: Provinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Province province = db.Provinces.Find(id);
            db.Provinces.Remove(province);
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
