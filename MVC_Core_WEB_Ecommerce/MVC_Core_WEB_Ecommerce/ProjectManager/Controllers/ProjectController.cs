using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManager.DataAccess;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var models = _context.Projects.ToList();
            return View(models);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Project model;

            if (id == 0)
            {
                model = new Project();
            }
            else
            {
                model = _context.Projects.Single(x => x.Id == id);
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(Project model)
        {
            var sameCodiceCount = _context.Projects
                .Where(x => x.Codice == model.Codice && x.Id != model.Id)
                .Count();

            if (sameCodiceCount > 0)
                ModelState.AddModelError(nameof(Project.Codice), "Codice già presente!");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Projects.Update(model);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //new fot thisproject
        [HttpGet]
        public IActionResult EmployeesOnProject(int id)
        {
            //var numOfEmployees = ListEmployeesOnProject.Count();
            var model = _context.Projects
                .Include(x => x.ListEmployeesOnProject)
                .Single(x => x.Id == id);          
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var model = _context.Projects.Single(x => x.Id == id);

            _context.Projects.Remove(model);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

