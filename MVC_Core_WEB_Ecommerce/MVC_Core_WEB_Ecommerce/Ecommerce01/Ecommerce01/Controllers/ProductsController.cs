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
    public class ProductsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 4;
        // GET: Products
        public ActionResult Index(int ? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Tax)
                .Where(p => p.CompanyId == user.CompanyId)
                .OrderBy( p => p.Description);
           
            return View(products.ToPagedList((int)page, itemsonPage));
            //return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
               .FirstOrDefault();

            ViewBag.CategoryId = new SelectList(DropDownHelper.GetCategories(user.CompanyId), "CategoryId", "Description");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.TaxId = new SelectList(DropDownHelper.GetTaxes(user.CompanyId), "TaxId", "Description");
            var product = new Product { CompanyId = user.CompanyId };
            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CompanyId,Description,BarCode,CategoryId,TaxId,Price,Image,ImageFile,Remarks")] Product product, HttpPostedFileBase ImageFile)
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
              .FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                if (product.ImageFile != null)
                {
                    //var file = string.Format("{0}.jpg", product.CompanyId);
                    var file = string.Format("{0}.jpg", product.ProductId);
                    var folder = "~/Content/Products";
                    var response = FilesHelper.UploadPhoto(ImageFile, folder, file, product.Image);
                    //var response = FilesHelper.UploadPhoto(product.ImageFile, folder, file);
                    if (response)
                    {
                        product.Image = string.Format("{0}/{1}", folder, file);
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(DropDownHelper.GetCategories(user.CompanyId), "CategoryId", "Description", product.CategoryId);
            //.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(DropDownHelper.GetTaxes(user.CompanyId), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(DropDownHelper.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
            //.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(DropDownHelper.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CompanyId,Description,BarCode,CategoryId,TaxId,Price,Image,ImageFile,Remarks")] Product product, HttpPostedFileBase ImageFile)
        {
            //
             var user = db.Users.Where(u => u.UserName == User.Identity.Name)
             .FirstOrDefault();

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //if (product.ImageFile != null)
                        if (ImageFile != null)
                        {
                        var folder = "~/Content/Products";
                        var file = string.Format("{0}.jpg", product.ProductId);
                        //var file = $"{product.ProductId}.jpg";
                         //System.IO.File.Delete(Path.Combine(HttpContext.Current.Server.MapPath(folder), name));
                        var response = FilesHelper.UploadPhoto(ImageFile, folder, file, product.Image);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            product.Image = pic;
                        }
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.CategoryId = new SelectList(DropDownHelper.GetCategories(product.CompanyId), "CategoryId", "Description", product.CategoryId);
            //.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", product.CompanyId);
            ViewBag.TaxId = new SelectList(DropDownHelper.GetTaxes(product.CompanyId), "TaxId", "Description", product.TaxId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
