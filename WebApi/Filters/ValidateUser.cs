using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Practices.Unity;

using EntityDTO;
using BusinessServices;
using BusinessServices.Contracts;

namespace WebApi.Filters
{
    public class ValidateUser : AuthorizeAttribute
    {
        private IUserServices _userServices;

        public ValidateUser()
        {
            UnityContainer resolver = new Microsoft.Practices.Unity.UnityContainer();
            _userServices = resolver.Resolve<UserServices>();
        }

        public override void OnAuthorization(HttpActionContext context)
        {
            try
            {
                string userToken = Convert.ToString(context.Request.Headers.GetValues("usertoken").FirstOrDefault());
                if (userToken == null || !_userServices.CheckUserToken(userToken))
                {
                    HttpContext.Current.Response.AddHeader("usertoken", userToken);
                    HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                    context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
