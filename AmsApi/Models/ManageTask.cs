using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Models
{
    public class ManageTaskRequest
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }

    }

    public class ManageTaskResponse
    {
        public bool TaskAdded { get; set; }
        public string TaskList { get; set; }
    }
}