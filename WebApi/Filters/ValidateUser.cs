using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Filters
{
    public class ValidateUser : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext context)
        {
            try
            {
                string userToken = Convert.ToString(context.Request.Headers.GetValues("usertoken").FirstOrDefault());
                if (userToken == null)
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
                context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.ExpectationFailed);
                context.Response.ReasonPhrase = "Not logged in or no valid user token/Api key. " + ex.Message;
                base.OnAuthorization(context);
            }
        }
    }
}
