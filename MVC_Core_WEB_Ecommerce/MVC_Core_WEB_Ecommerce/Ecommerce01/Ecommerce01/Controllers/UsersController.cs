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
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private Ecommerce01Context db = new Ecommerce01Context();
              
        private const int itemsonPage = 3;
        // GET: Users
        public ActionResult Index(int? page = null)
        {
            page = (page ?? 1);
            var users = db.Users
                .Include(u => u.City)
                .Include(u => u.Company)
                .Include(u => u.Departament)
                .Include(u => u.Province)
                .OrderBy(u => u.UserName);
            return View(users.ToPagedList((int)page, itemsonPage));
           
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name");
            ViewBag.CompanyId = new SelectList(DropDownHelper.GetCompanies(), "CompanyId", "Name");
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name");
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,FirstName,LastName,DateBirth,Phone,Address,Photo,PhotoFile,DepartamentId,ProvinceId,CityId,CompanyId")] User user, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                //?dupplicati
               if (db.Users.Any(u => u.UserName.Equals(user.UserName) && u.CompanyId.Equals(user.CompanyId)))
               {
                    ModelState.AddModelError(string.Empty, "Esiste già un Registro con lo stesso valore");
                }
                else
                {
                    db.Users.Add(user);
                    try
                    {
                        db.SaveChanges();
                        //attention Role User
                        UsersHelper.CreateUserAsp(user.UserName, "User");

                        if (user.PhotoFile != null)
                        {
                            var folder = "~/Content/Users";
                            //can be extention png,jpeg,gif,jpg
                            var file = string.Format("{0}.jpg", user.UserId);
                            //var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                            var response = FilesHelper.UploadPhoto(PhotoFile, folder, file, user.Photo);
                            if (response)
                                if (response)
                            {
                                var pic = string.Format("{0}/{1}", folder, file);
                                user.Photo = pic;
                                db.Entry(user).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        //ModelState.AddModelError(string.Empty, ex.Message);
                        ModelState.AddModelError(string.Empty, ex.Message + " NON RIESCO A SALVARE - User Duplicato");
                    }
                }
            }

            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId","Name", user.CityId);
            ViewBag.CompanyId = new SelectList(DropDownHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", user.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", user.ProvinceId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(DropDownHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", user.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", user.ProvinceId);

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,FirstName,LastName,DateBirth,Phone,Address,Photo,PhotoFile,DepartamentId,ProvinceId,CityId,CompanyId")] User user, HttpPostedFileBase PhotoFile)
        {
            if (ModelState.IsValid)
            {
                //var folder = "~/Content/Users";
                //var file = $"{user.UserId}.jpg";
                if (user.PhotoFile != null)
                {
                    var file = string.Format("{0}.jpg", user.UserId);
                    var folder = "~/Content/Users";
                    //var response = FilesHelper.UploadPhoto(user.PhotoFile, folder, file);
                    var response = FilesHelper.UploadPhoto(PhotoFile, folder, file, user.Photo);
                    user.Photo = string.Format("{0}/{1}", folder, file);
                }
                //if user change e-amail
                using (var db2 = new Ecommerce01Context())
                {
                    var currentUser = db2.Users.Find(user.UserId);

                    if (currentUser.UserName != user.UserName)
                    {
                        UsersHelper.UpdateUserName(currentUser.UserName, user.UserName);
                    }
                }

                db.Entry(user).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message + " NON RIESCO A SALVARE, email duplicata presente nel sistema");
                    //return View(user);
                    //ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            ViewBag.CityId = new SelectList(DropDownHelper.GetCities(), "CityId", "Name", user.CityId);
            ViewBag.CompanyId = new SelectList(DropDownHelper.GetCompanies(), "CompanyId", "Name", user.CompanyId);
            ViewBag.DepartamentId = new SelectList(DropDownHelper.GetDepartaments(), "DepartamentId", "Name", user.DepartamentId);
            ViewBag.ProvinceId = new SelectList(DropDownHelper.GetProvinces(), "ProvinceId", "Name", user.ProvinceId);
            return View(user);
        }
                 


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            //last add
            UsersHelper.DeleteUser(user.UserName, "User");
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
