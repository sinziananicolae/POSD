using POSD_Tema1.Models;
using POSD_Tema1.Services.ConstraintsService;
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
    public class RoleController : ApiController
    {
        private UserService _userService;
        private RoleService _roleService;
        private ConstraintsService _constraintsService;
        private PermissionService _permissionService;

        public RoleController()
        {
            _userService = new UserService();
            _roleService = new RoleService();
            _constraintsService = new ConstraintsService();
            _permissionService = new PermissionService();
        }

        [HttpPost]
        [Route("api/create-role")]
        public Response CreateRole([FromBody]RoleModel roleModel)
        {
            Response reqResponse = new Response();

            if (roleModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to create a role!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(roleModel.username, roleModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (_roleService.ExistsRole(roleModel.roleName))
            {
                reqResponse.SetResponse(500, "Already Existing", "Role '" + roleModel.roleName + "' already exists in the system.", null);
                goto Finish;
            }

            _roleService.CreateRole(roleModel.roleName);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/assign-role")]
        public Response AssignRole([FromBody]RoleModel roleModel)
        {
            Response reqResponse = new Response();

            if (roleModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to assign a user to a role!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(roleModel.username, roleModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_roleService.ExistsRole(roleModel.roleName))
            {
                reqResponse.SetResponse(500, "Not Existing", "Role '" + roleModel.roleName + "' does not exist in the system.", null);
                goto Finish;
            }

            if (!_userService.ExistsUser(roleModel.userInRole))
            {
                reqResponse.SetResponse(500, "Not Existing", "User '" + roleModel.userInRole + "' does not exist in the system.", null);
                goto Finish;
            }

            if (_roleService.ExistsUserInRole(roleModel.roleName, roleModel.userInRole))
            {
                reqResponse.SetResponse(500, "Already Existing", "User '" + roleModel.userInRole + "' is already assigned to role '" + roleModel.roleName + "'.", null);
                goto Finish;
            }

            if (_constraintsService.ExistConstraints(roleModel.userInRole, roleModel.roleName)) {
                reqResponse.SetResponse(500, "Forbidden!", "User '" + roleModel.userInRole + "' cannot be assigned to role '" + roleModel.roleName + "' as it is restricted by a constraint.", null);
                goto Finish;
            }

            _roleService.AssignRole(roleModel.roleName, roleModel.userInRole);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/add-permission-to-role")]
        public Response AddPermissionToRole([FromBody]PermissionModel permissionModel)
        {
            Response reqResponse = new Response();

            if (permissionModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to add a user to a role!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(permissionModel.username, permissionModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_roleService.ExistsRole(permissionModel.roleName))
            {
                reqResponse.SetResponse(500, "Not Existing", "Role '" + permissionModel.roleName + "' does not exist in the system.", null);
                goto Finish;
            }

            if (!_permissionService.ExistsPermission(permissionModel.permissionName))
            {
                reqResponse.SetResponse(500, "Not Existing", "User '" + permissionModel.permissionName + "' does not exist in the system.", null);
                goto Finish;
            }

            if (_roleService.ExistsPermissionInRole(permissionModel.roleName, permissionModel.permissionName))
            {
                reqResponse.SetResponse(500, "Already existing", "Permission '" + permissionModel.permissionName + "' is already assigned to role '" + permissionModel.roleName + "'.", null);
                goto Finish;
            }

            _permissionService.AssignPermissionToRole(permissionModel.permissionName, permissionModel.roleName);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

        [HttpPost]
        [Route("api/revoke-role")]
        public Response RevokeRole([FromBody]RoleModel roleModel)
        {
            Response reqResponse = new Response();

            if (roleModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to revoke a user from a role!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(roleModel.username, roleModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (!_roleService.ExistsRole(roleModel.roleName))
            {
                reqResponse.SetResponse(500, "Not Existing", "Role '" + roleModel.roleName + "' does not exist in the system.", null);
                goto Finish;
            }

            if (!_userService.ExistsUser(roleModel.userInRole))
            {
                reqResponse.SetResponse(500, "Not Existing", "User '" + roleModel.userInRole + "' does not exist in the system.", null);
                goto Finish;
            }

            if (!_roleService.ExistsUserInRole(roleModel.roleName, roleModel.userInRole))
            {
                reqResponse.SetResponse(500, "Invalid", "User '" + roleModel.userInRole + "' is not assigned to role '" + roleModel.roleName + "'.", null);
                goto Finish;
            }

            _roleService.RevokeRole(roleModel.roleName, roleModel.userInRole);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }
    }
}