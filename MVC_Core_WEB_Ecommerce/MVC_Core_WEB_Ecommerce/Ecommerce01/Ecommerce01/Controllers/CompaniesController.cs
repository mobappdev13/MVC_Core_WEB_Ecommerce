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
    [Authorize(Roles = "Admin")]
    public class CompaniesController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 3;
        // GET: Companies
        public ActionResult Index(int ? page = null)
        {
            page = (page ?? 1);
            var companies = db.Companies.Include(c => c.City)
                .Include(c => c.Province)
                .Include(c => c.Departament)
                .OrderBy(c => c.Name);
            return View(companies.ToPagedList((int)page, itemsonPage));
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
           
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name");
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name");
            return View();
        }

        // POST: Companies/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId,Name,ProvinceId,DepartamentId,CityId,AddressO,AddressL,Locality,Logo,LogoFile,PartitaIva,CodiceFiscale,Phone,PhoneMobil,Fax,Email,http")] Company company, HttpPostedFileBase LogoFile)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                try
                {
                    db.SaveChanges();
                    var folder = "~/Content/Logos";
                    //can be extention png,jpeg,gif,jpg
                    var file = string.Format("{0}.jpg", company.CompanyId);
                    if (company.LogoFile != null)
                    {
                        //var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                        var response = FilesHelper.UploadPhoto(LogoFile, folder, file, company.Logo);
                        //var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file, );
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            company.Logo = pic;
                            db.Entry(company).State = EntityState.Modified;
                            db.SaveChanges();

                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", company.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", company.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", company.CityId);
            return View(company);
        }



        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", company.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", company.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", company.CityId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyId,Name,ProvinceId,DepartamentId,CityId,AddressO,AddressL,Locality,Logo,LogoFile,PartitaIva,CodiceFiscale,Phone,PhoneMobil,Fax,Email,http")] Company company, HttpPostedFileBase LogoFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (company.LogoFile != null)
                    {
                        var folder = "~/Content/Logos";
                        var file = string.Format("{0}.jpg", company.CompanyId);
                        var response = FilesHelper.UploadPhoto(LogoFile, folder, file, company.Logo);
                        //var response = FilesHelper.UploadPhoto(company.LogoFile, folder, file);
                        if (response)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            company.Logo = pic;
                        }
                    }
                    db.Entry(company).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", company.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", company.ProvinceId);
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", company.CityId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
