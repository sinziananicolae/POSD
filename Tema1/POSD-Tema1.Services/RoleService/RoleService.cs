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

        public void AssignRole(string roleName, string userName) {
            var role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);
            var user = _dbEntities.Users.FirstOrDefault(f => f.Username == userName);

            _dbEntities.UserInRoles.Add(new UserInRole {
                UserId = user.Id,
                RoleId = role.Id
            });

            _dbEntities.SaveChanges();
        }

        public void RevokeRole(string roleName, string userName)
        {
            var role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);
            var user = _dbEntities.Users.FirstOrDefault(f => f.Username == userName);

            var userInRole = _dbEntities.UserInRoles.FirstOrDefault(f => f.Role.Name == roleName && f.User.Username == userName);

            _dbEntities.UserInRoles.Remove(userInRole);

            _dbEntities.SaveChanges();
        }

        public bool ExistsUserInRole(string roleName, string userName) {
            var userInRole = _dbEntities.UserInRoles.FirstOrDefault(f => f.Role.Name == roleName && f.User.Username == userName);

            if (userInRole == null)
                return false;

            return true;
        }

        public bool ExistsPermissionInRole(string roleName, string permissionName) {
            var role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);
            var permission = _dbEntities.Permissions.FirstOrDefault(f => f.Name == permissionName);

            var permissionInRole = _dbEntities.PermissionToRoles.FirstOrDefault(f => f.PermissionId == permission.Id && f.RoleId == role.Id);

            if (permissionInRole == null)
                return false;
            return true;
        }

    }
}
