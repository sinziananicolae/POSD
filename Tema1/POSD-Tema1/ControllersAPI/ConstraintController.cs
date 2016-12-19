using POSD_Tema1.Models;
using POSD_Tema1.Services.ConstraintsService;
using POSD_Tema1.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSD_Tema1.ControllersAPI
{
    public class ConstraintController : ApiController
    {
        private ConstraintsService _constraintsService;
        private UserService _userService;

        public ConstraintController()
        {
            _constraintsService = new ConstraintsService();
            _userService = new UserService();
        }

        [HttpPost]
        [Route("api/create-constraint")]
        public Response CreatePermission([FromBody]ConstraintModel constraintModel)
        {
            Response reqResponse = new Response();

            if (constraintModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to create a constraint!", null);
                goto Finish;
            }

            int userId = _userService.GetUser(constraintModel.username, constraintModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (_constraintsService.ExistsConstraint(constraintModel.roleName1, constraintModel.roleName2))
            {
                reqResponse.SetResponse(500, "Already Existing", "The constraint already exists in the system.", null);
                goto Finish;
            }

            _constraintsService.CreateConstraint(constraintModel.roleName1, constraintModel.roleName2);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

    }
}