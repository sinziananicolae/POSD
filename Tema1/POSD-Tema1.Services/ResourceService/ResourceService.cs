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
                    if (IsResourceReadable(resource))
                        return true;
                    break;
                case "w":
                    if (IsResourceWritable(resource))
                        return true;
                    break;
            }

            return false;
        }

        public bool IsResourceReadable (Resource resource) {
            var read = false;

            if (resource.Read != null) {
                return (bool)resource.Read;
            }

            while (resource.ParentId != null && read == false) {
                resource = _dbEntities.Resources.FirstOrDefault(f => f.Id == resource.ParentId);
                if (resource.Read == true)
                {
                    read = true;
                }
            }

            return read;
        }

        public bool IsResourceWritable(Resource resource)
        {
            var write = false;

            if (resource.Write != null)
            {
                return (bool)resource.Write;
            }

            while (resource.ParentId != null && write == false)
            {
                resource = _dbEntities.Resources.FirstOrDefault(f => f.Id == resource.ParentId);
                if (resource.Write == true)
                {
                    write = true;
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
            resource.Content = resource.Content + value;
            _dbEntities.SaveChanges();
        }

        public void SetRights(string resourceName, string rights)
        {
            var resource = _dbEntities.Resources.FirstOrDefault(f => f.FullPath == resourceName);

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
            
            resource.Write = write;
            resource.Read = read;

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
                    Read = resource.Read == true ? "yes" : resource.Read == false ? "no" : "",
                    Write = resource.Write == true ? "yes" : resource.Write == false ? "no" : ""
                });
            }

            return allRes;
        }
    }
}
