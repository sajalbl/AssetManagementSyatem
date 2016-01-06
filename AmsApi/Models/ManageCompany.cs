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
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Prefix { get; set; }
    }

    public class ManageCompanyResponse
    {
        public bool IsCompanyCreated { get; set; }
        public string CompanyList { get; set; }
        public string CompanyInfo { get; set; }
        public bool IsCompanyUpdated { get; set; }
        public bool IsCompanyExist { get; set; }
        public int CompanyId { get; set; }
        public string ManagerList { get; set; }
        //public bool EditLinkShow { get; set; }
        public string EmployeeList { get; set; }
    }
}