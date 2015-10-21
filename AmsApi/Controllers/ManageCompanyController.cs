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
    public class ManageCompanyController : ApiController
    {
       [Route("api/manage/newCompany")]
       [HttpPost]
        public HttpResponseMessage ManageCompany(ManageCompanyRequest request)
       
       {
           HttpResponseMessage response = new HttpResponseMessage();
           
           try          
           {
               NewCompanyAdapter adp = new NewCompanyAdapter();
               ManageCompanyResponse result = adp.AddCompany(request);
               response = Request.CreateResponse(HttpStatusCode.OK, result);
           }
           catch(Exception Ex)
           {
               throw Ex;
           }
           return response;
        }
    }
}
