using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ecommerce01WebApi2.Controllers
{
   // [Authorize]
    public class ValuesController : ApiController
    {
        //add
        static List<string> strings = new List<string>()
        { "value0", "value1", "value2" };

        // GET api/values
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return strings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return "value";
            return strings[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            //add
            strings.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            //add
            strings[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            //add
            strings.RemoveAt(id);
        }
    }
}
