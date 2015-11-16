using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Resources.Models
{
         public class ManageResourcesRequest
    {
        public string CompanyName { get; set; }
        public string NameOfDevice { get; set; }
        public string Type { get; set; }
        public DateTime IssuedTo { get; set; }
        public DateTime IssuedFrom { get; set; }
        public string EmployeeID { get; set; }

       
        }

    public class ManageResourcesResponse
    {
        public bool IsResourcesCreated { get; set; }

        public string ResourcesList { get; set; }

        public bool IsResourcesUpdated { get; set; }

        public bool ResourceDeleted { get; set; }

        public bool DeletedCompany { get; set; }

        public int Resourcecount { get; set; }

        public string ResourcesAllocated { get; set; }

    }
}
    
