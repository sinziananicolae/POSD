using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Models
{
    public class ResourcePathModel
    {
        public string resourceName { get; set; }
        public string fullResourcePath { get; set; }
        public string resourcePath { get; set; }
        public int ownerId { get; set; }

        public ResourcePathModel(string resourceName) {
            fullResourcePath = resourceName;

            string[] pathParams = resourceName.Split('/');

            this.resourceName = pathParams.Last();

            resourcePath = string.Join("/", pathParams.Take(pathParams.Length - 1));
        }
    }
}
