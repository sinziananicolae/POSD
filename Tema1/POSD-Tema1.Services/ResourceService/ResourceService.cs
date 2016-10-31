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

        public bool CheckResourceOwner(string resourcePath, int userId)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourcePath && f.OwnerId == userId).FirstOrDefault();

            if (resource == null)
                return false;

            return true;
        }

        public bool IsResourceUnique(string resourceName)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            if (resource == null)
                return true;

            return false;
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
                Read = parent.Read,
                Write = parent.Write
            });

            _dbEntities.SaveChanges();
        }

        public bool CheckResourceRights(string resourceName, int userId, string type)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            switch (type)
            {
                case "r":
                    if (resource.OwnerId == userId || resource.Read == true)
                        return true;
                    break;
                case "w":
                    if (resource.OwnerId == userId || resource.Write == true)
                        return true;
                    break;
                default:
                    if (resource.OwnerId == userId)
                        return true;
                    break;
            }

            return false;
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

        public bool IsFile(string resourceName)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();

            if (resource.ResourceTypeId == 2)
                return true;

            return false;
        }

        public void WriteInFile(string resourceName, string value)
        {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourceName).FirstOrDefault();
            resource.Content = resource.Content + value;
            _dbEntities.SaveChanges();
        }

        public void SetRights(string resourceName, string rights)
        {
            IEnumerable<Resource> contentOfDirectory = _dbEntities.Resources.Where(f => f.FullPath.StartsWith(resourceName)).ToList();
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

            foreach (Resource resource in contentOfDirectory)
            {
                if (resource.Write == null || resource.FullPath == resourceName)
                {
                    resource.Write = write;
                }

                if (resource.Read == null || resource.FullPath == resourceName)
                {
                    resource.Read = read;
                }
            }

            _dbEntities.SaveChanges();
        }

        public List<object> GetAllResources() {
            IEnumerable<Resource> allResources = _dbEntities.Resources.OrderBy(f => f.FullPath).ToList();
            List<object> allRes = new List<object>();

            foreach (Resource resource in allResources) {
                allRes.Add(new {
                    resource.FullPath,
                    resource.ResourceTypeId,
                    Level = resource.FullPath.Split('/').Count() - 1,
                    Owner = resource.User.Username,
                    Read = resource.Read == true ? "yes" : "no",
                    Write = resource.Write == true ? "yes" : "no"
                });
            }

            return allRes;
        }
    }
}
