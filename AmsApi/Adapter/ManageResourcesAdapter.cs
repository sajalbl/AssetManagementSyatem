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
using AmsApi.Resources.Models;

namespace AmsApi.Adapter
{
    public class ManageResourcesAdapter
    {

        public ManageResourcesResponse AddResources(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
            if (compId == null)
            {
                throw new Exception("No company found. Try again!");
            }
          
            using (var context = new Company_dbEntities())
            {

                var resp = (from a in context.Resources_table where a.CompanyID == compId.Value && request.Serial == a.Serial select a).FirstOrDefault<Resources_table>();

                if (resp == null)
                {
                    resp = new Resources_table();
                    resp.NameOfDevice = request.NameOfDevice;
                    resp.Type = request.Type;
                    resp.IssuedFrom = request.IssuedFrom.ToShortDateString();
                    resp.Serial = request.Serial;
                    resp.CompanyID = compId.Value;
                    resp.EmployeeID = null;
                    resp.Deleted = false;

                    context.Resources_table.Add(resp);


                    var comp = (from a in context.Company_table where a.CompanyID == compId.Value select a).FirstOrDefault<Company_table>();

                    if (comp != null)
                        comp.ResourceCount++;

                    context.SaveChanges();
                    response.IsResourcesCreated = true;
                }
                else
                {
                    response.IsResourcesCreated = false;
                }
               
            }

            return response;
        }

        public ManageResourcesResponse ShowResources(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
             int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
            if (compId == null)
            {
                throw new Exception("No company found. Try again!");
            }

            List<resourceDTO> res = new List<resourceDTO>();
            List<Resources_table> resource = new List<Resources_table>();
            using (var context = new Company_dbEntities())
            {

                 resource = (from a in context.Resources_table where a.CompanyID == compId.Value select a).ToList();

                if (resource != null) 
                { 
                    foreach(var entry in resource)
                    { 
                            resourceDTO r = new resourceDTO();
                            r.NameOfDevice = entry.NameOfDevice;
                            r.Type = entry.Type;
                            r.IssuedFrom = entry.IssuedFrom.ToString();
                            r.EmployeeID = entry.EmployeeID;
                            r.Serial = entry.Serial;
                            r.CompanyID = entry.CompanyID;
                            r.Deleted = entry.Deleted;
                            r.Picture = entry.Picture;
                            res.Add(r);
                    }
                
                }
                response.ResourcesList = JsonConvert.SerializeObject(res);
            }
            return response;
        }

        public ManageResourcesResponse UpdateResources(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            using (var context = new Company_dbEntities())
            {

                var comp = (from a in context.Resources_table where  a.NameOfDevice == request.NameOfDevice && a.Serial == request.Serial  select a).FirstOrDefault<Resources_table>();

                if (comp != null)
                {
                    //comp = new Resources_table();
                    comp.Type = request.Type;
                    //comp.IssuedTo = request.IssuedTo.ToShortDateString();
                    comp.IssuedFrom = request.IssuedFrom.ToShortDateString();
                    //comp.Picture = request.Picture;
                }

                context.SaveChanges();
                response.IsResourcesUpdated = true;

            }

            return response;
        }

        public ManageResourcesResponse DeleteResources(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            //Resources_table resource = new Resources_table();
            using (var context = new Company_dbEntities())
            {

               var resource = (from a in context.Resources_table where request.CompanyID == a.CompanyID && request.Serial == a.Serial select a).FirstOrDefault();

                if(resource != null)
                {
                    if(request.Deleted == true)
                    {
                        resource.Deleted = false;
                        var comp = (from a in context.Company_table where a.CompanyID == request.CompanyID select a).FirstOrDefault<Company_table>();

                        if (comp != null)
                            comp.ResourceCount++;
                    }

                    else
                    {
                        resource.Deleted = true;
                        resource.EmployeeID = null;
                        var comp = (from a in context.Company_table where a.CompanyID == request.CompanyID select a).FirstOrDefault<Company_table>();

                        if (comp != null)
                            comp.ResourceCount--;
                        //context.Resources_table.Remove(resource);
                    }
                }
                
                
                context.SaveChanges();

                response.ResourceDeleted = true;
            }

            return response;
        }

        public ManageResourcesResponse CompanyDeleted(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            Resources_table resource = new Resources_table();
            using (var context = new Company_dbEntities())
            {

                resource = (from a in context.Resources_table  select a).FirstOrDefault<Resources_table>();

                if (resource != null)
                {
                    context.Resources_table.Remove(resource);

                    context.SaveChanges();

                    CompanyDeleted(request);
                } 
                
                response.DeletedCompany = true;
            }

            return response;
        }

        //public ManageResourcesResponse ResourceCount(ManageResourcesRequest request)
        //{
        //    ManageResourcesResponse response = new ManageResourcesResponse();
           
        //    using (var context = new Company_dbEntities())
        //    {

        //       var resource = (from a in context.Resources_table  select a).Count();

        //       var company = (from a in context.Company_table where request.CompanyName == a.CompanyName select a).FirstOrDefault<Company_table>();

        //       company.ResourceCount = resource;
                               
        //       context.SaveChanges();
               
        //       response.Resourcecount = resource;

        //    }
        //    return response;
        //}

        public ManageResourcesResponse ReplaceResource(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
            if (compId == null)
            {
                throw new Exception("No company found. Try again!");
            }

            using (var context = new Company_dbEntities())
            {


                var res = (from a in context.Resources_table where a.Serial == request.Serial select a).FirstOrDefault();

                res.Serial = request.Serial;
                res.NameOfDevice = request.NameOfDevice;
                res.Type = request.Type;
                res.EmployeeID = request.UserName;
                res.CompanyID = compId.Value;
                res.IssuedFrom = request.IssuedFrom.ToString();
                res.Deleted = false;
                res.ModifiedOn = DateTime.Now;

                //var comp = (from a in context.Company_table
                //            where a.CompanyName == request.CompanyName
                //            select a).FirstOrDefault();

                //if (comp != null)
                //    comp.EmployeeCount++;

                //emp.CompanyID = compId.Value;

                context.SaveChanges();
                response.IsResourceReplaced = true;
            }
            return response;
        }

        public ManageResourcesResponse ResourceAllocated(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
            //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");

            List<resourceDTO> res = new List<resourceDTO>();
            List<Resources_table> resource = new List<Resources_table>();
            using (var context = new Company_dbEntities())
            {

                resource = (from a in context.Resources_table where a.EmployeeID == request.UserName && a.Deleted == false select a).ToList();

                if (resource != null)
                {
                    foreach (var entry in resource)
                    {
                        resourceDTO r = new resourceDTO();
                        r.NameOfDevice = entry.NameOfDevice;
                        r.Type = entry.Type;
                        r.IssuedTo = entry.IssuedTo;
                       // r.IssuedFrom = entry.IssuedFrom.ToString();
                        
                        res.Add(r);
                    }

                }
                response.ResourcesAllocated = JsonConvert.SerializeObject(res);
            }
            return response;
        }

             
        public ManageResourcesResponse ShowImage(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
            using (var context = new Company_dbEntities())
            {

                var image = (from a in context.Resources_table where a.Serial == request.Serial select a).FirstOrDefault();

                if(image != null)
                { 
                resourceDTO r = new resourceDTO();

                r.Picture = image.Picture;
                response.resourceImage = r.Picture;
                }
               
            }
            return response;
        }

        public ManageResourcesResponse Allocate(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
            using (var context = new Company_dbEntities())
            {

                var alloc = (from a in context.Resources_table where a.Serial == request.Serial select a).FirstOrDefault();

                if (alloc != null)
                {
                    if(request.Allocate == true)
                    {
                        alloc.EmployeeID = request.UserName;
                        alloc.IssuedTo = DateTime.Now.ToString();
                        context.SaveChanges();
                        response.allocated = true;
                    }

                    else
                    {
                        alloc.EmployeeID = null;
                        alloc.IssuedTo = DateTime.Now.ToString();
                        context.SaveChanges();
                        response.allocated = false;
                    }
                    
                    
                }

            }
            return response;
        }


       
    }

    public class resourceDTO
    {
        public string CompanyName { get; set; }
        public string NameOfDevice { get; set; }
        public string Type { get; set; }
        public string IssuedTo { get; set; }
        public string IssuedFrom { get; set; }
        public string EmployeeID { get; set; }
        public string Serial { get; set; }
        public string Picture { get; set; }
        public int CompanyID { get; set; }
        public bool? Deleted { get; set; }
    }
}