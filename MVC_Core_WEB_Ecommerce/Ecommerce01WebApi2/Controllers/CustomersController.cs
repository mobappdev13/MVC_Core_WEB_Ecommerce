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
using Ecommerce01DataAccess;

namespace Ecommerce01WebApi2.Controllers
{
    public class CustomersController : ApiController
    {
        private Ecommerce19Entities entities = new Ecommerce19Entities();

        // GET: api/Customers
        public IQueryable<Customer> GetCustomers()
        {
            return entities.Customers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = entities.Customers.Find(id);
            if (customer == null)
            {
                //add
                return Content(HttpStatusCode.NotFound, " Il Cliente con questo Id = " + id.ToString() + " non è stato trovato!");
                //return NotFound();
               
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            entities.Entry(customer).State = EntityState.Modified;

            try
            {
                entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CustomerExists(id))
                {
                    //add
                    return Content(HttpStatusCode.NotFound, " Il cliente con questo Id = " + id.ToString() + " non è stato trovato!");
                    //return NotFound();
                }
                else
                {

                    //Log the error (add a variable name after Exception)
                    ModelState.AddModelError(ex.ToString(), "Impossibile salvare in Customers. Riprovare in seguito. Se il problema persiste, contattare l'amministratore di sistema.");

                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            entities.Customers.Add(customer);
            entities.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = entities.Customers.Find(id);
            if (customer == null)
            {
                //add
                return Content(HttpStatusCode.NotFound, " Il cliente con questo Id = " + id.ToString() + " non è stato trovato!");
                //return NotFound();
            }

            entities.Customers.Remove(customer);
            entities.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return entities.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
    // public HttpResponseMessage Put(int id, [FromBody] Entity entity)
    //can be  HttpResponseMessage Put([FromBody]int id, [FromUri] Entity entity)
}