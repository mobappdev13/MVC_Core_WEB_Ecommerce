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
    public class SuppliersController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        private const int itemsonPage = 2;
        // GET: Suppliers
        //public ActionResult Index(int? page = null)
        public ActionResult Index()
        {
            //page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var supplierscompany = (from su in db.Suppliers
                                    join cs in db.CompanySuppliers on su.SupplierId equals cs.SupplierId
                                    join co in db.Companies on cs.CompanyId equals co.CompanyId
                                    where co.CompanyId == user.CompanyId
                                    select su)
                                    .Include(s => s.Departament)
                                    .Include(s => s.Province)
                                    .Include(s => s.City)
                                    .Include(s => s.SupplierClasses)
                                    .OrderBy(c => c.UserName)
                                    .ToList();

            ////var customers = customerscompany.Select(item => item.cu).ToList();
            //customers = customers.OrderBy(c => c.UserName).ToList();

            return View(supplierscompany);
            //.ToPagedList((int)page, itemsonPage));
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers
                .Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }

            supplier.City = db.Cities.FirstOrDefault(s => s.CityId == supplier.CityId);
            supplier.Departament = db.Departaments.FirstOrDefault(s => s.DepartamentId == supplier.DepartamentId);
            supplier.Province = db.Provinces.FirstOrDefault(s => s.ProvinceId == supplier.ProvinceId);
            supplier.SupplierClasses = db.SupplierClasses.FirstOrDefault(s => s.SupplierClasseId == supplier.SupplierClasseId);
            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(0), "ProvinceId", "Name");
            ViewBag.SupplierClasseId = new SelectList(DropDownHelper.GetSupplierClasses(0), "SupplierClasseId", "Description");
            return View();
        }

        // POST: Suppliers/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,UserName,Name,DateCreated,PartitaIva,CodiceFiscale,SiteHttp,IsfavoriteSupplier,SupplierClasseId,Phone,PhoneMobil,Fax,Address,CityId,Locality,ProvinceId,DepartamentId")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                using (var tran = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        db.Suppliers.Add(supplier);
                        var response = DbHelper.SaveChanges(db);
                        if (response.Succeeded)
                        {
                            UsersHelper.CreateUserAsp(supplier.UserName, "Supplier");

                            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

                            if (user == null)
                            {
                                return RedirectToAction("Index", "Home");
                            }

                            var companySupplier = new CompanySupplier
                            {
                                CompanyId = user.CompanyId,
                                SupplierId = supplier.SupplierId
                            };
                            db.CompanySuppliers.Add(companySupplier);
                            db.SaveChanges();
                            tran.Commit();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            tran.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        tran.Rollback();
                    }
                }
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.CityId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.ProvinceId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.SupplierClasseId = new SelectList(DropDownHelper.GetSupplierClasses(supplier.SupplierClasseId), "SupplierClasseId", "Description", supplier.SupplierClasseId);
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }
           
            //
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
            ViewBag.SupplierClasseId = new SelectList(DropDownHelper.GetSupplierClasses(supplier.SupplierClasseId), "SupplierClasseId", "Description", supplier.SupplierClasseId);
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,UserName,Name,DateCreated,PartitaIva,CodiceFiscale,SiteHttp,IsfavoriteSupplier,SupplierClasseId,Phone,PhoneMobil,Fax,Address,CityId,Locality,ProvinceId,DepartamentId")] Supplier supplier)
        {
            try
            {
                db.Entry(supplier).State = EntityState.Modified;

                var response = DbHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    using (var db_other = new Ecommerce01Context())
                    {
                        var currentSupplier = db_other.Suppliers.Find(supplier.SupplierId);

                        if (currentSupplier.UserName != supplier.UserName)
                        {
                            UsersHelper.UpdateUserName(currentSupplier.UserName, supplier.UserName);
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("in edit post Supplier- ", response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("in edit post Supplier2 -", ex.Message);
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
            ViewBag.SupplierClasseId = new SelectList(DropDownHelper.GetSupplierClasses(supplier.SupplierClasseId), "SupplierClasseId", "Description", supplier.SupplierClasseId);
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplier = db.Suppliers.Find(id);

            if (supplier == null)
            {
                return HttpNotFound();
            }
            supplier.City = db.Cities.FirstOrDefault(s => s.CityId == supplier.CityId);
            supplier.Departament = db.Departaments.FirstOrDefault(s => s.DepartamentId == supplier.DepartamentId);
            supplier.Province = db.Provinces.FirstOrDefault(s => s.ProvinceId == supplier.ProvinceId);
            ViewBag.SupplierClasseId = db.SupplierClasses.FirstOrDefault(s => s.SupplierClasseId == supplier.SupplierClasseId);
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var supplier = db.Suppliers.Find(id);
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var companySupplier = db.CompanySuppliers
               .SingleOrDefault(
                    s => s.CompanyId == user.CompanyId && s.SupplierId == supplier.SupplierId);

            using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    db.CompanySuppliers.Remove(companySupplier);
                    db.Suppliers.Remove(supplier);

                    var response = DbHelper.SaveChanges(db);
                    if (response.Succeeded)
                    {
                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, response.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.CityId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.ProvinceId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.SupplierClasseId = new SelectList(DropDownHelper.GetSupplierClasses(supplier.SupplierClasseId), "SupplierClasseId", "Description", supplier.SupplierClasseId);
            return View(supplier);
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
