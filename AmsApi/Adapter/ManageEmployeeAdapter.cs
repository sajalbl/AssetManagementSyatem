using AmsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
namespace AmsApi.Adapter
{
    public class ManageEmployeeAdapter
    {
        public ManageEmployeeResponse AddEmployee(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (!compId.HasValue)
                    throw new Exception("No company found. Try again!");

                //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                //if (empId.HasValue)
                //    throw new Exception("Employee already exists.");

                var employee = (from a in context.Employee_table where a.Email == request.Email select a).FirstOrDefault();

               if (employee == null)
               {
                   employee = new Employee_table();

                   employee.EmployeeName = request.EmployeeName.ToLower();
                   employee.CompanyID = compId.Value;

                   employee.Email = request.Email.ToLower();

                   employee.Designation = request.Designation;
                   employee.ManagerID = request.ManagerID;
                   //employee.Contact = request.Contact;
                   employee.ModifiedOn = DateTime.Now;
                   employee.IsActive = true;
                   employee.EmployeeInfo = request.EmployeeInfo;

                   //employeeInfo info = new employeeInfo();

                   //info.Address = request.Address;
                   //info.Contact = request.Contact;
                   //info.Department = request.Department;
                   //info.DOB = request.DOB;

                   //employee.EmployeeInfo = JsonConvert.SerializeObject(info);

                   context.Employee_table.Add(employee);


                   //Updating the count of employees on successfulkl addition of employee.
                   var comp = (from a in context.Company_table
                               where a.CompanyID == compId.Value
                               select a).FirstOrDefault();

                   if (comp != null)
                       comp.EmployeeCount++;

                   context.SaveChanges();

                   //var prefix = "BL_";

                   var emp = (from a in context.Employee_table where a.EmployeeName == request.EmployeeName.ToLower() && a.Email == request.Email select a).FirstOrDefault();

                   var ID = comp.Prefix + emp.EmployeeID;

                   emp.UserName = ID;

                   context.SaveChanges();

                   response.EmployeeID = emp.UserName;
               }
               else
               {
                   response.EmployeeID = null;
               }
            }
            return response;
        }

        public ManageEmployeeResponse EmployeeDetail(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();
            //List<Employee_table> employee = new List<Employee_table>();

            using (var context = new Company_dbEntities())
            {
                if(request.CompanyName != null)
                {
                    int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                    if (compId == null)
                    {
                        throw new Exception("No company found. Try again!");
                    }

                    var employee = (from a in context.Employee_table
                                    where a.CompanyID == compId.Value
                                    select a).ToList();

                    
                    List<dto> dt = new List<dto>();

                    if (employee != null)
                    {
                        //response.IsEmployeeExist = true;

                        foreach(var entry in employee)
                        { 

                        dto d = new dto();

                        d.EmployeeName = entry.EmployeeName;
                        d.EmployeeID = entry.UserName;
                        d.Email = entry.Email;
                        d.Designation = entry.Designation;
                        //emp.DOB = employee.DOB;
                        //emp.Department = employee.Department;
                        d.ManagerID = entry.ManagerID;
                        d.CompanyID = entry.CompanyID;
                       // emp.CompanyId = employee.CompanyID;
                        //emp.Address = employee.Address;
                        //emp.Contact = employee.Contact;

                        var emp = (from a in context.Employee_table where a.UserName == entry.UserName select a).Count();

                            if(emp > 1)
                            {
                                d.count = 1;
                            }
                            else
                            {
                                d.count = 0;
                            }
                        //var comp = (from a in context.Company_table where a.CompanyID == compId select a.Prefix).FirstOrDefault();

                        //d.EmployeeID = comp + entry.EmployeeID;

                        dt.Add(d);
                        }

                        response.EmployeeInfo = JsonConvert.SerializeObject(dt);
                    }

                }
                else
                {
                    //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                    //if (!empId.HasValue)
                    //    throw new Exception("Employee does not exist!");

                    var employee = (from a in context.Employee_table
                                    where a.UserName == request.UserName && a.Email == request.Email
                                    select a).FirstOrDefault();

                    if (employee != null)
                    {
                        response.IsEmployeeExist = true;

                        dynamic emp = new ExpandoObject();
                        emp.EmployeeName = employee.EmployeeName;
                        emp.UserName = employee.UserName;
                        emp.Email = employee.Email;
                        emp.Designation = employee.Designation;
                        //emp.DOB = employee.DOB;
                        //emp.Department = employee.Department;
                        emp.ManagerId = employee.ManagerID;
                        emp.CompanyId = employee.CompanyID;
                        //emp.Address = employee.Address;
                        //emp.Contact = employee.Contact;
                        emp.EmployeeInfo = employee.EmployeeInfo;

                        response.EmployeeInfo = JsonConvert.SerializeObject(emp);
                    }
                }
               
            }
            return response;
        }

        //public ManageEmployeeResponse checkEmployee(ManageEmployeeRequest request)
        //{
        //    ManageEmployeeResponse response = new ManageEmployeeResponse();

        //    using (var context = new Company_dbEntities())
        //    {
        //        int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
        //        if (!empId.HasValue)
        //            throw new Exception("Employee does not exist!");


        //        var employee = (from a in context.Employee_table
        //                        where a.EmployeeID == empId.Value
        //                        select a).FirstOrDefault();

        //        if (employee != null)
        //        {
        //            response.IsEmployeeExist = true;
        //            response.EmployeeDetail = 
        //        }


        //        var employee = (from a in context.Employee_table where a.EmployeeName == request.EmployeeName select a).FirstOrDefault<Employee_table>();

        //        if (employee != null)
        //        {
        //            response.IsEmployeeExist = true;
        //        }
        //        else
        //        {
        //            response.IsEmployeeExist = false;
        //        }
        //    }
        //    return response;
        //}

        //public ManageEmployeeResponse countEmployee(ManageEmployeeRequest request)
        //{
        //    ManageEmployeeResponse response = new ManageEmployeeResponse();

        //    using (var context = new Company_dbEntities())
        //    {
        //        int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
        //        if (!compId.HasValue)
        //            throw new Exception("No company found. Try again!");

        //        var employeeCount = (from a in context.Employee_table where a.CompanyID == compId.Value select a).Count();

        //        var company = (from a in context.Company_table where request.CompanyName == a.CompanyName select a).FirstOrDefault<Company_table>();

        //        company.EmployeeCount = employee;


        //        context.SaveChanges();


        //        response.EmployeeCount = employee;

        //    }
        //    return response;
        //}

        public ManageEmployeeResponse ReplaceEmployee(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
            if (compId == null)
            {
                throw new Exception("No company found. Try again!");
            }

            using (var context = new Company_dbEntities())
            {
                

                var emp = (from a in context.Employee_table where a.UserName == request.UserName && a.Email == request.Email select a).FirstOrDefault();

                emp.UserName = request.UserName;
                emp.Email = request.Email;
                emp.EmployeeName = request.EmployeeName;
                emp.Designation = request.Designation;
                emp.ManagerID = request.ManagerID;

                //var comp = (from a in context.Company_table
                //            where a.CompanyName == request.CompanyName
                //            select a).FirstOrDefault();

                //if (comp != null)
                //    comp.EmployeeCount++;

                emp.CompanyID = compId.Value;

                context.SaveChanges();
                response.IsEmployeeReplaced = true;
            }
            return response;
        }

        public ManageEmployeeResponse employees(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");

           // List<dynamic> list = new List<dynamic>();
            //List<Employee_table> employee = new List<Employee_table>();
            using (var context = new Company_dbEntities())
            {
                dto dt = new dto();

                 var employee = (from a in context.Employee_table
                                where a.UserName == request.UserName 
                                select a).FirstOrDefault();

                if (employee != null)
                {
                    dt.EmployeeName = employee.EmployeeName;
                    dt.EmployeeID = employee.UserName;
                   // dt.EmployeeID = employee.EmployeeID;
                    dt.ManagerID = employee.ManagerID;
                    //dt.ManagerID = employee.ManagerID;
                    //dt.Department = employee.Department;
                    dt.Designation = employee.Designation;
                    //dt.DOB = employee.DOB;
                    //dt.Address = employee.Address;
                    //dt.Contact = employee.Contact;
                    dt.Email = employee.Email;
                    dt.EmployeeInfo = employee.EmployeeInfo;

                    //var comp = (from a in context.Company_table where a.CompanyID == employee.CompanyID select a.Prefix).FirstOrDefault();

                    //dt.EmployeeID = comp + employee.EmployeeID;

                }
                
                 
                response.EmployeeList = JsonConvert.SerializeObject(dt);

            }
            return response;
        }

        public ManageEmployeeResponse CheckManager(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using (var context = new Company_dbEntities())
            {
                //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                //if (!empId.HasValue)
                //    throw new Exception("Employee does not exist!");

                var employee = (from a in context.Employee_table
                                where a.UserName == request.UserName
                                select a).FirstOrDefault();

                if (employee != null && (employee.Designation == "Manager" || employee.Designation == "manager"))
                    response.IsManager = true;
                else
                    response.IsManager = false;
            }
            return response;
        }

        public ManageEmployeeResponse UpdateEmployee(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using (var context = new Company_dbEntities())
            {
                //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                //if (!empId.HasValue)
                //    throw new Exception("Employee does not exist!");

                var employee = (from a in context.Employee_table
                                where a.UserName == request.UserName
                                select a).FirstOrDefault();

                if (employee != null)
                {
                    //employee.Designation = request.Designation;
                    //employee.ManagerID = request.ManagerID;
                    //employee.Department = request.Department;
                    //employee.DOB = request.DOB;
                    //employee.Address = request.Address;
                   // employee.Contact = request.Contact;
                 
                    employee.EmployeeInfo = request.EmployeeInfo;
                    employee.Email = request.Email;
                    employee.ModifiedOn = DateTime.Now;
                    employee.IsActive = true;
                }
                context.SaveChanges();
                response.IsEmployeeUpdated = true;
            }
            return response;
        }

        


        //All employeees under the requested EmployeeId
        public ManageEmployeeResponse ManagerEmployees(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();
            List<Employee_table> employee = new List<Employee_table>();
            //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");
            List<dto> dto = new List<dto>();
            using (var context = new Company_dbEntities())
            {
                employee = (from a in context.Employee_table
                            where a.ManagerID == request.UserName
                            select a).ToList();

                if (employee.Count() > 0)
                {
                    foreach (var entry in employee)
                    {
                        dto d = new dto();
                        d.EmployeeID = entry.UserName;
                        d.EmployeeName = entry.EmployeeName;

                        dto.Add(d);
                    }

                    response.ManagerEmployees = JsonConvert.SerializeObject(dto);
                }
                else
                    response.ManagerEmployees = null;
            }
            return response;
        }
    }

    public class dto
    {
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public int CompanyID { get; set; }
        //public string Department { get; set; }
        public string Designation { get; set; }
        //public DateTime? DOB { get; set; }
        //public string Address { get; set; }
       // public string Contact { get; set; }
        public string Email { get; set; }
        public string ManagerID { get; set; }
        public string Picture { get; set; }
        public string EmployeeInfo { get; set; }
        public int count { get; set; }
    }

    //public class employeeInfo
    //{
    //    public string Department { get; set; }
    //    public DateTime? DOB { get; set; }
    //    public string Address { get; set; }
    //    public string Contact { get; set; }
        
    //}
}