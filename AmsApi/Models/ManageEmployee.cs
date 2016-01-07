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
        public DateTime? DOB { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string ManagerID { get; set; }
        public string Picture { get; set; }
        public string EmployeeInfo { get; set; }
        public string UserName { get; set; }
        public int CompanyID { get; set; }
        //public bool Resource { get; set; }

    }

    public class ManageEmployeeResponse
    {
        //public ManageEmployeeResponse()
        //{
        //    EmployeeList = new List<dynamic>();
        //}
        public bool IsEmployeeCreated { get; set; }

        public bool IsEmployeeExist { get; set; }

        public int EmployeeCount { get; set; }

        public string EmployeeList { get; set; }

        public bool IsManager { get; set; }

        public bool Edit { get; set; }

        public string EmployeeInfo { get; set; }

        public string ManagerEmployees { get; set; }
        public bool IsEmployeeUpdated { get; set; }
        public string EmployeeID { get; set; }
        public bool IsEmployeeReplaced { get; set; }
    }
}