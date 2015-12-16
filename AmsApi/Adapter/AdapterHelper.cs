using AmsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Adapter
{
    public class AdapterHelper
    {
        public static int? GetCompanyId(string companyName)
        {
            int? compId = null;
            using (var context = new Company_dbEntities())
            {
                var company = (from a in context.Company_table
                               where a.CompanyName.ToLower() == companyName.ToLower()
                               select a).FirstOrDefault();

                if (company != null)
                    compId = company.CompanyID;
            }
            return compId;
        }

        public static int? GetEmployeeId(string employeeName, string email)
        {
            int? empId = null;
            using (var context = new Company_dbEntities())
            {
                var employee = (from a in context.Employee_table
                                where a.EmployeeName.ToLower() == employeeName.ToLower() && a.Email.ToLower() == email.ToLower()
                                select a).FirstOrDefault();

                if (employee != null)
                    empId = employee.EmployeeID;
            }
            return empId;
        }

    }
}