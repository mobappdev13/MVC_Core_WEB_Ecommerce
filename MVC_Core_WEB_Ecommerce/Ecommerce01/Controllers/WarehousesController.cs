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
    public class WarehousesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        private const int itemsonPage = 4;
        // GET: Warehouses

        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var warehouses = db.Warehouses.Where(w => w.CompanyId == user.CompanyId)
                .Include(w => w.City)
                .Include(w => w.Departament)
                .Include(w => w.Province)
                .OrderBy(w => w.CompanyId)
                .ThenBy(w => w.Name);
            return View(warehouses.ToPagedList((int)page, itemsonPage));
           
        }

        // GET: Warehouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var warehouse = db.Warehouses.Find(id);

            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //fondamental
            var warehouse = new Warehouse()
            {
                CompanyId = user.CompanyId
            };

            //var inventory = new Inventory()
            //{
            //    WarehouseId = warehouse.WarehouseId
            //};
           
            //attention 0
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(0), "CityId", "Name");
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(0), "ProvinceId", "Name");

            return View(warehouse);
        }

        // POST: Warehouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarehouseId,CompanyId,Name,Phone,Address,DepartamentId,ProvinceId,CityId")] Warehouse warehouse)
        {
            try
            {
                using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            //save only warehouse
                            db.Warehouses.Add(warehouse);
                            var response = DbHelper.SaveChanges(db);
                            if (response.Succeeded)
                            {
                                //attention transaction

                                var inventory = new Inventory()
                                {
                                        ProductId = 1,
                                       // SupplierId = 1,
                                        Stock = 0
                                };

                                db.Inventories.Add(inventory);
                                var response1 = DbHelper.SaveChanges(db);
                                if (response1.Succeeded)
                                {
                                    transaction.Commit();
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, response1.Message);
                                    transaction.Rollback();
                                }
                            }
                            else
                            {
                                ModelState.AddModelError(string.Empty, response.Message);
                                transaction.Rollback();
                            }
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, ex.Message);
                            transaction.Rollback();
                        }
                    }
                    ViewBag.CityId = new SelectList(DropDownHelper.GetCities(warehouse.ProvinceId), "CityId", "Name", warehouse.CityId);
                    ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
                    ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", warehouse.DepartamentId);
                    ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(warehouse.DepartamentId), "ProvinceId", "Name", warehouse.ProvinceId);
                    return View(warehouse);
                }
            }
            catch (Exception ex2)
            {
                ModelState.AddModelError(string.Empty, ex2.Message);

            }
            //return RedirectToAction("Index");
            return View(warehouse);
        }
               






        //    ///
        //    using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
        //    {
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Warehouses.Add(warehouse);
        //                db.SaveChanges();
        //                ////
        //                //var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        //                //if (user == null)
        //                //{
        //                //    return RedirectToAction("Index", "Home");
        //                //}
        //                ////for Inventories ?

        //                //var inventory = new Inventory()
        //                //{
        //                //    WarehouseId = warehouse.WarehouseId,
        //                //    ProductId = 0,
        //                //    SupplierId = 0,
        //                //    Stock = 0
        //                //};

        //                //db.Inventories.Add(inventory);
        //                //db.SaveChanges();
        //                //return RedirectToAction("Index");

        //                ////all ok
        //                transaction.Commit();
        //                return RedirectToAction("Index");
        //            }
        //             return RedirectToAction("Index");
        //            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(warehouse.ProvinceId), "CityId", "Name", warehouse.CityId);
        //            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
        //            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", warehouse.DepartamentId);
        //            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(warehouse.DepartamentId), "ProvinceId", "Name", warehouse.ProvinceId);
        //            return View(warehouse);

        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ex.Message);
        //            // non ok
        //            transaction.Rollback();
        //        }
        //    }
        //    return RedirectToAction("Index");
        //}

        // GET: Warehouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var warehouse = db.Warehouses.Find(id);

            if (warehouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(warehouse.ProvinceId), "CityId", "Name", warehouse.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", warehouse.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(warehouse.DepartamentId), "ProvinceId", "Name", warehouse.ProvinceId);
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarehouseId,CompanyId,Name,Phone,Address,DepartamentId,ProvinceId,CityId")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(warehouse.ProvinceId), "CityId", "Name", warehouse.CityId);
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", warehouse.CompanyId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", warehouse.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(warehouse.DepartamentId), "ProvinceId", "Name", warehouse.ProvinceId);
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var warehouse = db.Warehouses.Find(id);

            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var warehouse = db.Warehouses.Find(id);
            db.Warehouses.Remove(warehouse);
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
