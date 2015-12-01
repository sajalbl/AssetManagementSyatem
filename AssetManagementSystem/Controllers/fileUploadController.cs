﻿using System;
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
                            List<dynamic> zipFileDatas = new List<dynamic>();
                            
                            
                            var zipFilePath = Path.Combine(employeeFolder, uploadedImage.FileName);
                            uploadedImage.SaveAs(zipFilePath);

                            //var folder = Path.GetFileNameWithoutExtension(uploadedImage.FileName);
                            //var elementPath = Path.Combine(zipFilePath, folder);
                            
                            using (ZipFile zip = ZipFile.Read(zipFilePath))
                            {
                                foreach (ZipEntry entry in zip)
                                {
                                    using (MemoryStream data = new MemoryStream())
                                    {
                                        entry.Extract(data);
                                        data.Seek(0, SeekOrigin.Begin);

                                       dynamic e = new ExpandoObject();
                                        var zipFolder = Path.Combine(employeeFolder, entry.FileName);
                                        e.fileName = entry.FileName;
                                        e.filePath = zipFolder;
                                        e.uploadDateTime = DateTime.Now;
                                        zipFileDatas.Add(e);

                                        byte[] fileData = File.ReadAllBytes(entry.FileName);
                                        
                                    }
                                }
                            }
                        }
                        else
                        {
                            var picture = Path.Combine(employeeFolder, uploadedImage.FileName);
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
        
    }
}
