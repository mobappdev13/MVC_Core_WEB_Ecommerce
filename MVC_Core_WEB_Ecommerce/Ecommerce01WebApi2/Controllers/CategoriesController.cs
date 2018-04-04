using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ecommerce01DataAccess;

namespace Ecommerce01WebApi2.Controllers
{
    public class CategoriesController : ApiController
    {
        //add methods for Web API 1.0
        //[HttpGet]
        public HttpResponseMessage Get()
        {
            using (Ecommerce19Entities entities = new Ecommerce19Entities())
            {
                var categories = entities.Categories.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, categories);
            }
               
        }

        //add
        //[HttpGet]
        public HttpResponseMessage Get(int id)
        {
            //db context
            using (Ecommerce19Entities entities = new Ecommerce19Entities())
            {
                var entity = entities.Categories.FirstOrDefault(c => c.CategoryId == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, " la Categoria con questo Id = " + id.ToString() + " non è stata trovata!");
                    return message;
                }
                
                        
            }
        }

        //add
        //[HttpPost]
        public  HttpResponseMessage Post([FromBody]Category category)
        {
            try
            {
                using (Ecommerce19Entities entities = new Ecommerce19Entities())
                {
                    entities.Categories.Add(category);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, category);

                    message.Headers.Location = new Uri(Request.RequestUri +
                                                        category.CategoryId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;

            }

        }

        //add
       // [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (Ecommerce19Entities entities = new Ecommerce19Entities())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.CategoryId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound," Categoria con questo Id = " + id.ToString() + " non è stata trovata!");
                    }
                    else
                    {
                        entities.Categories.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK, " Eliminato " + entity.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;
            }
            
        }

       // [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Category category)
        {
            try
            {
                using (Ecommerce19Entities entities = new Ecommerce19Entities())
                {
                    var entity = entities.Categories.FirstOrDefault(e => e.CategoryId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "La categoria con Id = " + id.ToString() + " Non è stata trovata per aggiornarla!");
                    }
                    else
                    {
                        //from local to database
                        entity.Description = category.Description;
                        entity.CompanyId = category.CompanyId;

                        entities.SaveChanges();
                        return Request.CreateErrorResponse(HttpStatusCode.OK, entity.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
               var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
               return message;
            }
        }
        // public HttpResponseMessage Put(int id, [FromBody] Category category)
        //can be  HttpResponseMessage Put([FromBody]int id, [FromUri] Category category)
    }
}
