using AssetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AssetManagementSystem.Controllers
{
    public class csvUploadController : ApiController
    {
        [Route("api/manage/csvController")]
        [HttpPost]

        public HttpResponseMessage parseCSV(csvUploadRequest request)
        {
            var path = "";
            HttpResponseMessage response = new HttpResponseMessage();

            csvUploadAdapter adp = new csvUploadAdapter();

            var source = Path.Combine(HttpContext.Current.Server.MapPath("~/CSV/"), path);

            request.FilePath = Path.Combine(source, request.FileName);

            csvUploadResponse result = adp.ParseCSV(request);

            response = Request.CreateResponse(HttpStatusCode.OK, result);

            return response;
        }

        [Route("api/manage/downloadCsv")]
        [HttpPost]

        public HttpResponseMessage downloadCsv(csvUploadRequest request)
         {
             var path = "";

             var source = Path.Combine(HttpContext.Current.Server.MapPath("~/Downloads/"), path);

             request.FilePath = source;

             HttpResponseMessage response = new HttpResponseMessage();

             csvUploadAdapter adp = new csvUploadAdapter();

             string result = adp.DownloadCsv(request);

             csvUploadResponse res = adp.download(request,result);

             response = Request.CreateResponse(HttpStatusCode.OK, res);

             return response;
          }

    }
}