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
    public class ProductsController : ApiController
    {
        private Ecommerce19Entities entities = new Ecommerce19Entities();

        //add methods for Web API 2.0 with IHttpActionResult
        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return entities.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = entities.Products.Find(id);
            if (product == null)
            {
                //add
                return Content(HttpStatusCode.NotFound, " Il prodotto con questo Id = " + id.ToString() + " non è stato trovato!");
                //return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            entities.Entry(product).State = EntityState.Modified;

            try
            {
                entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                {
                    //add
                    return Content(HttpStatusCode.NotFound, " Il prodotto con questo Id = " + id.ToString() + " non è stato trovato!");
                    //return NotFound();
                }
                else
                {
                    //Log the error (add a variable name after Exception)
                    ModelState.AddModelError(ex.ToString(), "Impossibile salvare in Products. Riprovare in seguito. Se il problema persiste, contattare l'amministratore di sistema.");
                   
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            entities.Products.Add(product);
            entities.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = entities.Products.Find(id);
            if (product == null)
            {
                //add
                return Content(HttpStatusCode.NotFound, " Il prodotto con questo Id = " + id.ToString() + " non è stato trovato!");
                //return NotFound();
            }

            entities.Products.Remove(product);
            entities.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                entities.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return entities.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}