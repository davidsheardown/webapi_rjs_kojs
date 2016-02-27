using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using EntityDTO;
using BusinessServices;
using BusinessServices.Contracts;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

         #region Public Constructor

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #endregion

        [HttpPost]
        public HttpResponseMessage Post(UserRequestDTO loginDTO)
        {
            if (loginDTO != null)
            {
                var user = _userServices.GetUserLogin(loginDTO.loginUsername);
                if (user != null)
                {
                    // Need to validate users password / hashed
                    if (Security.Authenticate.VerifyPassword(loginDTO.loginPassword, user.Password))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No user found matching supplied data");
        }

        [HttpPost]
        [Route("api/v1/user/createuser")]
        public HttpResponseMessage CreateUser(UserRequestDTO userDTO)
        {
            if (userDTO != null)
            {
                // Create a salted/hashed password
                string saltedHashedPassword = Security.Authenticate.CreatePasswordHash(userDTO.loginPassword);
                var user = _userServices.CreateUserLogin(userDTO.loginUsername, saltedHashedPassword);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No username or password sent");
        }
    }
}
