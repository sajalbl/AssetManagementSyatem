using AssetManagementSystem.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace AssetManagementSystem.Controllers
{
    public class csvUploadAdapter
    {
        public csvUploadResponse ParseCSV(csvUploadRequest request)
        {
            csvUploadResponse response = new csvUploadResponse();
            
            List<dto> list = new List<dto>();
            dto dt = new dto();

            List<dynamic> data = new List<dynamic>();

            data.Add(parse(request));

            using (var context = new Company_dbEntities())
            {
                var comp = (from a in context.Company_table where a.CompanyName == request.CompanyName select a).FirstOrDefault<Company_table>();

                if (comp != null)
                {
                    if (request.Employee == true)
                    {
                        foreach (var entry in data[0])
                        {
                            dynamic fields = JsonConvert.SerializeObject(entry.Data);

                            dynamic item = JsonConvert.DeserializeObject(fields);

                            dt.UserName = item.UserName;
                            dt.Email = item.Email;


                            var emp = (from a in context.Employee_table where a.UserName == dt.UserName && a.Email == dt.Email select a).FirstOrDefault();

                            if (emp == null)
                            {
                                emp = new Employee_table();

                                emp.CompanyID = comp.CompanyID;
                                emp.EmployeeName = item.EmployeeName;
                                emp.Email = item.Email;
                                emp.ManagerID = item.ManagerID;
                                emp.Designation = item.Designation;
                                emp.UserName = item.UserName;
                                emp.ModifiedOn = DateTime.Now;
                                emp.IsActive = true;

                               
                                var company = (from a in context.Company_table where a.CompanyID == comp.CompanyID select a).FirstOrDefault();

                                company.EmployeeCount++;

                                context.Employee_table.Add(emp);

                                context.SaveChanges();

                                response.csvUploaded = true;
                            }
                            else
                            {
                                dto d = new dto();

                                d.UserName = item.UserName;
                                d.Email = item.Email;
                                d.EmployeeName = item.EmployeeName;
                                d.CompanyName = item.CompanyName;
                                d.Designation = item.Designation;
                                d.ManagerID = item.ManagerID;

                                list.Add(d);
                            }

                        }
                        response.Duplicate = JsonConvert.SerializeObject(list);
                        response.csvUploaded = false;

                    }

                    else
                    {
                        foreach (var entry in data[0])
                        {
                            dynamic fields = JsonConvert.SerializeObject(entry.Data);

                            dynamic item = JsonConvert.DeserializeObject(fields);

                            dt.Serial = item.Serial;

                            var res = (from a in context.Resources_table where a.Serial == dt.Serial select a).FirstOrDefault<Resources_table>();

                            if (res == null)
                            {
                                res = new Resources_table();

                                res.CompanyID = comp.CompanyID;
                                res.NameOfDevice = item.NameOfDevice;
                                //res.EmployeeID = item.UserName;
                                res.Type = item.Type;
                                res.Serial = item.Serial;
                                res.IssuedFrom = item.IssuedFrom;
                                res.ModifiedOn = DateTime.Now;
                                res.Deleted = false;
                                //res.IsActive = true;
                                var company = (from a in context.Company_table where a.CompanyID == comp.CompanyID select a).FirstOrDefault();

                                company.ResourceCount++;

                                context.Resources_table.Add(res);

                                context.SaveChanges();
                                response.csvUploaded = true;
                            }

                            else
                            {
                                dto d = new dto();

                                d.UserName = item.UserName;
                                d.NameOfDevice = item.NameOfDevice;
                                d.Type = item.Type;
                                d.Serial = item.Serial;
                                d.IssuedFrom = item.IssuedFrom;
                                d.CompanyName = request.CompanyName;

                                list.Add(d);
                            }
                        }
                        response.Duplicate = JsonConvert.SerializeObject(list);
                        response.csvUploaded = false;
                    }
                }
                else
                {
                    throw new Exception("Company Name does not Exists. Try again.");
                }

            }



            return response;
        }


        public IEnumerable<dynamic> parse(csvUploadRequest request)
        {
            // TextFieldParser is in the Microsoft.VisualBasic.FileIO namespace.
            using (TextFieldParser parser = new TextFieldParser(request.FilePath))
            {
                parser.CommentTokens = new string[] { "#" };
                parser.SetDelimiters(new string[] { "," });
                parser.HasFieldsEnclosedInQuotes = true;

                // Skip over header line.
                //parser.ReadLine();


                string[] headerFields = parser.ReadFields();
                string[] propertyNames = headerFields.Select(str => str.Replace(" ", "_")).ToArray<string>(); //(*)

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dynamic baseObject = new ExpandoObject();
                    IDictionary<string, object> baseDataUnderlyingObject = baseObject;

                    dynamic dataObject = new ExpandoObject();
                    IDictionary<string, object> dataUnderlyingObject = dataObject;

                    //dynamic searchDataObject = new ExpandoObject();
                    //IDictionary<string, object> searchDataUnderlyingObject = searchDataObject;

                    //dynamic authData = new ExpandoObject();
                    //IDictionary<string, object> authDataUnderlyingObject = authData;

                    //dynamic authSearchDataObject = new ExpandoObject();
                    //IDictionary<string, object> authSearchDataUnderlyingObject = authSearchDataObject;


                    for (int i = 0; i < headerFields.Length; i++)
                    {
                        var propName = propertyNames[i];
                        var fieldValue = fields[i];

                        dataUnderlyingObject.Add(propName, fieldValue);

                        //var authField = (from a in request.AuthFieldNames
                        //                 where a.ToUpper() == propName.ToUpper()
                        //                 select a).FirstOrDefault();

                        //if (!string.IsNullOrEmpty(authField))
                        //{
                        //    authDataUnderlyingObject.Add(propName, fieldValue);
                        //    ExtractSearchData(request, authSearchDataUnderlyingObject, propName, fieldValue);
                        //}
                        //else
                        //{
                        //    authDataUnderlyingObject.Add(propName, fieldValue);
                        //    dataUnderlyingObject.Add(propName, fieldValue);
                        //    ExtractSearchData(request, authSearchDataUnderlyingObject, propName, fieldValue);
                        //    ExtractSearchData(request, searchDataUnderlyingObject, propName, fieldValue);
                        //}
                    }

                    baseDataUnderlyingObject.Add("Data", dataObject);
                    //baseDataUnderlyingObject.Add("SearchData", searchDataObject);
                    //baseDataUnderlyingObject.Add("AuthData", authData);
                    //baseDataUnderlyingObject.Add("AuthSearchData", authSearchDataObject);
                    yield return baseObject;
                }
            }
        }


        public string DownloadCsv(csvUploadRequest request)
        {

            //csvUploadResponse response = new csvUploadResponse();

            bool exist = Directory.Exists(request.FilePath);

            if (!exist)
            {
                Directory.CreateDirectory(request.FilePath);

            }

            if (request.Employee == true)
            {
                var file = Path.Combine(request.FilePath, "Employee.csv");
                bool present = File.Exists(file);

                if (!present)
                {
                    //File.Delete(file);
                    //DownloadCsv(request);
                    File.Create(file);
                }
                return file;
            }

            else
            {
                var file = Path.Combine(request.FilePath, "Resource.csv");
                bool present = File.Exists(file);

                if (!present)
                {
                    //File.Delete(file);
                    //DownloadCsv(request);
                    File.Create(file);
                }
                return file;
            }

            //else
            //{
            //    File.Create(file);

            //}

            //var csv = Path.Combine(request.FilePath, "Employee.csv");
            //bool pres = File.Exists(csv);
            //if (!pres)
            //{
            //    File.Create(csv);
            //}
            //response.csvDowloaded = true;


        }


        public csvUploadResponse download(csvUploadRequest request, string path)
        {
            csvUploadResponse response = new csvUploadResponse();

            using (var context = new Company_dbEntities())
            {

                using (StreamWriter write = new StreamWriter(path))
                {
                    if (request.Employee == true)
                    {
                        string header = "EmployeeID,EmployeeName,CompanyID,ManagerID,Designation,Email";

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(header);

                        var CompanyID = (from a in context.Company_table where a.CompanyName == request.CompanyName select a.CompanyID).FirstOrDefault();

                        var employee = (from a in context.Employee_table where a.CompanyID == CompanyID select a).ToList<Employee_table>();

                        if (employee != null)
                        {

                            foreach (var entry in employee)
                            {
                                sb.AppendLine(string.Join(",",
                                    string.Format(entry.UserName),
                                    string.Format(entry.EmployeeName),
                                    string.Format(entry.CompanyID.ToString()),
                                    string.Format(entry.ManagerID),
                                    string.Format(entry.Designation),
                                    string.Format(entry.Email)));

                            }

                            write.WriteLine(sb.ToString());

                            write.Close();
                            //HttpContext content = HttpContext.Current;
                            //content.Response.Write(sb.ToString());
                            //content.Response.ContentType = "text/csv";
                            //content.Response.AddHeader("Content-Disposition", "attachment; filename=EmployeeCSV.csv");
                            //content.Response.End();

                            response.csvDowloaded = true;
                        }

                        else
                        {
                            response.csvDowloaded = false;
                        }
                    }

                    else
                    {
                        string header = "NameOfDevice,CompanyID,Type,Serial,Deleted";

                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine(header);

                        var CompanyID = (from a in context.Company_table where a.CompanyName == request.CompanyName select a.CompanyID).FirstOrDefault();

                        var resource = (from a in context.Resources_table where a.CompanyID == CompanyID select a).ToList<Resources_table>();

                        if (resource != null)
                        {

                            foreach (var entry in resource)
                            {
                                sb.AppendLine(string.Join(",",
                                    string.Format(entry.NameOfDevice),
                                    string.Format(entry.CompanyID.ToString()),
                                    string.Format(entry.Type),
                                    string.Format(entry.Serial),
                                    string.Format(entry.Deleted.ToString())));

                            }

                            write.WriteLine(sb.ToString());

                            write.Close();
                            //HttpContext content = HttpContext.Current;
                            //content.Response.Write(sb.ToString());
                            //content.Response.ContentType = "text/csv";
                            //content.Response.AddHeader("Content-Disposition", "attachment; filename=EmployeeCSV.csv");
                            //content.Response.End();

                            response.csvDowloaded = true;
                        }

                        else
                        {
                            response.csvDowloaded = false;
                        }
                    }
                }

            }
            return response;
        }

        //private static void ExtractSearchData(UpdateListSearchRequest request, IDictionary<string, object> searchDataObject, string propName, string fieldValue)
        //{
        //    var searchField = (from a in request.SearchFieldNames
        //                       where a.ToUpper() == propName.ToUpper()
        //                       select a).FirstOrDefault();
        //    if (!string.IsNullOrEmpty(searchField))
        //    {
        //        searchDataObject.Add(propName, fieldValue);
        //    }
        //}
    }

    public class dto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string ManagerID { get; set; }
        public string Serial { get; set; }
        public string NameOfDevice { get; set; }
        public string Type { get; set; }
        public string IssuedFrom { get; set; }


    }

}