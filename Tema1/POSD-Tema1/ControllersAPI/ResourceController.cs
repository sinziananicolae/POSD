using POSD_Tema1.Models;
using POSD_Tema1.Services;
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

        public ResourceController()
        {
            _resourceService = new ResourceService();
            _userService = new UserService();
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
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!");
                goto Finish;
            }

            ResourcePathModel resourceInfo = new ResourcePathModel(resourceModel.resourceName);

            if (!_resourceService.IsPathValid(resourceInfo.resourcePath)) {
                reqResponse.SetResponse(404, "Not Existing", resourceInfo.resourcePath + " does not exist in the current filesystem.");
                goto Finish;
            }

            if (!_resourceService.CheckResourceOwner(resourceInfo.resourcePath, userId))
            {
                reqResponse.SetResponse(401, "Not Authorized", "You do not have the rights to access this resource. Please contact the owner of the selected folder.");
                goto Finish;
            }

            if (!_resourceService.IsResourceUnique(resourceInfo.fullResourcePath))
            {
                reqResponse.SetResponse(500, "Already Existing", resourceInfo.fullResourcePath + " already exists in the current filesystem.");
                goto Finish;
            }

            _resourceService.CreateResource(resourceInfo.resourceName, resourceInfo.fullResourcePath, userId, resourceModel.resourceTypeId, resourceModel.value);

        Finish:
            return reqResponse;
        }

    }
}