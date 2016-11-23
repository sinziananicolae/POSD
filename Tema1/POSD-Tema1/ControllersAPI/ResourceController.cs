using POSD_Tema1.Models;
using POSD_Tema1.Services;
using POSD_Tema1.Services.RoleService;
using POSD_Tema1.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSD_Tema1.ControllersAPI
{
    public class ResourceController : ApiController
    {
        private ResourceService _resourceService;
        private UserService _userService;
        private RoleService _roleService;

        public ResourceController()
        {
            _resourceService = new ResourceService();
            _userService = new UserService();
            _roleService = new RoleService();
        }

        public IEnumerable<object> Get()
        {
            return _resourceService.GetResourceTypes();
        }

        [HttpPost]
        [Route("api/create-resource")]
        public Response CreateResource([FromBody] ResourceModel resourceModel)
        {
            Response reqResponse = new Response();

            int userId = _userService.GetUser(resourceModel.username, resourceModel.password);
            if (userId == -1) {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            ResourcePathModel resourceInfo = new ResourcePathModel(resourceModel.resourceName);

            if (!_resourceService.IsPathValid(resourceInfo.resourcePath)) {
                reqResponse.SetResponse(404, "Not Existing", resourceInfo.resourcePath + " does not exist in the current filesystem.", null);
                goto Finish;
            }

            if (!_resourceService.IsUserOwner(resourceInfo.resourcePath, userId))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You do not have the rights to access this resource. Please contact the owner of the selected resource.", null);
                goto Finish;
            }

            if (_resourceService.ResourceExists(resourceInfo.fullResourcePath))
            {
                reqResponse.SetResponse(500, "Already Existing", resourceInfo.fullResourcePath + " already exists in the current filesystem.", null);
                goto Finish;
            }

            _resourceService.CreateResource(resourceInfo.resourceName, resourceInfo.fullResourcePath, resourceInfo.resourcePath, userId, resourceModel.resourceTypeId, resourceModel.value);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/read-resource")]
        public Response ReadResource([FromBody] ResourceModel resourceModel)
        {
            Response reqResponse = new Response();

            int userId = _userService.GetUser(resourceModel.username, resourceModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_resourceService.ResourceExists(resourceModel.resourceName))
            {
                reqResponse.SetResponse(404, "Not Existing", resourceModel.resourceName + " does not exist in the current filesystem.", null);
                goto Finish;
            }

            if (!_resourceService.IsResourceAccessible(resourceModel.resourceName, userId, "r"))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You do not have the rights to access this resource. Please contact the owner of the selected resource.", null);
                goto Finish;
            }

            object content = _resourceService.GetResourceContent(resourceModel.resourceName);
            reqResponse.SetResponse(200, "OK", "", content);

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/write-resource")]
        public Response WriteResource([FromBody] ResourceModel resourceModel)
        {
            Response reqResponse = new Response();

            int userId = _userService.GetUser(resourceModel.username, resourceModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_resourceService.ResourceExists(resourceModel.resourceName))
            {
                reqResponse.SetResponse(404, "Not Existing", resourceModel.resourceName + " does not exist in the current filesystem.", null);
                goto Finish;
            }

            if (!_resourceService.IsResourceAccessible(resourceModel.resourceName, userId, "w"))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You do not have the rights to access this resource. Please contact the owner of the selected resource.", null);
                goto Finish;
            }

            if (_resourceService.IsDirectory(resourceModel.resourceName))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You cannot write inside a directory.", null);
                goto Finish;
            }

            _resourceService.WriteInFile(resourceModel.resourceName, resourceModel.value);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/add-rights")]
        public Response AddRights([FromBody]ResourceModel resourceModel)
        {
            Response reqResponse = new Response();

            int userId = _userService.GetUser(resourceModel.username, resourceModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            ResourcePathModel resourceInfo = new ResourcePathModel(resourceModel.resourceName);

            if (!_resourceService.IsUserOwner(resourceInfo.fullResourcePath, userId))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not allowed to change the roles of the selected resource.", null);
                goto Finish;
            }

            if (!_resourceService.ResourceExists(resourceModel.resourceName))
            {
                reqResponse.SetResponse(404, "Not Existing", resourceModel.resourceName + " does not exist in the current filesystem.", null);
                goto Finish;
            }

            if (!_roleService.ExistsRole(resourceModel.roleName))
            {
                reqResponse.SetResponse(500, "Not Existing", "Role '" + resourceModel.roleName + "' does not exist in the system.", null);
                goto Finish;
            }

            _resourceService.AddRights(resourceModel.roleName, resourceModel.resourceName);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }


        [HttpGet]
        [Route("api/get-all-resources")]
        public Response GetAllResources() {
            Response reqResponse = new Response();
            return reqResponse;
        }


    }
}