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

        // GET: Customers
        public ActionResult Index()
        {
            var user = db.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

            var customerscompany = (from cu in db.Customers
                                    join cc in db.CompanyCustomers on cu.CustomerId equals cc.CustomerId
                                    join co in db.Companies on cc.CompanyId equals co.CompanyId
                                    where co.CompanyId == user.CompanyId
                                    select new { cu }).ToList();

            var customers = new List<Customer>();

            foreach (var item in customerscompany)
            {
                customers.Add(item.cu);
            }

           
            return View(customers.OrderBy(c=> c.UserName)
                .ThenBy( c=> c.DepartamentId)
                .ThenBy(c => c.ProvinceId)
                .ThenBy(c => c.CityId).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
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
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(0), "ProvinceId", "Name");
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(0), "CityId", "Name");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId")] Customer customer)
        {
          if (ModelState.IsValid)
           {
            using (var tran = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                   db.Customers.Add(customer);
                    var response = DbHelper.SaveChanges(db);
                    if (response.Succeeded)
                    {
                        UsersHelper.CreateUserAsp(customer.UserName, "Customer");

                        var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

                        if (user == null)
                        {
                            return RedirectToAction("Index", "Home");
                        }
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Message);
                        tran.Rollback();
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
            return View(customer);
        }

        // POST: Customers/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,UserName,FirstName,LastName,DateBirth,Phone,Address,DepartamentId,ProvinceId,CityId")] Customer customer)
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
               .SingleOrDefault(
                    c => c.CompanyId == user.CompanyId && c.CustomerId == customer.CustomerId);

            using (var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    db.CompanyCustomers.Remove(companyCustomer);
                    db.Customers.Remove(customer);

                    var response = DbHelper.SaveChanges(db);
                    if (response.Succeeded)
                    {
                        transaction.Commit();
                        return RedirectToAction("Index");
                    }
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
