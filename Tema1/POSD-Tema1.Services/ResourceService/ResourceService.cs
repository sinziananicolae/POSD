using POSD_Tema1.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSD_Tema1.Services
{
    public class ResourceService
    {
        private readonly Entities _dbEntities;

        public ResourceService()
        {
            _dbEntities = new Entities();
        }

        public IEnumerable<ResourceType> GetResourceTypes()
        {
            IEnumerable<ResourceType> allresTypes = _dbEntities.ResourceTypes.ToList();

            return allresTypes;
        }

        public bool IsPathValid(string resourcePath)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourcePath).FirstOrDefault();

            if (resource == null)
                return false;

            return true;
        }

        public bool IsUserOwner(string resourcePath, int userId)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourcePath && f.OwnerId == userId).FirstOrDefault();

            if (resource == null)
                return false;

            return true;
        }

        public bool ResourceExists(string resourceName)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            if (resource == null)
                return false;

            return true;
        }

        public void CreateResource(string resourceName, string fullResourcePath, string parentPath, int userId, int resourceTypeId, string value)
        {
            var parent = _dbEntities.Resources.Where(f => f.FullPath == parentPath).FirstOrDefault();

            _dbEntities.Resources.Add(new Resource
            {
                Name = resourceName,
                FullPath = fullResourcePath,
                OwnerId = userId,
                Content = value,
                ResourceTypeId = resourceTypeId,
                ParentId = parent.Id
            });

            _dbEntities.SaveChanges();
        }

        public bool IsResourceAccessible(string resourceName, int userId, string type)
        {
            Resource resource = _dbEntities.Resources.FirstOrDefault(f => f.FullPath == resourceName);

            if (resource.OwnerId == userId)
                return true;

            switch (type)
            {
                case "r":
                    if (IsResourceReadable(userId, resource))
                        return true;
                    break;
                case "w":
                    if (IsResourceWritable(userId, resource))
                        return true;
                    break;
            }

            return false;
        }

        public List<int> GetPermissionsOfUser(List<int> permissionsOfRole, List<int> permissionsOfUser, int roleId)
        {
            foreach (int permissionId in permissionsOfRole)
                permissionsOfUser.Add(permissionId);
            List<Role> childrenOfRole = _dbEntities.Roles.Where(f => f.ParentId == roleId).ToList();
            foreach (Role childRole in childrenOfRole)
            {
                permissionsOfRole = _dbEntities.PermissionToRoles.Where(f => f.RoleId == childRole.Id).Select(f => f.PermissionId).ToList();
                return GetPermissionsOfUser(permissionsOfRole, permissionsOfUser, childRole.Id);
            }
            return permissionsOfRole;
        }

        public bool IsResourceReadable(int userId, Resource resource) {
            var read = false;
            List<UserInRole> rolesOfUser = _dbEntities.UserInRoles.Where(f => f.UserId == userId).ToList();
            List<int> permissionsOfUser = new List<int>();

            foreach (UserInRole userInRole in rolesOfUser) {
                List<int> permissionsOfRole = userInRole.Role.PermissionToRoles.Select(f => f.PermissionId).ToList();

                permissionsOfUser = GetPermissionsOfUser(permissionsOfRole, permissionsOfUser, userInRole.RoleId);
            }

            foreach (PermissionForResource permissionForResource in resource.PermissionForResources)
            {
                if (permissionsOfUser.Contains(permissionForResource.PermissionId) && permissionForResource.Permission.Read == true)
                {
                    read = true;
                    break;
                }
            }

            if (read == true) return read;

            while (resource.ParentId != null && read == false)
            {
                resource = _dbEntities.Resources.FirstOrDefault(f => f.Id == resource.ParentId);
                foreach (PermissionForResource permissionForResource in resource.PermissionForResources)
                {
                    if (permissionsOfUser.Contains(permissionForResource.PermissionId) && permissionForResource.Permission.Read == true)
                    {
                        read = true;
                        break;
                    }
                }
            }

            return read;
        }

        public bool IsResourceWritable(int userId, Resource resource)
        {
            var write = false;
            List<UserInRole> rolesOfUser = _dbEntities.UserInRoles.Where(f => f.UserId == userId).ToList();
            List<int> permissionsOfUser = new List<int>();

            foreach (UserInRole userInRole in rolesOfUser)
            {
                List<int> permissionsOfRole = userInRole.Role.PermissionToRoles.Select(f => f.PermissionId).ToList();

                permissionsOfUser = GetPermissionsOfUser(permissionsOfRole, permissionsOfUser, userInRole.RoleId);
            }

            foreach (PermissionForResource permissionForResource in resource.PermissionForResources)
            {
                if (permissionsOfUser.Contains(permissionForResource.PermissionId) && permissionForResource.Permission.Write == true)
                {
                    write = true;
                    break;
                }
            }

            if (write == true) return write;

            while (resource.ParentId != null && write == false)
            {
                resource = _dbEntities.Resources.FirstOrDefault(f => f.Id == resource.ParentId);
                foreach (PermissionForResource permissionForResource in resource.PermissionForResources)
                {
                    if (permissionsOfUser.Contains(permissionForResource.PermissionId) && permissionForResource.Permission.Write == true)
                    {
                        write = true;
                        break;
                    }
                }
            }

            return write;
        }

        public string GetResourceContent(string resourceName)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            if (resource.ResourceTypeId == 2)
            {
                return resource.Content;
            }
            else
            {
                int pathLevel = resourceName.Split('/').Count();
                IEnumerable<Resource> contentOfDirectory = _dbEntities.Resources.Where(f => f.FullPath.StartsWith(resourceName)).ToList();
                var content = "";

                foreach (Resource element in contentOfDirectory)
                {
                    if (element.FullPath.Split('/').Count() == pathLevel + 1)
                        content += element.FullPath.Split('/')[pathLevel] + "; ";
                }

                return content;
            }
        }

        public bool IsDirectory(string resourceName)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            if (resource.ResourceTypeId == 1)
                return true;

            return false;
        }

        public void WriteInFile(string resourceName, string value)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();
            resource.Content = value;
            _dbEntities.SaveChanges();
        }

        public Dictionary<string, object> GetAllResources() {
            IEnumerable<Resource> allResources = _dbEntities.Resources.OrderBy(f => f.FullPath).ToList();
            Dictionary<string, object> allRes = new Dictionary<string, object>();
            List<object> resources = new List<object>();

            foreach (Resource resource in allResources) {
                resources.Add(new {
                    resource.FullPath,
                    resource.ResourceTypeId,
                    Level = resource.FullPath.Split('/').Count() - 1,
                    Owner = resource.User.Username,
                    Permissions = resource.PermissionForResources.Select(f => new {
                        f.Permission.Name
                    }).ToList()
                });
            }

            allRes.Add("allResources", resources);

            IEnumerable<Role> allRoles = _dbEntities.Roles.ToList();
            List<object> roles = new List<object>();

            foreach (Role role in allRoles) {
                var parentRole = _dbEntities.Roles.FirstOrDefault(f => f.Id == role.ParentId);
                roles.Add(new {
                    role.Name,
                    Users = role.UserInRoles.Select(f => new {
                        f.User.Username
                    }).ToList(),
                    ParentRoleName = parentRole != null ? parentRole.Name : "",
                    Permissions = role.PermissionToRoles.Select(f => new {
                        f.Permission.Name
                    }).ToList()
                });
            }

            allRes.Add("allRoles", roles);

            IEnumerable<Permission> allPermissions = _dbEntities.Permissions.ToList();
            List<object> permissions = new List<object>();

            foreach (Permission permission in allPermissions)
            {
                permissions.Add(new
                {
                    permission.Name,
                    permission.Read,
                    permission.Write
                });
            }

            allRes.Add("allPermissions", permissions);

            IEnumerable<Constraint> allConstraints = _dbEntities.Constraints.ToList();
            List<object> constraints = new List<object>();

            foreach (Constraint constraint in allConstraints)
            {
                constraints.Add(new
                {
                    constraint.Id,
                    Role1 = constraint.Role.Name,
                    Role2 = constraint.Role1.Name
                });
            }

            allRes.Add("allConstraints", constraints);

            return allRes;
        }
    }
}
