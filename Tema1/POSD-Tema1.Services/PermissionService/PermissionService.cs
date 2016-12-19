using POSD_Tema1.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Services.PermissionService
{
    public class PermissionService
    {
        private readonly Entities _dbEntities;

        public PermissionService()
        {
            _dbEntities = new Entities();
        }

        public bool ExistsPermission(string permissionName)
        {
            var permission = _dbEntities.Permissions.FirstOrDefault(f => f.Name == permissionName);

            if (permission == null)
                return false;

            return true;
        }

        public void CreatePermission(string permissionName, string rights)
        {
            var permission = new Permission();

            var read = false;
            var write = false;

            switch (rights)
            {
                case "r":
                    read = true;
                    break;
                case "w":
                    write = true;
                    break;
                case "rw":
                case "wr":
                    read = true;
                    write = true;
                    break;
            }

            _dbEntities.Permissions.Add(new Permission {
                Name = permissionName,
                Read = read,
                Write = write
            });
            _dbEntities.SaveChanges();
        }

        public void AssignPermissionToRole(string permissionName, string roleName)
        {
            var permission = _dbEntities.Permissions.FirstOrDefault(f => f.Name == permissionName);
            var role = _dbEntities.Roles.FirstOrDefault(f => f.Name == roleName);

            _dbEntities.PermissionToRoles.Add(new PermissionToRole
            {
                PermissionId = permission.Id,
                RoleId = role.Id
            });

            _dbEntities.SaveChanges();
        }

        public void AssignPermissionToResource(string permissionName, string resourceName)
        {
            var permission = _dbEntities.Permissions.FirstOrDefault(f => f.Name == permissionName);
            var resource = _dbEntities.Resources.FirstOrDefault(f => f.FullPath == resourceName);

            _dbEntities.PermissionForResources.Add(new PermissionForResource
            {
                PermissionId = permission.Id,
                ResourceId = resource.Id
            });

            _dbEntities.SaveChanges();
        }

        public void ChangeRights(string permissionName, string rights)
        {
            var read = false;
            var write = false;

            switch (rights)
            {
                case "r":
                    read = true;
                    break;
                case "w":
                    write = true;
                    break;
                case "rw":
                case "wr":
                    read = true;
                    write = true;
                    break;
            }

            var permission = _dbEntities.Permissions.FirstOrDefault(f => f.Name == permissionName);
            permission.Read = read;
            permission.Write = write;
            _dbEntities.SaveChanges();
        }
    }
}
