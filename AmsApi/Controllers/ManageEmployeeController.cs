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
    public class ManageEmployeeController : ApiController
    {
        [Route("api/Employee/newEmployee")]
        [HttpPost]
        public HttpResponseMessage ManageEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.AddEmployee(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Employee/searchEmployee")]
        [HttpPost]
        public HttpResponseMessage CheckEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.EmployeeDetail(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        //[Route("api/manage/employeeCount")]
        //[HttpPost]
        //public HttpResponseMessage CountEmployee(ManageEmployeeRequest request)
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    try
        //    {
        //        ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
        //        ManageEmployeeResponse result = adp.countEmployee(request);
        //        response = Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    return response;
        //}

        [Route("api/Employee/employees")]
        [HttpPost]
        public HttpResponseMessage Employees(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.employees(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Employee/checkManager")]
        [HttpPost]
        public HttpResponseMessage checkManager(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.CheckManager(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Employee/updateEmployee")]
        [HttpPost]
        public HttpResponseMessage updateEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.UpdateEmployee(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Employee/employeeDetail")]
        [HttpPost]
        public HttpResponseMessage employeeDetail(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.EmployeeDetail(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }

        [Route("api/Employee/managerEmployees")]
        [HttpPost]
        public HttpResponseMessage managerEmployees(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.ManagerEmployees(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }


        [Route("api/Employee/replaceEmployee")]
        [HttpPost]
        public HttpResponseMessage replaceEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.ReplaceEmployee(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                HttpError myCustomError = new HttpError(e.Message) { { "IsSuccess", false } };
                return Request.CreateErrorResponse(HttpStatusCode.OK, myCustomError);
            }
            return response;
        }
    }
}