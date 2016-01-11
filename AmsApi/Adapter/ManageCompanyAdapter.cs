using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
namespace AmsApi.Adapter
{
    public class ManageCompanyAdapter
    {
        public ManageCompanyResponse allCompany()
        {
            ManageCompanyResponse response = new ManageCompanyResponse();
            List<Company_table> company = new List<Company_table>();
            using (var context = new Company_dbEntities())
            {
                company = (from a in context.Company_table select a).ToList<Company_table>();

                if (company.Count() > 0)
                    response.CompanyList = JsonConvert.SerializeObject(company);
                else
                    response.CompanyList = null;
            }
            return response;
        }

        public ManageCompanyResponse AddCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId != null)
                {
                    throw new Exception("Company Name already Exists. Try another name.");
                }

                var comp = (from a in context.Company_table where a.Prefix == request.Prefix select a).FirstOrDefault();
                if(comp == null)
                { 
                 comp = new Company_table();
                comp.CompanyName = request.CompanyName;
                comp.OwnerName = request.OwnerName;
                comp.Address = request.Address;
                comp.Contact = request.Contact;
                comp.EmployeeCount = 0;
                comp.ResourceCount = 0;
                comp.Email = request.Email;
                comp.Prefix = request.Prefix;
                comp.ModifiedOn = DateTime.Now;
                comp.IsActive = true;

                context.Company_table.Add(comp);
                context.SaveChanges();

                response.IsCompanyCreated = true;
                response.CompanyId = comp.CompanyID;
                }

                else
                {
                    response.IsCompanyCreated = false;
                }
            }
            return response;
        }

        public ManageCompanyResponse SearchCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId == null)
                {
                    throw new Exception("No company found. Try again!");
                }

                var company = (from a in context.Company_table
                               where a.CompanyID == compId
                               select a).FirstOrDefault();

                //Whenever selecting the first or default, always check for null
                if (company != null)
                {
                    response.IsCompanyExist = true;

                    dynamic comp = new ExpandoObject();
                    comp.CompanyName = company.CompanyName;
                    comp.CompanyId = company.CompanyID;
                    comp.Address = company.Address;
                    comp.Contact = company.Contact;
                    comp.Email = company.Email;
                    comp.EmployeeCount = company.EmployeeCount;
                    comp.ResourceCount = company.ResourceCount;
                    comp.OwnerName = company.OwnerName;
                    comp.Prefix = company.Prefix;

                    response.CompanyInfo = JsonConvert.SerializeObject(comp);
                }
            }
            return response;
        }

        public ManageCompanyResponse UpdateCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId == null)
                {
                    throw new Exception("No company found. Try again!");
                }
                var comp = (from a in context.Company_table
                            where a.CompanyID == compId
                            select a).FirstOrDefault();
                if (comp != null)
                {
                    //comp = new Company_table();
                    // comp.ResourceCount = request.Resources.ToString();
                    comp.Address = request.Address;
                    comp.Contact = request.Contact;
                    comp.Email = request.Email;
                    //comp.EmployeeCount = 0;
                    //comp.ResourceCount = 0;
                    
                }

                // this method does not update any information about the employee or the resources of the company!
                context.SaveChanges();
                response.IsCompanyUpdated = true;
            }
            return response;
        }

        public ManageCompanyResponse deleteCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            //creating a new instance is not required
            //Company_table company = new Company_table();

            //calling the constructor would suffice
            Company_table company = default(Company_table);

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId == null)
                {
                    throw new Exception("No company found. Try again!");
                }

                company = (from a in context.Company_table
                           where a.CompanyID == compId
                           select a).FirstOrDefault();

                if (company != null)
                {
                    context.Company_table.Remove(company);
                    response.IsCompanyUpdated = true;
                }
                context.SaveChanges();
            }
            return response;
        }

        public ManageCompanyResponse allManagers(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId == null)
                    throw new Exception("No company found. Try again!");

                //  List<Employee_table> managers = new List<Employee_table>();

                //Add FK bw employee and comapny table using company Id, redo LINQ for this- DONE!
                var managers = (from a in context.Employee_table
                                where a.CompanyID == compId && a.Designation == "Manager"
                                select new
                                {
                                    EmployeeName = a.EmployeeName,
                                    EmployeeID = a.UserName,
                                    CompanyID = a.CompanyID,
                                    ManagerID = a.ManagerID,
                                    Designation = a.Designation,
                                    //Department = a.Department,
                                    //DOB = a.DOB,
                                    //Address = a.Address,
                                   // Contact = a.Contact,
                                    Email = a.Email
                                }).ToList();

                //dynamic manList = new ExpandoObject();
                //manList.Managers = managers;

                response.ManagerList = JsonConvert.SerializeObject(managers);
            }
            return response;
        }

        // this works the same as search company...

        //public ManageCompanyResponse CheckCompany(ManageCompanyRequest request)
        //{
        //    ManageCompanyResponse response = new ManageCompanyResponse();

        //    //Company_table company = new Company_table();
        //    using (var context = new Company_dbEntities())
        //    {

        //        var company = (from a in context.Company_table where request.CompanyName == a.CompanyName && request.OwnerName == a.OwnerName select a).FirstOrDefault<Company_table>();
        //        if (company != null)
        //        {
        //            response.IsCompanyExist = true;
        //        }
        //        else
        //        {
        //            response.IsCompanyExist = false;
        //        }

        //    }
        //    return response;
        //}


        public ManageCompanyResponse allEmployee(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (compId == null)
                    throw new Exception("No company found. Try again!");

                //  List<Employee_table> managers = new List<Employee_table>();
                List<employee> emp = new List<employee>();
                //Add FK bw employee and comapny table using company Id, redo LINQ for this- DONE!
                var emplo = (from a in context.Employee_table
                                where a.CompanyID == compId
                                select a).ToList();

                foreach(var entry in emplo)
                {
                    employee e = new employee();

                    e.UserName = entry.UserName;

                    emp.Add(e);
                }
                
                //dynamic manList = new ExpandoObject();
                //manList.Managers = managers;

                response.EmployeeList = JsonConvert.SerializeObject(emp);
            }
            return response;
        }

        
    }

    public class employee
    {
        public string UserName { get; set; }
    }

}