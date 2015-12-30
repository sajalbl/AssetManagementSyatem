using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementSystem.Models
{
    public class csvUploadRequest
    {
        public string CompanyName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool Employee { get; set; }
        //public string EmployeeName { get; set; }
        //public string EmployeeID { get; set; }
        //public string Department { get; set; }
        //public string Designation { get; set; }
        //public DateTime? DOB { get; set; }
        //public string Address { get; set; }
        //public string Contact { get; set; }
        //public string Email { get; set; }
        //public string ManagerID { get; set; }
        //public string Picture { get; set; }
        //public string EmployeeInfo { get; set; }
       

    }

    public class csvUploadResponse
    {
        public bool csvUploaded { get; set; }

        public bool csvDowloaded { get; set; }
    }
}