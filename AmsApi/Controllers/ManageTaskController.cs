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
    public class ManageTaskController : ApiController
    {
        [Route("api/manage/task")]
        [HttpPost]

        public HttpResponseMessage addTask(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.AddTask(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }

        [Route("api/manage/TaskList")]
        [HttpPost]

        public HttpResponseMessage task(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.Task(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }


        [Route("api/manage/taskAssign")]
        [HttpPost]

        public HttpResponseMessage taskAssign(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.TaskAssign(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        [Route("api/manage/employeeConfirm")]
        [HttpPost]

        public HttpResponseMessage employeeConfirm(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.EmployeeConfirm(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        [Route("api/manage/approval")]
        [HttpPost]

        public HttpResponseMessage approval(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.Approval(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        [Route("api/manage/deleteTask")]
        [HttpPost]

        public HttpResponseMessage deleteTask(ManageTaskRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageTaskAdapter adp = new ManageTaskAdapter();
                ManageTaskResponse result = adp.DeleteTask(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        
    }
}