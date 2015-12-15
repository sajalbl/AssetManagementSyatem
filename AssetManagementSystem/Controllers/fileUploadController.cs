using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO.Compression;
using Ionic.Zip;
using System.Dynamic;
using Newtonsoft.Json;
using AssetManagementSystem.Models;


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

                            var employeeFolder = Path.Combine(source, employeeID + "/");
                            bool present = Directory.Exists(employeeFolder);

                            if (!present)
                            {
                                Directory.CreateDirectory(employeeFolder);
                            }

                        if (string.Equals(Path.GetExtension(uploadedImage.FileName), ".zip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            List<Image> pic = new List<Image>();
                          
                            var zipFilePath = Path.Combine(employeeFolder, uploadedImage.FileName);
                            uploadedImage.SaveAs(zipFilePath);

                            using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead(zipFilePath))
                            {
                                foreach (ZipArchiveEntry entry in archive.Entries)
                                {
                                    entry.ExtractToFile(Path.Combine(employeeFolder, entry.FullName));

                                    Image i = new Image();

                                    i.name = entry.FullName;

                                    pic.Add(i);

                               }
                                using (var context = new Company_dbEntities())
                                {
                                    var result = JsonConvert.SerializeObject(pic);
                                    var image = (from a in context.Resources_table where employeeID == a.Serial select a).FirstOrDefault<Resources_table>();
                                    image.Picture = result;

                                    context.SaveChanges();
                                }
                            }
                            
                        }
                        else
                        {
                            var picture = Path.Combine(employeeFolder, uploadedImage.FileName);
                            
                            using (var context = new Company_dbEntities())
                            {

                                var image = (from a in context.Resources_table where employeeID == a.Serial select a).FirstOrDefault<Resources_table>();

                                if(image != null)
                                {
                                    List<Image> pic = new List<Image>();
                                    Image i = new Image();

                                    i.name = uploadedImage.FileName;

                                    pic.Add(i);
                                    var result = JsonConvert.SerializeObject(pic);

                                    image.Picture = result;
                                    context.SaveChanges();
                                }

                                else
                                {
                                    var profile = (from a in context.Employee_table where employeeID == a.EmployeeID select a).FirstOrDefault<Employee_table>();

                                    profile.Picture = uploadedImage.FileName;
                                    context.SaveChanges();
                                }

                                
                            }
                            uploadedImage.SaveAs(picture);
                        }
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

        [Route("api/manage/UploadCSV")]
        [HttpPost]

        public HttpResponseMessage uploadCSV()
        {
            HttpResponseMessage response = null;

            try
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var uploadedCSV = HttpContext.Current.Request.Files["UploadCSV"];
                    var path = HttpContext.Current.Request.Params["FolderPath"];

                    if (uploadedCSV != null)
                    {
                        var source = Path.Combine(HttpContext.Current.Server.MapPath("~/CSV/"), path);
                        bool exist = Directory.Exists(source);

                        if (!exist)
                        {
                            Directory.CreateDirectory(source);

                        }

                        var csvName = Path.GetFileNameWithoutExtension(uploadedCSV.FileName);
                        var uploadPath = Path.Combine(source, csvName);

                        uploadedCSV.SaveAs(uploadPath);

                        //csvUploadAdapter adp = new csvUploadAdapter();
                        //csvUploadResponse result = adp.parseCSV(uploadedCSV);
                        response = Request.CreateResponse(HttpStatusCode.OK, true);
                    }

                   
                }

                
            }

            catch(Exception ex)
            {
                throw ex;
            }
            return response;
        }


    }

    public class Image
    {
        public string name { get; set; }
    }


}

