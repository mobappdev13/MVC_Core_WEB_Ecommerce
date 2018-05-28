using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Models;

namespace Ecommerce01.Controllers
{
    public class ColorSelsController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();

        // GET: ColorSels
        [HttpGet]
        public ActionResult Index()
        {
            var colorsel = new ColorSel();

            //Using Model [NotMapped] public SelectList ColorList { get; set; }
            colorsel.ColorList = new SelectList(db.ColorSels, "ColorId", "Description");

            //using ViewBag
            ViewBag.VBColorList = new SelectList(db.Colors, "ColorId", "Description");

            //Multi select using VIewBAG data sent for multiselect dropdown.
            //ViewBag.MultiselectCountry = GetCountries(null);

            //using view bag
            //new MultiSelectList(ViewBag.Customers, "Id", "Name", Model.Customers.Select(c => c.Id))
            //new MultiSelectList(ViewBag.Customers, "Id", "Name", Model.Customers)
             ViewBag.VBMultiColor = new MultiSelectList(db.ColorSels, "ColorId", "Description");
             ViewBag.VBMultiSizes = new MultiSelectList(db.SizeDims, "SizeDimId", "Description");

            //using MODEL  [NotMapped] public MultiSelectList MultiSelecColorList { get; set; }
            colorsel.MultiSelecColorList = new MultiSelectList(db.ColorSels, "ColorId", "Description");
            //foreach (var size in ViewBag.VBMultiSizes)
            //{
            //    ViewData["size"] = size;
            //}

            return View(colorsel);
            
        }

        [HttpPost]
        public string Index(IEnumerable<string> MultiSelecColorList)
        {
            if (MultiSelecColorList == null)
            {
                return " non hai scelto niente !";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("hai scelto - " + string.Join(",", MultiSelecColorList));
                return sb.ToString();

            }

        }






            private MultiSelectList GetCountries(string[] SelectedValues)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "India", Value = "1" });
            items.Add(new SelectListItem { Text = "China", Value = "2" });
            items.Add(new SelectListItem { Text = "United Sates", Value = "3" });
            items.Add(new SelectListItem { Text = "Srilanka", Value = "4" });
            items.Add(new SelectListItem { Text = "Germany", Value = "5" });
            items.Add(new SelectListItem { Text = "Japan", Value = "6" });
            items.Add(new SelectListItem { Text = "Nepal", Value = "7" });
            items.Add(new SelectListItem { Text = "Russia", Value = "8" });
            items.Add(new SelectListItem { Text = "Spain", Value = "9" });
            items.Add(new SelectListItem { Text = "Frans", Value = "10" });
            items.Add(new SelectListItem { Text = "Canada", Value = "11" });
            items.Add(new SelectListItem { Text = "brazil", Value = "12" });
            items.Add(new SelectListItem { Text = "Koria", Value = "13" });
            items.Add(new SelectListItem { Text = "England", Value = "14" });
            return new MultiSelectList(items, "Value", "Text", SelectedValues);

        }

        public static List<SelectListItem> GetDropDown()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            ////lm = (call database);
           
            //foreach (var temp in lm)
            //{
            //    ls.Add(new SelectListItem() { Text = temp.name, Value = temp.id });
            //}
            return ls;
        }

        public ActionResult ChoosenDropDown()
        {
            ColorSel objectcolorsel = new ColorSel();
            objectcolorsel.GetColorSelList = db.ColorSels.ToList();
                
                
                //db.ColorSels
                //.Select
                //(c => new ColorSel {
                //    ColorId = c.ColorId,
                //    Description = c.Description })
                //.ToList();
            return View(objectcolorsel);
        }

        [HttpPost]
        public ActionResult ChoosenDropDown(ColorSel colorsel)
        {

            return RedirectToAction("ChoosenDropDown");
        }

        // GET: ColorSels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorSel colorSel = db.ColorSels.Find(id);
            if (colorSel == null)
            {
                return HttpNotFound();
            }
            return View(colorSel);
        }

        // GET: ColorSels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorSels/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ColorId,Description")] ColorSel colorSel)
        {
            if (ModelState.IsValid)
            {
                db.ColorSels.Add(colorSel);
                db.SaveChanges();

                //
                
                //db.MultiColors.Add(item);
                //db.SaveChanges();


                return RedirectToAction("Index");
                
                //foreach (var item in collection)
                //{
                //    db..Add(colorSel);
                //}
                //// EF_Test test;
                //for (int i = 0; i < count; i++)
                //{
                //    test = new EF_Test();
                //    test.Name = "linfei";
                //    test.Date = DateTime.Now;
                //    db.EF_Test.Add(test);
                //}
                //db.SaveChanges();
            }

            return View(colorSel);
        }

        // GET: ColorSels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorSel colorSel = db.ColorSels.Find(id);
            if (colorSel == null)
            {
                return HttpNotFound();
            }
            return View(colorSel);
        }

        // POST: ColorSels/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColorId,Description")] ColorSel colorSel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorSel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colorSel);
        }

        // GET: ColorSels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorSel colorSel = db.ColorSels.Find(id);
            if (colorSel == null)
            {
                return HttpNotFound();
            }
            return View(colorSel);
        }

        // POST: ColorSels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ColorSel colorSel = db.ColorSels.Find(id);
            db.ColorSels.Remove(colorSel);
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
