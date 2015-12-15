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
        [Route("api/manage/newEmployee")]
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/checkEmployee")]
        [HttpPost]
        public HttpResponseMessage CheckEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.checkEmployee(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/employeeCount")]
        [HttpPost]
        public HttpResponseMessage CountEmployee(ManageEmployeeRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ManageEmployeeAdapter adp = new ManageEmployeeAdapter();
                ManageEmployeeResponse result = adp.countEmployee(request);
                response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/employees")]
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/checkManager")]
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/updateEmployee")] 
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/employeeDetail")]
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        [Route("api/manage/managerEmployees")]
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
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }
    }
}