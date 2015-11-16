using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Models
{
    public class ManageEmployeeRequest
    {
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }

    }

    public class ManageEmployeeResponse
    {
        public bool IsEmployeeCreated { get; set; }

        public bool IsEmployeeExist { get; set; }

        public int EmployeeCount { get; set; }

        public string EmployeeList { get; set; }

        public bool IsManager { get; set; }
    }
}