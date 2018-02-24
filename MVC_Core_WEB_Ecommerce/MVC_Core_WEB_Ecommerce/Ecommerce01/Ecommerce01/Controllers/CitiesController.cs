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
using PagedList.Mvc;

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 4;
        // GET: Cities
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var cities = db.Cities.Include(c => c.Departament).Include(c => c.Province)
            .OrderBy(c => c.DepartamentId)
            .ThenBy(c => c.ProvinceId)
            .ThenBy(c => c.Name);            
            return View(cities.ToPagedList((int)page, itemsonPage));
            //return View(cities.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // GET: Cities/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name");
            return View();
        }

        // POST: Cities/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityId,Name,SigCap,DepartamentId,ProvinceId,Latitud,Longitud")] City city)
        {
            if (ModelState.IsValid)
            {
                //controllo duplicati
                var existingCitye = db.Cities.SingleOrDefault(
                c => c.Name == city.Name 
                && c.SigCap == city.SigCap);
                if (existingCitye != null)
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    db.Cities.Add(city);
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
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", city.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", city.ProvinceId);
            
            return View(city);
        }

        // GET: Cities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", city.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", city.ProvinceId);
            return View(city);
        }

        // POST: Cities/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,Name,SigCap,DepartamentId,ProvinceId,Latitud,Longitud")] City city)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(city).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                //
                var existingCitye = db.Cities.SingleOrDefault(
                c => c.Name == city.Name
                && c.SigCap == city.SigCap);
                if (existingCitye != null)
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                //
                else
                {
                    ////
                    db.Entry(city).State = EntityState.Modified;
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
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", city.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", city.ProvinceId);

            return View(city);
        }


        // GET: Cities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City city = db.Cities.Find(id);
            db.Cities.Remove(city);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //////add
        ////[HttpGet]
        public JsonResult GetProvinces(int departamentId)
        {
            //can be
            //https://stackoverflow.com/questions/4596371/what-are-the-downsides-to-turning-off-proxycreationenabled-for-ctp5-of-ef-code-f
            //http://www.c-sharpcorner.com/UploadFile/db2972/json-result-in-controller-sample-in-mvc-day-13/
            db.Configuration.ProxyCreationEnabled = false;
            var provinces = db.Provinces.Where(p => p.DepartamentId == departamentId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(provinces);
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
