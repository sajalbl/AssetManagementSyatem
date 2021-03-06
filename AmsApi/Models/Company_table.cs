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
    
    public partial class Company_table
    {
        public Company_table()
        {
            this.Employee_table = new HashSet<Employee_table>();
            this.Resources_table = new HashSet<Resources_table>();
        }
    
        public int CompanyID { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string OwnerName { get; set; }
        public Nullable<int> ResourceCount { get; set; }
        public Nullable<int> EmployeeCount { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Prefix { get; set; }
    
        public virtual ICollection<Employee_table> Employee_table { get; set; }
        public virtual ICollection<Resources_table> Resources_table { get; set; }
    }
}
