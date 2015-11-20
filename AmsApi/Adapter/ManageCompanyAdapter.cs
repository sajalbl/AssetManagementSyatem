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
namespace AmsApi.Adapter
{
    public class ManageCompanyAdapter
    {

        public ManageCompanyResponse AddCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {

                var comp = (from a in context.Company_table where a.CompanyName == request.CompanyName && a.OwnerName == request.OwnerName select a).FirstOrDefault<Company_table>();

                var isNew = false;

                if (comp == null)
                {
                    isNew = true;
                    comp = new Company_table();

                }

                comp.CompanyName = request.CompanyName;
                comp.OwnerName = request.OwnerName;
                comp.Address = request.Address;
                comp.Contact = request.Contact;
                comp.Email = request.Email;

                if (isNew)
                {
                    context.Company_table.Add(comp);
                    context.SaveChanges();
                    response.IsCompanyCreated = true;
                }

                else { 
                context.SaveChanges();
                response.IsCompanyCreated = false;
                }
            }

            return response;
        }

        public ManageCompanyResponse SearchCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();
            
             List<Company_table> company = new List<Company_table>();
             using (var context = new Company_dbEntities())
             {

                 company = (from a in context.Company_table where request.CompanyName == a.CompanyName && request.OwnerName == a.OwnerName select a).ToList<Company_table>();
                 response.CompanyList = JsonConvert.SerializeObject(company);
                 
             }
             return response;
        }

        public ManageCompanyResponse UpdateCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            using (var context = new Company_dbEntities())
            {

                var comp = (from a in context.Company_table where a.CompanyName == request.CompanyName && a.OwnerName == request.OwnerName select a).FirstOrDefault<Company_table>();

                if(comp != null)
                {
                    //comp = new Company_table();
                   // comp.ResourceCount = request.Resources.ToString();
                    comp.Address = request.Address;
                    comp.Contact = request.Contact;
                    comp.Email = request.Email;
                }
              
                context.SaveChanges();
                response.IsCompanyUpdated = true;

            }

            return response;
        }

        public ManageCompanyResponse CheckCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

            //Company_table company = new Company_table();
            using (var context = new Company_dbEntities())
            {

               var company = (from a in context.Company_table where request.CompanyName == a.CompanyName && request.OwnerName == a.OwnerName select a).FirstOrDefault<Company_table>();
                if(company != null)
                { 
                response.IsCompanyExist = true;
                }
                else
                {
                    response.IsCompanyExist = false;
                }

            }
            return response;
        }

        //public ManageCompanyResponse EditLink(ManageCompanyRequest request)
        //{
        //    ManageCompanyResponse response = new ManageCompanyResponse();
            
        //    using (var context = new Company_dbEntities())
        //    {

        //       var company = (from a in context.Company_table where request.CompanyName == a.CompanyName select a).FirstOrDefault<Company_table>();
        //       response.EditLinkShow = true;

        //    }
        //    return response;
        //}

        //public ManageCompanyResponse UpdateResources(ManageCompanyRequest Req)
        //{
        //    ManageCompanyResponse response = new ManageCompanyResponse();
        //    using(var context = new Company_dbEntities())
        //    {
        //        var company = (from a in context.Company_table where a.CompanyName == Req.CompanyName select a).FirstOrDefault<Company_table>();

        //        company.ResourceCount = Req.Resources.ToString();

        //    }
        //    return response;
            
        //}
    }

}