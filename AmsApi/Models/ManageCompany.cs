using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Models
{
    public class ManageCompanyRequest
    {
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public int Resources { get; set; }
        public string Address { get; set; }
        public string Contact{ get; set; }
        public string Email { get; set; }
        }

    public class ManageCompanyResponse
    {
        public bool IsCompanyCreated { get; set; }

        public string CompanyList { get; set; }

        public bool IsCompanyUpdated { get; set; }

        public bool IsCompanyExist { get; set; }

        //public bool EditLinkShow { get; set; }

        
    }
}