using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AmsApi.Controllers
{
    public class ManageCompanyController : Controller
    {
        //// GET: ManageCompany
        //public ActionResult Index()
        //{
        //    return View();
        //}
       
        [Route("api/manage/newCompany")]
        [HttpPost]
        public HttpResponseMessage ManageCompany(ManageCompanyRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            return response;
        }
    }
}