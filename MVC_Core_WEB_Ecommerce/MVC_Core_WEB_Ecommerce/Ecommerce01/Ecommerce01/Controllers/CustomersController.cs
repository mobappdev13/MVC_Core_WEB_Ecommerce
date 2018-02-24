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

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomersController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.City).Include(c => c.Province).OrderBy(c => c.UserName); ;
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name");
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name");
            return View();
        }

        // POST: Customers/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", customer.ProvinceId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", customer.ProvinceId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", customer.ProvinceId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
