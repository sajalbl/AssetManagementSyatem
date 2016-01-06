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
        public string IssuedTo { get; set; }
        public DateTime IssuedFrom { get; set; }
        public string UserName { get; set; }
        public string Serial { get; set; }
        public string Picture { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public bool Allocate { get; set; }
        public int CompanyID { get; set; }
        public bool Deleted { get; set; }
       
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

        public string resourceImage { get; set; }

        public bool allocated { get; set; }

    }
}
    
