using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ecommerce01.Models;

namespace Ecommerce01.Controllers
{
    public class GenericoController : Controller
    {
        private readonly Ecommerce01Context db = new Ecommerce01Context();
        //////add
        ////[HttpGet]
        public JsonResult GetProvinces(int departamentId)
        {
            //add
            db.Configuration.ProxyCreationEnabled = false;
            var provinces = db.Provinces.Where(p => p.DepartamentId == departamentId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(provinces);
        }

        public JsonResult GetCities(int provinceId)
        {
            //can be
            db.Configuration.ProxyCreationEnabled = false;
            var cities = db.Cities.Where(c => c.ProvinceId == provinceId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(cities);
        }


        public JsonResult GetCompanies(int cityId)
        {
            //can be
            db.Configuration.ProxyCreationEnabled = false;
            var companies = db.Companies.Where(c => c.CityId == cityId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(companies);
        }

        public JsonResult GetSupplierClasses(int supplierClasseId)
        {
            //can be
            db.Configuration.ProxyCreationEnabled = false;
            var supplierclasses = db.SupplierClasses.Where(s => s.SupplierClasseId == supplierClasseId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(supplierclasses);
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

