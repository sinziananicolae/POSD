using POSD_Tema1.Models;
using POSD_Tema1.Services.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSD_Tema1.ControllersAPI
{
    public class UserController : ApiController
    {
        private UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpPost]
        [Route("api/create-user")]
        public Response CreateUser([FromBody]UserModel userModel)
        {
            Response reqResponse = new Response();

            int userId = _userService.GetUser(userModel.username, userModel.password);
            if (userId == -1)
            {
                reqResponse.SetResponse(401, "Not Authorized", "Invalid credentials inserted!", null);
                goto Finish;
            }

            if (userModel.username != "root")
            {
                reqResponse.SetResponse(401, "Not Authorized", "You are not authorized to create a user!", null);
                goto Finish;
            }

            _userService.CreateUser(userModel.newUsername, userModel.newPassword);
            reqResponse = new Response();

        Finish:
            return reqResponse;
        }

    }
}