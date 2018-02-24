using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DataAccess;
using ProjectManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectManager.Controllers
{
    public class EmployeeController : Controller
    {
        private AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            
            var models = _context.Employees
                .Include(x => x.Progetto)
                .ToList();
           
            return View(models);

        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee model;
            //string genereW;

            if (id == 0)
            {
                model = new Employee();
            }
            else
            {
                model = _context.Employees.Single(x => x.Id == id);
            }

            //verifiche e riempimenti
            FillForeignKeyLists();

            return View(model);
        }


        private void FillForeignKeyLists()
        {
            var progetti = _context.Projects.ToList();
            ViewData["projects"] = progetti;

        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {

            //verifiche
            var sameNameCount = _context.Employees
                .Where(x => x.Nome == model.Nome && x.Id != model.Id)
                .Count();

            
            if (sameNameCount > 0)
                ModelState.AddModelError(nameof(Employee.Nome), "Nome già presente!");

            //fare verifiche qui....

            if (!ModelState.IsValid)
            {
                //prima di load the view
                FillForeignKeyLists();
                return View(model);
            }

            _context.Employees.Update(model);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = _context.Employees.Single(x => x.Id == id);

            _context.Employees.Remove(model);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

