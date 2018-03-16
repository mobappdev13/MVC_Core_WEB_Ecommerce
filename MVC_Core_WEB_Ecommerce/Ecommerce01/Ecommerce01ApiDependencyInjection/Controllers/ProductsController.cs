using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ecommerce01ApiDependencyInjection.Models;;

namespace Ecommerce01ApiDependencyInjection.Controllers
{
    public class ProductsController : ApiController
        {
            private IProductRepository _repository;

            public ProductsController(IProductRepository repository)
            {
                _repository = repository;
            }

            //[HttpGet]
            public IHttpActionResult Get(int id)
            {
                try
                {
                    var product = _repository.GetById(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    return Ok(product);
                }
                catch (Exception ex)
                {
                    return (IHttpActionResult)Request.CreateResponse(System.Net.HttpStatusCode.OK, ex.ToString());
                }

            }

            //[HttpGet]
            public IHttpActionResult Get()
            {
                var products = _repository.GetAll();
                if (products == null)
                {
                    return NotFound();
                }
                return Ok(products);
            }

            //[HttpPost]
            public IHttpActionResult Add(Product product)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _repository.Add(product);
                return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
            }
        }
    }

