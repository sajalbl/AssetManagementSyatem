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
          
            using (var context = new Company_dbEntities())
            {

                var resp = (from a in context.Resources_table where request.Serial == a.Serial select a).FirstOrDefault<Resources_table>();

                if (resp == null)
                {
                    resp = new Resources_table();
                    resp.NameOfDevice = request.NameOfDevice;
                    resp.Type = request.Type;

                    resp.IssuedFrom = request.IssuedFrom.ToShortDateString();
                    resp.Serial = request.Serial;

                    context.Resources_table.Add(resp);
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

            List<Resources_table> resource = new List<Resources_table>();
            using (var context = new Company_dbEntities())
            {

                resource = (from a in context.Resources_table select a).ToList<Resources_table>();
                
                response.ResourcesList = JsonConvert.SerializeObject(resource);

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
                    comp.IssuedTo = request.IssuedTo.ToShortDateString();
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
            //using (var context = new Company_dbEntities())
            //{

            //    resource = (from a in context.Resources_table  request.NameOfDevice == a.NameOfDevice && request.Type == a.Type && request.Serial == a.Serial select a).FirstOrDefault<Resources_table>();

            //    context.Resources_table.Remove(resource);

            //    context.SaveChanges();

            //    response.ResourceDeleted = true;
            //}

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

        public ManageResourcesResponse ResourceCount(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
           
            using (var context = new Company_dbEntities())
            {

               var resource = (from a in context.Resources_table  select a).Count();

               var company = (from a in context.Company_table where request.CompanyName == a.CompanyName select a).FirstOrDefault<Company_table>();

               company.ResourceCount = resource.ToString();
                               
               context.SaveChanges();
               
               response.Resourcecount = resource;

            }
            return response;
        }

        public ManageResourcesResponse ResourceAllocated(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();

            List<Resources_table> resource = new List<Resources_table>();
            using (var context = new Company_dbEntities())
            {

               // resource = (from a in context.Resources_table where request.EmployeeID == a.EmployeeID select a).ToList<Resources_table>();

                response.ResourcesAllocated = JsonConvert.SerializeObject(resource);

            }
            return response;
        }

             
        public ManageResourcesResponse ShowImage(ManageResourcesRequest request)
        {
            ManageResourcesResponse response = new ManageResourcesResponse();
            using (var context = new Company_dbEntities())
            {

                var image = (from a in context.Resources_table where a.Serial == request.Serial select a).FirstOrDefault<Resources_table>();


                response.resourceImage = image.Picture;
            }
            return response;
        }


       
    }
}