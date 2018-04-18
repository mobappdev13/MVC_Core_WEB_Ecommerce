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
    [Authorize(Roles = "User")]
    public class ProductsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        private List<Product> Product_List = new List<Product>();
        private List<Inventory> Inventory_List = new List<Inventory>();
        //last remove
        // private List<ProductAttribute> ProductAttribute_List = new List<ProductAttribute>();

        //private List<AttributeOpt> AttributeOpt_List = new List<AttributeOpt>();

        //last add
        //private List<ProductAttributeStock> ProductAttributeStocks_List = new List<ProductAttributeStock>();
        private List<ProductStockByAttrib> ProductStockByAttribs_List = new List<ProductStockByAttrib>();

        private const int itemsonPage = 3;
        // GET: Products
        //public ActionResult Index()
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();
           
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Tax)
                .Include(p => p.Inventories)
                .Where(p => p.CompanyId == user.CompanyId)
                .OrderBy(p => p.Description);

            //product.Category = db.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            //product.Tax = db.Taxes.FirstOrDefault(t => t.TaxId == product.TaxId);
           
            return View(products.ToPagedList((int)page, itemsonPage));
           // return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            //ViewBag.products = db.Products.GetAll();
            //List<Product> Product_List = new List<Product>();
            //List<Inventory> Inventory_List = new List<Inventory>();
            ////
            //Product_List = db.Products
            //    .Include(p => p.Category)
            //    .Include(p => p.Tax)
            //    .Where(p => p.CompanyId == user.CompanyId)
            //    .OrderBy(p => p.Description).ToList();

            //Inventory_List = db.Inventories
            //    .Include(i => i.Warehouse)
            //    .Include(i => i.Product)
            //    .Where(i => i.Product.CompanyId == user.CompanyId)
            //    .OrderBy(i => i.ProductId).ToList();

            //ProductInventoryViewModel ProductInventory = new ProductInventoryViewModel();

            //ProductInventory.Altro = "Questo Text";

            //ProductInventory.ProductList = Product_List;
            //ProductInventory.InventoryList = Inventory_List;

            ////return View(ProductInventory.ToPagedList((int)page, itemsonPage));
            //return View(ProductInventory);
            ////
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var product = db.Products.Find(id);

            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();
           

            if (product == null)
            {
                return HttpNotFound();
            }

            Inventory_List = db.Inventories
                .Include(i => i.Warehouse)
                .Include(i => i.Product)
                .Where(i => i.Product.CompanyId == user.CompanyId)
                .Where(i => i.ProductId == product.ProductId)
                .OrderBy(i => i.ProductId).ToList();

            ////last remove
            //ProductAttribute_List = db.ProductAttributes
            //   .Include(pa => pa.Product)
            //   .Include(pa => pa.AttributeOpt)
            //   .Where(pa => pa.Product.CompanyId == user.CompanyId)
            //   .Where(pa => pa.ProductId == product.ProductId)
            //   .OrderBy(pa => pa.ProductId).ToList();

            //last Add
            ProductStockByAttribs_List = db.ProductStockByAttribs
               .Include(pa => pa.Product)
               .Where(pa => pa.Product.CompanyId == user.CompanyId)
               .Where(pa => pa.ProductId == product.ProductId)
               .OrderBy(pa => pa.ProductId)
               .ThenBy(pa => pa.AttributeOptId)
               .ToList();



            //product.Inventories = db.Inventories.FirstOrDefault(c => c.ProductId == product.ProductId);
            product.Category = db.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            product.Tax = db.Taxes.FirstOrDefault(t => t.TaxId == product.TaxId);
            product.Inventories = Inventory_List;
            //last add
            //product.ProductAttributeStocks = ProductAttributeStocks_List;

            //last remove
            product.ProductStockByAttribs = ProductStockByAttribs_List;
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            //var ProdAttributes1 = (from a in db.AttributeOpts
            //                      group a.Description by a.CompanyId into pa
            //                      select new { CompanyId = pa.Key, Description = pa.ToList() });

            //var ProdAttributes2 = db.AttributeOpts.GroupBy(
            //                    a => a.CompanyId,
            //                    a => a.Description,
            //                (key, g) => new { CompanyId = key, Description = g.ToList() });

            //var ProdAttributes3 = (db.AttributeOpts.GroupBy(
            //                   a => a.Description,
            //                (key, g) => new { CompanyId = key, Description = g.ToList() }).ToList());


            //var ProdAttributes4 = db.AttributeOpts.Select(a => new
            //{
            //    //AttributeOptId = Convert.ToString(a.AttributeOptId),
            //   // CompanyId = Convert.ToString(a.CompanyId),
            //    Description = a.Description,
            //   // ValueAttribute = a.ValueAttribute,
               
            //}).ToList();


            //SELECT Description
            //FROM dbo.AttributeOpts
            //GROUP BY Description, CompanyId
            //to attributes list

            ////
            //var results = from p in persons
            //              group p.car by p.PersonId into g
            //              select new { PersonId = g.Key, Cars = g.ToList() };

            //
            //        var results = persons.GroupBy(
            //p => p.PersonId,
            //p => p.car,
            //(key, g) => new { PersonId = key, Cars = g.ToList() });
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
               .FirstOrDefault();

            ViewBag.CategoryId = new SelectList(DropDownHelper.GetCategories(user.CompanyId), "CategoryId", "Description");
            //ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.TaxId = new SelectList(DropDownHelper.GetTaxes(user.CompanyId), "TaxId", "Description");
           // ViewBag.ProductAttributes[0] = ProductAttribute_List[0];
            var product = new Product { CompanyId = user.CompanyId };

            //
            //var ProductAttribute_List = db.AttributeOpts.Select(a => new
            //{
            //    AttributeOptId = a.AttributeOptId, //        Convert.ToString(a.AttributeOptId),
            //    CompanyId = a.CompanyId,
            //    Description = a.Description,
            //    ValueAttribute = a.ValueAttribute

            //}).ToList();


            //foreach (var item in ProductAttribute_List)
            //{
            //    attributes.Add(item.Description);
            //}

            //last Add
            ProductStockByAttribs_List = db.ProductStockByAttribs
              .Include(pa => pa.Product)
              .Where(pa => pa.Product.CompanyId == user.CompanyId)
              .Where(pa => pa.ProductId == product.ProductId)
              .OrderBy(pa => pa.ProductId)
              .ThenBy(pa => pa.AttributeOptId)
              .ToList();

            //last remove
            //ProductAttribute_List = db.ProductAttributes
            //    .Where(pa => pa.Product.CompanyId == user.CompanyId)
            //    .Include(pa => pa.AttributeOpt)
            //    .ToList();

            product.ProductStockByAttribs = ProductStockByAttribs_List;
            //last remove
            //product.ProductAttributes = ProductAttribute_List;
            //ViewBag.ProdAttrib = new SelectList(DropDownHelper.GetAttributes(user.CompanyId), "Attibuto", "Description");
            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CompanyId,CategoryId,Description,BarCode,VendorPrice,VendorProductCode,Price,TaxId,Image,ImageFile,Remarks,ReorderPoint")] Product product, HttpPostedFileBase ImageFile)
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
        public ActionResult Edit([Bind(Include = "ProductId,CompanyId,CategoryId,Description,BarCode,VendorPrice,VendorProductCode,Price,TaxId,Image,Remarks,ReorderPoint")] Product product, HttpPostedFileBase ImageFile)
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
                        var response = FilesHelper.UploadPhoto(product.ImageFile, folder, file, product.Image);
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

            product.Category = db.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
            product.Tax = db.Taxes.FirstOrDefault(t => t.TaxId == product.TaxId);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult SetAttribute(int? page = null)
        {
            page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name)
                .FirstOrDefault();

            var products = db.Products
               .Include(p => p.Category)
               .Include(p => p.Tax)
               .Include(p => p.Inventories)
               .Where(p => p.CompanyId == user.CompanyId)
               .OrderBy(p => p.Description);

            //return View(products.ToPagedList((int)page, itemsonPage));
            return View(products.ToPagedList((int)page, itemsonPage));
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
