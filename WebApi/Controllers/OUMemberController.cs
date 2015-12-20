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
using WebApi.Filters;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OUMemberController : ApiController
    {
         private readonly IOUMemberServices _ouMemberServices;

         #region Public Constructor

        public OUMemberController(IOUMemberServices ouMemberServices)
        {
            _ouMemberServices = ouMemberServices;
        }

        #endregion

        [Route("api/v1/OUMember/all")]
        [Route("api/v1/OUMember")]
        [HttpGet]
        [ValidateUser]
        public HttpResponseMessage Get()
        {
            var ouMembers = _ouMemberServices.GetAllOUMembers();
            if (ouMembers != null)
            {
                var ouMemberEntities = ouMembers as List<OUmemberEntity> ?? ouMembers.ToList();
                if (ouMemberEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, ouMemberEntities);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "OUMembers not found");
        }

        //[Route("api/v1/OUMember/{memberName}")]
        //public HttpResponseMessage Get(string memberName)
        //{
        //    var ouMember = _ouMemberServices.GetAllOUMembers()
        //        .Where(x => x.Firstname.ToLower() == memberName);
        //    if (ouMember != null)
        //        return Request.CreateResponse(HttpStatusCode.OK, ouMember);
        //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No OUMember found matching: " + memberName);
        //}

        [Route("api/v1/OUMember/{name}")]
        [HttpGet]
        public HttpResponseMessage Get(string name)
        {
            var ouMembers = _ouMemberServices.GetOUMemberByName(name);
            if (ouMembers != null)
                return Request.CreateResponse(HttpStatusCode.OK, ouMembers);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No OUMembers found matching: " + name);
        }

        [Route("api/v1/OUMember/{id:int}")]
        [Route("api/v1/OUMember/{order=int}")]
        [HttpGet]
        public HttpResponseMessage Get(long id)
        {
            var ouMember = _ouMemberServices.GetOUMemberById(id);
            if (ouMember != null)
                return Request.CreateResponse(HttpStatusCode.OK, ouMember);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No OUMember found for this id");
        }

        public long Post([FromBody] OUmemberEntity ouMemberEntity)
        {
            return _ouMemberServices.CreateOUMember(ouMemberEntity);
        }

        public bool Put(long id, [FromBody]OUmemberEntity ouMemberEntity)
        {
            if (id  > 0)
            {
                return _ouMemberServices.UpdateOUMember(id,ouMemberEntity);
            }
            return false;
        }

        public bool Delete(long id)
        {
            if (id > 0)
                return _ouMemberServices.DeleteOUMember(id);
            return false;
        }
    }
}
