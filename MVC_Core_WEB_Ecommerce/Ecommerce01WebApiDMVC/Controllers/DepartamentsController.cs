using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Ecommerce01WebApiDMVC.Models;


namespace Ecommerce01WebApiMVC.Controllers
{
    public class DepartamentsController : ApiController
    {
        private Ecommerce01ApiContext db = new Ecommerce01ApiContext();

        // GET: api/Departaments
        public IQueryable<Departament> GetDepartaments()
        {
            return db.Departaments;
        }

        // GET: api/Departaments/5
        [ResponseType(typeof(Departament))]
        public IHttpActionResult GetDepartament(int id)
        {
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return NotFound();
            }

            return Ok(departament);
        }

        // PUT: api/Departaments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartament(int id, Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departament.DepartamentId)
            {
                return BadRequest();
            }

            db.Entry(departament).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Departaments
        [ResponseType(typeof(Departament))]
        public IHttpActionResult PostDepartament(Departament departament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departaments.Add(departament);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = departament.DepartamentId }, departament);
        }

        // DELETE: api/Departaments/5
        [ResponseType(typeof(Departament))]
        public IHttpActionResult DeleteDepartament(int id)
        {
            Departament departament = db.Departaments.Find(id);
            if (departament == null)
            {
                return NotFound();
            }

            db.Departaments.Remove(departament);
            db.SaveChanges();

            return Ok(departament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartamentExists(int id)
        {
            return db.Departaments.Count(e => e.DepartamentId == id) > 0;
        }
    }
}