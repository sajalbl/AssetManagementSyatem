using AmsApi.Adapter;
using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AmsApi.Controllers
{
    public class AllCompanyController : ApiController
    {
        [Route("api/manage/allCompany")]
        [HttpPost]

        public HttpResponseMessage delete()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                AllCompanyAdapter adp = new AllCompanyAdapter();
                ManageCompanyResponse result = adp.allCompany();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception EX)
            {
                throw EX;
            }
            return response;
        }
    }
}