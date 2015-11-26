using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;

namespace AmsApi.Controllers
{
    public class imageController : ApiController
    {
        [Route("api/manage/imageUpload")]
        [HttpPost]

        public HttpResponseMessage Post()
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
                        using (var fileStream = File.OpenRead(readSavePath))
                        {
                            insertDB DB = new insertDB();
                            bool result = DB.addToDB(employeeID.ToString(), uploadedImage.ToString());
                            if (result == true)
                            {
                                fileStream.Close();
                            }

                        }
                        var picture = Path.Combine(source, uploadedImage.FileName);
                        uploadedImage.SaveAs(picture);

                    }
                }
                response = Request.CreateResponse(HttpStatusCode.OK, true);

            }
            catch (Exception ex)
            {

            }
            return response;


        }

    }

    public class insertDB
    {
        public bool addToDB(string EmployeeID, string Picture)
        {
            using (var context = new Company_dbEntities())
            {
                var image = (from a in context.Employee_table where a.EmployeeID == EmployeeID select a).FirstOrDefault<Employee_table>();

                if (image != null)
                {
                    image.Picture = Picture;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    context.SaveChanges();
                    return false;
                }
            }
        }
    }
}