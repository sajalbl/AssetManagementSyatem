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
    public class fileUploadController : ApiController
    {
        [Route("api/manage/imageUpload")]
        [HttpPost]

        public HttpResponseMessage uploadImage()
        {
            HttpResponseMessage response = null;

            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var uploadedImage = HttpContext.Current.Request.Files["UploadImage"];
                    var path = HttpContext.Current.Request.Params["FolderPath"];
                    var employeeID = HttpContext.Current.Request.Params["EmployeeID"];

                    if (uploadedImage != null)
                    {
                        var source = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), path);
                        bool exist = Directory.Exists(source);

                        if (!exist)
                        {
                            Directory.CreateDirectory(source);
                        }

                        var readSavePath = string.Format(@"{0}/{1}", source, uploadedImage);
                        //using (var fileStream = File.OpenRead(readSavePath))
                        //{
                        //    insertDB DB = new insertDB();
                        //    bool result = DB.addToDB(employeeID.ToString(), uploadedImage.ToString());
                        //    if (result == true)
                        //    {
                        //        fileStream.Close();
                        //    }

                        //}
                        var picture = Path.Combine(source, uploadedImage.ToString());
                        uploadedImage.SaveAs(picture);

                    }
                    response = Request.CreateResponse(HttpStatusCode.OK, true);
                }
            }



            catch (Exception ex)
            {
                throw ex;
            }
            return response;


        }
    }
}
