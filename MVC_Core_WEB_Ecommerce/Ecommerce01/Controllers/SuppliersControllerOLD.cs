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
    [Authorize(Roles = "User")]
    public class SuppliersControllerOLD : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        private const int itemsonPage = 2;
        // GET: Suppliers
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var supplierscompany = (from su in db.Suppliers
                                    join sc in db.CompanySuppliers on su.SupplierId equals sc.SupplierId
                                    join co in db.Companies on sc.CompanyId equals co.CompanyId
                                    where co.CompanyId == user.CompanyId
                                    select  su)
                                    .Include(c => c.Departament)
                                    .Include(c => c.Province)
                                    .Include(c => c.City)
                                    .OrderBy(c => c.UserName)
                                    .ThenBy(c => c.DepartamentId)
                                    .ThenBy(c => c.ProvinceId)
                                    .ThenBy(c => c.CityId)
                                    .ToList();

            

            return View(supplierscompany
               .ToPagedList((int)page, itemsonPage));
        }

        // GET: Suppliers/Details/5
        public ActionResult Details(int? id)
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

            return View(supplier);
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(0), "ProvinceId", "Name");
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(0), "CityId", "Name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierId,UserName,FirstName,LastName,DateCreated,IsfavoriteSupplier,Phone,Address,DepartamentId,ProvinceId,CityId")] Supplier supplier)
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
                            var companySupplier = new CompanySupplier()
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
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
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

            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
            return View(supplier);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierId,UserName,FirstName,LastName,DateCreated,IsfavoriteSupplier,Phone,Address,DepartamentId,ProvinceId,CityId")] Supplier supplier)
        {
            try
            {
                db.Entry(supplier).State = EntityState.Modified;

                var response = DbHelper.SaveChanges(db);
                if (response.Succeeded)
                {
                    using (var db_other = new Ecommerce01Context())
                    {
                        var currentSupplier = db_other.Customers.Find(supplier.SupplierId);

                        //is dupplicate ?
                        if (currentSupplier.UserName != supplier.UserName)
                        {
                            UsersHelper.UpdateUserName(currentSupplier.UserName, supplier.UserName);
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("in edit post supplier-1 ", response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("in edit post supplier-2 ", ex.Message);
            }

            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
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

            //table related
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
                    //qualcosa sbagliato
                    transaction.Rollback();
                    ModelState.AddModelError("in delete post supplier 1", response.Message);
                }
                catch (Exception ex2)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("in delete post supplier 2", ex2.Message);
                }
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", supplier.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(supplier.DepartamentId), "ProvinceId", "Name", supplier.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(supplier.ProvinceId), "CityId", "Name", supplier.CityId);
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
