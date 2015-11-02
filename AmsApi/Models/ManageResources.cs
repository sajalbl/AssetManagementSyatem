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
        public DateTime IssuedOn { get; set; }
        public DateTime IssuedFrom { get; set; }
       
        }

    public class ManageResourcesResponse
    {
        public bool IsResourcesCreated { get; set; }

        public string ResourcesList { get; set; }

        public bool IsResourcesUpdated { get; set; }

        
    }
}
    
