using POSD_Tema1.Models;
using POSD_Tema1.Services;
using POSD_Tema1.Services.PermissionService;
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
    public class PermissionController : ApiController
    {
        private ResourceService _resourceService;
        private UserService _userService;
        private RoleService _roleService;
        private PermissionService _permissionService;

        public PermissionController()
        {
            _resourceService = new ResourceService();
            _userService = new UserService();
            _roleService = new RoleService();
            _permissionService = new PermissionService();
        }

        [HttpPost]
        [Route("api/create-permission")]
        public Response CreatePermission([FromBody]PermissionModel permissionModel)
        {
            Response reqResponse = new Response();

            if (permissionModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to create a permission!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(permissionModel.username, permissionModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (_permissionService.ExistsPermission(permissionModel.permissionName))
            {
                reqResponse.SetResponse(500, "Already Existing", "Permission '" + permissionModel.permissionName + "' already exists in the system.", null);
                goto Finish;
            }

            _permissionService.CreatePermission(permissionModel.permissionName, permissionModel.rights);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/change-rights")]
        public Response ChangeRights([FromBody]PermissionModel permissionModel)
        {
            Response reqResponse = new Response();

            if (permissionModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to change the rights of the selected permission!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(permissionModel.username, permissionModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_permissionService.ExistsPermission(permissionModel.permissionName))
            {
                reqResponse.SetResponse(500, "Not Existing", "Permission '" + permissionModel.permissionName + "' does not exist in the system.", null);
                goto Finish;
            }

            _permissionService.ChangeRights(permissionModel.permissionName, permissionModel.rights);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

    }
}