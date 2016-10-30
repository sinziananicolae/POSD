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

        public bool IsPathValid(string resourcePath) {
            var resource = _dbEntities.Resources.Where(f => f.FullPath == resourcePath).FirstOrDefault();

            if (resource == null)
                return false;

            return true;
        }

        public bool CheckResourceOwner(string resourcePath, int userId) {
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

        public void CreateResource(string resourceName, string fullResourcePath, int userId, int resourceTypeId, string value) {
            _dbEntities.Resources.Add(new Resource {
                Name = resourceName,
                FullPath = fullResourcePath,
                OwnerId = userId,
                Content = value,
                ResourceTypeId = resourceTypeId
            });

            _dbEntities.SaveChanges();
        }
    }
}
