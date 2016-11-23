using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Models
{
    public class ResourceModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string resourceName { get; set; }
        public int resourceTypeId { get; set; }
        public string value { get; set; }
        public string rights { get; set; }
        public string roleName { get; set; }
    }
}
