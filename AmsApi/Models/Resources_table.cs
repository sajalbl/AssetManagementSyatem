//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmsApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resources_table
    {
        public int ResourceId { get; set; }
        public string NameOfDevice { get; set; }
        public string Type { get; set; }
        public string IssuedTo { get; set; }
        public string IssuedFrom { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string IsActive { get; set; }
        public string EmployeeID { get; set; }
        public string CompanyName { get; set; }
        public string Serial { get; set; }
        public string Picture { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
