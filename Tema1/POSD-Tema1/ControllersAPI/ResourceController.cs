using POSD_Tema1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSD_Tema1.ControllersAPI
{
    public class ResourceController : ApiController
    {
        private ResourceService _resourceService;

        public ResourceController() {
            _resourceService = new ResourceService();
        }

        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            return _resourceService.GetResourceTypes();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}