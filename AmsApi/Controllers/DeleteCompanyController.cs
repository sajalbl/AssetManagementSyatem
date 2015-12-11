using AmsApi.Adapter;
using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmsApi.Controllers
{
    public class DeleteCompanyController : ApiController
    {
        [Route("api/manage/deleteCompany")]
        [HttpPost]


       // NameValueCollection col = new NameValueCollection();
        public HttpResponseMessage delete(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            { 
                DeleteCompanyAdapter adp = new DeleteCompanyAdapter();
                ManageCompanyResponse result = adp.deleteCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception EX)
            {
                throw EX;
            }
            return response;
        }
       
    }
}