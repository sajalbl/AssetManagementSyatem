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
using System.Data; 
namespace AmsApi.Adapter
{  
    public class DeleteCompanyAdapter
    {

          
        public ManageCompanyResponse deleteCompany(ManageCompanyRequest request)
        {
            ManageCompanyResponse response = new ManageCompanyResponse();

           Company_table company = new Company_table();
            using (var context = new Company_dbEntities())
            {

                company = (from a in context.Company_table where request.CompanyName == a.CompanyName && request.OwnerName == a.OwnerName select a).FirstOrDefault<Company_table>();
               
                context.Company_table.Remove(company);
                
                context.SaveChanges();
                
            }
        
            return response;
        }

    }
}