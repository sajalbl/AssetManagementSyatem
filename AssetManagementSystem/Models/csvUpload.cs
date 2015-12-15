using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetManagementSystem.Models
{
    public class csvUploadRequest
    {
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string CompanyName { get; set; }
        public string csvFilePath { get; set; }
        public string csvFileName { get; set; }

    }

    public class csvUploadResponse
    {

    }
}