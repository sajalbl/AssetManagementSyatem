using AssetManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AssetManagementSystem.Controllers
{
    public class csvUploadController: ApiController
    {
        [Route("api/manage/csvController")]
        [HttpPost]

        public HttpResponseMessage parseCSV(csvUploadRequest request)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            csvUploadAdapter adp = new csvUploadAdapter();

            csvUploadResponse result = adp.parseCSV(request.csvFilePath);

            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}