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
    public class ProductAttributeStocksController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private List<ProductAttributeStock> ProductAttributeStock_List = new List<ProductAttributeStock>();
        private List<ProductAttribute> ProductAttribute_List = new List<ProductAttribute>();

        // GET: ProductAttributeStocks
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
              .FirstOrDefault();

            var productAttributeStocks = db.ProductAttributeStocks
                .Include(p => p.Company)
                .Include(p => p.Product)
                .Include(p => p.ProductAttribute)
                 .Where(p => p.CompanyId == user.CompanyId)
                 .Where(p => p.ProductId == p.Product.ProductId)
                .OrderBy(p => p.ProductAttributeValue);

            ;
            return View(productAttributeStocks.ToList());
        }

        // GET: ProductAttributeStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeStock productAttributeStock = db.ProductAttributeStocks.Find(id);
            if (productAttributeStock == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeStock);
        }

        // GET: ProductAttributeStocks/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
             .FirstOrDefault();

            var companies = db.Companies
            .Where(p => p.CompanyId == user.CompanyId).ToList();

            var productAttributeStocks = db.ProductAttributeStocks
                .Include(p => p.Company)
                .Include(p => p.Product)
                .Include(p => p.ProductAttribute)
                 .Where(p => p.CompanyId == user.CompanyId)
                 .Where(p => p.ProductId == p.Product.ProductId)
                .OrderBy(p => p.ProductAttributeValue).ToList();

            var AttributeOpts = db.AttributeOpts
               .Include(p => p.Company)
                .Where(p => p.CompanyId == user.CompanyId)
               .OrderBy(p => p.Description).ToList();

            //ProductAttributeStock_List = db.ProductAttributeStocks
            //    .Where(pas => pas.Product.CompanyId == user.CompanyId)
            //    .Include(pas => pas.ProductAttribute)
            //    .ToList();

            //ProductAttribute_List = db.ProductAttributes
            //.Where(pa => pa.Product.CompanyId == user.CompanyId)
            //    .Include(pa => pa.AttributeOpt)
            //    .ToList();

            //ProductAttributeStock productAttributeStock = new ProductAttributeStock { CompanyId = user.CompanyId };

            //productAttributeStock.ProductAttribute = ProductAttribute_List;



            ViewBag.CompanyId = new SelectList(companies, "CompanyId", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description");
            //ViewBag.ProductAttributeId = new SelectList(productAttributeStocks, "ProductAttributeId", "ProductAttributeValue");
            ViewBag.ProductAttributeId = new SelectList(AttributeOpts, "AttributeOptId", "Description");
            //
            return View();
        }

        // POST: ProductAttributeStocks/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductAttributeStockId,CompanyId,ProductId,ProductAttributeId,ProductAttributeValue,OverriddenPrice,StockQuantity,ReorderPoint,SkuEAN")] ProductAttributeStock productAttributeStock)
        {
            if (ModelState.IsValid)
            {
                db.ProductAttributeStocks.Add(productAttributeStock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productAttributeStock.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productAttributeStock.ProductId);
            ViewBag.ProductAttributeId = new SelectList(db.ProductAttributes, "ProductAttributeId", "ProductAttributeId", productAttributeStock.ProductAttributeId);
            return View(productAttributeStock);
        }

        // GET: ProductAttributeStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeStock productAttributeStock = db.ProductAttributeStocks.Find(id);
            if (productAttributeStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productAttributeStock.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productAttributeStock.ProductId);
            ViewBag.ProductAttributeId = new SelectList(db.ProductAttributes, "ProductAttributeId", "ProductAttributeId", productAttributeStock.ProductAttributeId);
            return View(productAttributeStock);
        }

        // POST: ProductAttributeStocks/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductAttributeStockId,CompanyId,ProductId,ProductAttributeId,ProductAttributeValue,OverriddenPrice,StockQuantity,ReorderPoint,SkuEAN")] ProductAttributeStock productAttributeStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productAttributeStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", productAttributeStock.CompanyId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Description", productAttributeStock.ProductId);
            ViewBag.ProductAttributeId = new SelectList(db.ProductAttributes, "ProductAttributeId", "ProductAttributeId", productAttributeStock.ProductAttributeId);
            return View(productAttributeStock);
        }

        // GET: ProductAttributeStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAttributeStock productAttributeStock = db.ProductAttributeStocks.Find(id);
            if (productAttributeStock == null)
            {
                return HttpNotFound();
            }
            return View(productAttributeStock);
        }

        // POST: ProductAttributeStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductAttributeStock productAttributeStock = db.ProductAttributeStocks.Find(id);
            db.ProductAttributeStocks.Remove(productAttributeStock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddValueAttributeStocks()
        {
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

           
            return PartialView();
            
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
