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

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "User")]
    public class TaxesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private User user;
        private const int itemsonPage = 4;
        // GET: taxes
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var taxes = db.Taxes.Where(t => t.CompanyId == user.CompanyId)
                .OrderBy(t => t.Description);
            return View(taxes.ToPagedList((int)page, itemsonPage));
        }

        // GET: Taxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // GET: Taxes/Create
        public ActionResult Create()
        {
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var tax = new Tax()
            {
                CompanyId = user.CompanyId
            };
            return View(tax);
            
        }

        // POST: Taxes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxId,Description,Rate,CompanyId")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                //?dupplicati
                if (db.Taxes.Any(d => d.Description.Equals(tax.Description) && d.CompanyId.Equals(tax.CompanyId)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    db.Taxes.Add(tax);
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", tax.CompanyId);
            return View(tax);
        }           


        // GET: Taxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }

            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", tax.CompanyId);
            return View(tax);
        }

        // POST: Taxes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxId,Description,Rate,CompanyId")] Tax tax)
        {
            if (ModelState.IsValid)
            {
                //?dupplicati
                if (db.Taxes.Any(d => d.Description.Equals(tax.Description) && d.CompanyId.Equals(tax.CompanyId)))
                {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    db.Entry(tax).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(tax);
                    }
                }
            }
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", tax.CompanyId);
            return View(tax);
        }

        // GET: Taxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tax = db.Taxes.Find(id);
            if (tax == null)
            {
                return HttpNotFound();
            }
            return View(tax);
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tax = db.Taxes.Find(id);
            db.Taxes.Remove(tax);
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
