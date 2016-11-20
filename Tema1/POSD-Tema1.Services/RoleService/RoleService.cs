using POSD_Tema1.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Services.RoleService
{
    public class RoleService
    {
        private readonly Entities _dbEntities;

        public RoleService()
        {
            _dbEntities = new Entities();
        }

        public void CreateRole(string roleName)
        {
            var role = new Role();

            role.Name = roleName;

            _dbEntities.Roles.Add(role);
            _dbEntities.SaveChanges();
        }

        public bool ExistsRole(string roleName) {
            var role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);

            if (role == null)
                return false;

            return true;
        }

    }
}
