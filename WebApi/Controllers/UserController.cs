using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using BusinessEntities;
using BusinessServices;
using BusinessServices.Contracts;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        public HttpResponseMessage Post(BusinessEntities.LoginRequestEntity loginEntity)
        {
            if (loginEntity != null)
            {
                var user = _userServices.GetUserLogin(loginEntity.loginUsername, loginEntity.loginPassword);
                if (user != null)
                    return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found matching supplied data");
        }
    }
}
