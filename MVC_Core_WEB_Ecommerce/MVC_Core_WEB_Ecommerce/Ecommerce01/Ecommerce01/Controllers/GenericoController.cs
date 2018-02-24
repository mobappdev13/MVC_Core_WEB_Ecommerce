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
        private readonly Ecommerce01Context _db = new Ecommerce01Context();
        //////add
        ////[HttpGet]
        public JsonResult GetProvinces(int departamentId)
        {
            //can be
            //https://stackoverflow.com/questions/4596371/what-are-the-downsides-to-turning-off-proxycreationenabled-for-ctp5-of-ef-code-f
            //http://www.c-sharpcorner.com/UploadFile/db2972/json-result-in-controller-sample-in-mvc-day-13/
            _db.Configuration.ProxyCreationEnabled = false;
            var provinces = _db.Provinces.Where(p => p.DepartamentId == departamentId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(provinces);
        }

        public JsonResult GetCities(int provinceId)
        {
            //can be
            //https://stackoverflow.com/questions/4596371/what-are-the-downsides-to-turning-off-proxycreationenabled-for-ctp5-of-ef-code-f
            //http://www.c-sharpcorner.com/UploadFile/db2972/json-result-in-controller-sample-in-mvc-day-13/
            _db.Configuration.ProxyCreationEnabled = false;
            var cities = _db.Cities.Where(c => c.ProvinceId == provinceId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(cities);
        }


        public JsonResult GetCompanies(int cityId)
        {
            //can be
            //https://stackoverflow.com/questions/4596371/what-are-the-downsides-to-turning-off-proxycreationenabled-for-ctp5-of-ef-code-f
            //http://www.c-sharpcorner.com/UploadFile/db2972/json-result-in-controller-sample-in-mvc-day-13/
            _db.Configuration.ProxyCreationEnabled = false;
            var companies = _db.Companies.Where(c => c.CityId == cityId);
            // can be  return Json(modelList,JsonRequestBehavior.AllowGet);
            return Json(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

