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

namespace Ecommerce01.Controllers
{
    [Authorize(Roles = "User")]
    public class CustomersController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
        private const int itemsonPage = 2;
        // GET: Customers
        //public ActionResult Index(int? page = null)
        public ActionResult Index()
        {
            //page = (page ?? 1);
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var customerscompany = (from cu in db.Customers
                                    join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                                    join co in db.Companies on cc.CompanyId equals co.CompanyId
                                    where co.CompanyId == user.CompanyId
                                    select cu)
                                    .Include(c => c.Departament)
                                    .Include(c => c.Province)
                                    .Include(c => c.City)
                                    .OrderBy(c => c.UserName)
                                    .ToList();

            ////var customers = customerscompany.Select(item => item.cu).ToList();
            //customers = customers.OrderBy(c => c.UserName).ToList();

            return View(customerscompany);
            //.ToPagedList((int)page, itemsonPage));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers
                .Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.City = db.Cities.FirstOrDefault(c => c.CityId == customer.CityId);
            customer.Departament = db.Departaments.FirstOrDefault(c => c.DepartamentId == customer.DepartamentId);
            customer.Province = db.Provinces.FirstOrDefault(c => c.ProvinceId == customer.ProvinceId);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(0), "ProvinceId", "Name");
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(0), "CityId", "Name");
            ViewBag.Gender = new SelectList(DropDownHelper.GetGenders(), "Text", "Value");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId, Gender")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                using (var tran = db.Database.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        db.Customers.Add(customer);
                        var response = DbHelper.SaveChanges(db);
                        if (!response.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, response.Message);
                            tran.Rollback();
                        }
                        else
                        {
                            UsersHelper.CreateUserAsp(customer.UserName, "Customer");
                            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                            if (user == null)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            //new object
                            var companyCustomer = new CompanyCustomer()
                            {
                                CompanyId = user.CompanyId,
                                CustomerId = customer.CustomerId
                            };

                            db.CompanyCustomers.Add(companyCustomer);
                            db.SaveChanges();
                            tran.Commit();
                            return RedirectToAction("Index");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        tran.Rollback();
                    }
                }
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(customer.ProvinceId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(customer.DepartamentId), "ProvinceId", "Name", customer.ProvinceId);
            ViewBag.Gender = new SelectList(DropDownHelper.GetGenders(), "Text", "Value", customer.Gender);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(customer.ProvinceId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(customer.DepartamentId), "ProvinceId", "Name", customer.ProvinceId);
            ViewBag.Gender = new SelectList(DropDownHelper.GetGenders(), "Text", "Value", customer.Gender);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId,Gender")] Customer customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Modified;

                var response = DbHelper.SaveChanges(db);

                if (response.Succeeded)
                {
                    using (var db_other = new Ecommerce01Context())
                    {
                        var currentCustomer = db_other.Customers.Find(customer.CustomerId);

                        if (currentCustomer.UserName != customer.UserName)
                        {
                            UsersHelper.UpdateUserName(currentCustomer.UserName, customer.UserName);
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("in edit post customer- ", response.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("in edit post customer -", ex.Message);
            }

            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(customer.ProvinceId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(customer.DepartamentId), "ProvinceId", "Name", customer.ProvinceId);
            ViewBag.Gender = new SelectList(DropDownHelper.GetGenders(), "Text", "Value", customer.Gender);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = db.Customers.Find(id);
           
            if (customer == null)
            {
                return HttpNotFound();
            }
            customer.City = db.Cities.FirstOrDefault(c => c.CityId == customer.CityId);
            customer.Departament = db.Departaments.FirstOrDefault(c => c.DepartamentId == customer.DepartamentId);
            customer.Province = db.Provinces.FirstOrDefault(c => c.ProvinceId == customer.ProvinceId);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = db.Customers.Find(id);
            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var companyCustomer = db.CompanyCustomers
               .SingleOrDefault(c => c.CompanyId == user.CompanyId && c.CustomerId == customer.CustomerId);
             //where ?
            using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    //remove one
                    db.CompanyCustomers.Remove(companyCustomer);
                    //remove two
                    db.Customers.Remove(customer);

                    var response = DbHelper.SaveChanges(db);

                    if (response.Succeeded)
                    {
                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
                    //else
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, response.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(customer.ProvinceId), "CityId", "Name", customer.CityId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", customer.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(customer.DepartamentId), "ProvinceId", "Name", customer.ProvinceId);
            ViewBag.Gender = new SelectList(DropDownHelper.GetGenders(), "Text", "Value", customer.Gender);
            return View(customer);
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
