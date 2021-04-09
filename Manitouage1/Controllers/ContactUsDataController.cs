using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Manitouage1.Controllers
{
    public class ContactUsDataController : ApiController
    {
        // GET: api/ContactUsData
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ContactUsData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ContactUsData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ContactUsData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ContactUsData/5
        public void Delete(int id)
        {
        }
    }
}
