using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSD_Tema1.Models
{
    public class PermissionModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string permissionName { get; set; }
        public string roleName { get; set; }
        public string rights { get; set; }
    }
}