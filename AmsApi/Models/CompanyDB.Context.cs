﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Company_dbEntities : DbContext
    {
        public Company_dbEntities()
            : base("name=Company_dbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company_table> Company_table { get; set; }
        public virtual DbSet<Employee_table> Employee_table { get; set; }
        public virtual DbSet<Resources_table> Resources_table { get; set; }
        public virtual DbSet<Task_table> Task_table { get; set; }
    }
}
