using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace AmsApi.Adapter
{
    public class NewCompanyAdapter
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
                comp.ResourceCount = request.Resources.ToString();
                comp.Address = request.Address;
                comp.Contact = request.Contact;
                comp.Email = request.Email;

                if (isNew)
                {
                    context.Company_table.Add(comp);
                }
                
                context.SaveChanges();
                response.IsCompanyCreated = true;

            }

            return response;
        }
    }

}