using AmsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Adapter
{
    public class AllCompanyAdapter
    {
        public ManageCompanyResponse allCompany()
        {
            ManageCompanyResponse response = new ManageCompanyResponse();
            List<Company_table> company = new List<Company_table>();
            using (var context = new Company_dbEntities())
            {
                company = (from a in context.Company_table select a).ToList<Company_table>();
                response.CompanyList = JsonConvert.SerializeObject(company);
            }
            return response;
        }
    }
}