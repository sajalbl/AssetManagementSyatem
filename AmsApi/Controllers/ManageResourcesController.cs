using AmsApi.Adapter;
using AmsApi.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AmsApi.Controllers
{
    public class ManageResourcesController : ApiController
    {
        [Route("api/manage/newResources")]
        [HttpPost]
        public HttpResponseMessage ManageResources(ManageResourcesRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageResourcesAdapter adp = new ManageResourcesAdapter();
                ManageResourcesResponse result = adp.AddResources(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/showResources")]
        [HttpPost]
        public HttpResponseMessage showResources(ManageResourcesRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageResourcesAdapter adp = new ManageResourcesAdapter();
                ManageResourcesResponse result = adp.ShowResources(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }


        [Route("api/manage/updateResources")]
        [HttpPost]
        public HttpResponseMessage updateResources(ManageResourcesRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageResourcesAdapter adp = new ManageResourcesAdapter();
                ManageResourcesResponse result = adp.UpdateResources(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/deleteResources")]
        [HttpPost]
        public HttpResponseMessage deleteResources(ManageResourcesRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageResourcesAdapter adp = new ManageResourcesAdapter();
                ManageResourcesResponse result = adp.DeleteResources(request);
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