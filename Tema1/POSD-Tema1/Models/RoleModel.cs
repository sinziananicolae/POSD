using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSD_Tema1.Models
{
    public class RoleModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string roleName { get; set; }
        public string userInRole { get; set; }
        public bool read { get; set; }
        public bool write { get; set; }
    }
}