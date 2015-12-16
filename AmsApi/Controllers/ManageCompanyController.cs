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
        [Route("api/Company/allCompany")]
        [HttpPost]

        public HttpResponseMessage delete()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.allCompany();
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Company/newCompany")]
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
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Company/searchCompany")]
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
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Company/updateCompany")]
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
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Company/deleteCompany")]
        [HttpPost]

        // NameValueCollection col = new NameValueCollection();
        public HttpResponseMessage delete(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.deleteCompany(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Company/allManagers")]
        [HttpPost]

        public HttpResponseMessage manage(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageCompanyAdapter adp = new ManageCompanyAdapter();
                ManageCompanyResponse result = adp.allManagers(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        //[Route("api/manage/checkCompany")]
        //[HttpPost]
        //public HttpResponseMessage checkCompany(ManageCompanyRequest request)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    try
        //    {
        //        ManageCompanyAdapter adp = new ManageCompanyAdapter();
        //        ManageCompanyResponse result = adp.CheckCompany(request);
        //        response = Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    return response;
        //}
        
    }
}
