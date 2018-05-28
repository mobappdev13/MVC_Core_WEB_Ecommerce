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
    [Authorize(Roles = "User")]
    public class ProductStockByAttribsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        // GET: ProductStockByAttribs
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
             .FirstOrDefault();
            var productStockByAttribs = db.ProductStockByAttribs
                .Include(p => p.AttributeOpt)
                .Include(p => p.Company)
                .Include(p => p.Product);
            return View(productStockByAttribs
                .OrderBy(p => p.ProductId)
                .ThenBy(p => p.AttributeOptId)
                .ToList());
        }

        // GET: ProductStockByAttribs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productStockByAttrib = db.ProductStockByAttribs.Find(id);

            if (productStockByAttrib == null)
            {
                return HttpNotFound();
            }
            return View(productStockByAttrib);
        }

        // GET: ProductStockByAttribs/Create
        public ActionResult Create()
        {
            //ProductStockByAttrib productStockByAttrib = new ProductStockByAttrib();

            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts, "AttributeOptId", "Description");
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
            //productStockByAttrib.MultiSelecColorList = new SelectList(db.Colors, "ColorId", "Description");
            //ViewBag.ProductAttributeValue = new SelectList(db.AttributeOptValues, "ValueAttribute", "ValueAttribute");
            return View();
        }

        // POST: ProductStockByAttribs/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductStockByAttribId,CompanyId,ProductId,AttributeOptId,ProductAttributeValue,OverriddenPrice,StockQuantity,ReorderPoint,SkuEAN,IsDisponible")] ProductStockByAttrib productStockByAttrib)
        {
            var tempo1 = productStockByAttrib.ProductAttributeValue;
            if (ModelState.IsValid)
            {
                db.ProductStockByAttribs.Add(productStockByAttrib);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts, "AttributeOptId", "Description", productStockByAttrib.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productStockByAttrib.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productStockByAttrib.ProductId);
            //ViewBag.AttributeOptValueId = new SelectList(db.AttributeOptValues, "AttributeOptValueId", "ValueAttribute", productStockByAttrib.ProductStockByAttribId);
            ViewBag.ProductAttributeValue = new SelectList(db.AttributeOptValues, "ValueAttribute", "ValueAttribute", productStockByAttrib.ProductAttributeValue);
            //productStockByAttrib.MultiSelecColorList = new SelectList(db.Colors, "ColorId", "Description", productStockByAttrib.MultiSelecColorList);
            return View(productStockByAttrib);
        }

        // GET: ProductStockByAttribs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStockByAttrib productStockByAttrib = db.ProductStockByAttribs.Find(id);
            if (productStockByAttrib == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts, "AttributeOptId", "Description", productStockByAttrib.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productStockByAttrib.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productStockByAttrib.ProductId);
            return View(productStockByAttrib);
        }

        // POST: ProductStockByAttribs/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductStockByAttribId,CompanyId,ProductId,AttributeOptId,ProductAttributeValue,OverriddenPrice,StockQuantity,ReorderPoint,SkuEAN,IsDisponible")] ProductStockByAttrib productStockByAttrib)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productStockByAttrib).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeOptId = new SelectList(db.AttributeOpts, "AttributeOptId", "Description", productStockByAttrib.AttributeOptId);
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productStockByAttrib.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productStockByAttrib.ProductId);
            return View(productStockByAttrib);
        }

        // GET: ProductStockByAttribs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductStockByAttrib productStockByAttrib = db.ProductStockByAttribs.Find(id);
            if (productStockByAttrib == null)
            {
                return HttpNotFound();
            }
            return View(productStockByAttrib);
        }

        // POST: ProductStockByAttribs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductStockByAttrib productStockByAttrib = db.ProductStockByAttribs.Find(id);
            db.ProductStockByAttribs.Remove(productStockByAttrib);
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
