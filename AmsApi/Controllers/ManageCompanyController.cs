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
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.AddCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/searchCompany")]
        [HttpPost]
        public HttpResponseMessage searchCompany(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.SearchCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/updateCompany")]
        [HttpPost]
        public HttpResponseMessage updateCompany(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.UpdateCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/checkCompany")]
        [HttpPost]
        public HttpResponseMessage checkCompany(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.CheckCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        
    }
}
