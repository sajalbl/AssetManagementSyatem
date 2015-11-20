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
        public string AssignedBy { get; set; }
        public string EmployeeConfirm { get; set; }
        public string ManagerConfirm { get; set; }
        public bool Accept { get; set; }

    }

    public class ManageTaskResponse
    {
        public bool TaskAdded { get; set; }
        public string TaskList { get; set; }
        public string TaskAssign { get; set; }
        public bool ConfirmEmployee { get; set; }
        public bool ConfirmManager { get; set; }
        public bool TaskDeleted { get; set; }

    }
}