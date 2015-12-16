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

                int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                if (empId.HasValue)
                    throw new Exception("Employee already exists.");

                var employee = new Employee_table();
                employee.EmployeeName = request.EmployeeName.ToLower();
                employee.CompanyID = compId.Value;
                employee.Address = request.Address;
                employee.Contact = request.Contact;
                employee.Email = request.Email.ToLower();
                employee.Department = request.Department;
                employee.Designation = request.Designation;
                employee.ManagerID = request.ManagerID;
                employee.DOB = request.DOB;
                employee.ModifiedOn = DateTime.Now;
                employee.IsActive = true;

                context.Employee_table.Add(employee);

                //Updating the count of employees on successfulkl addition of employee.
                var comp = (from a in context.Company_table
                            where a.CompanyID == compId.Value
                            select a).FirstOrDefault();

                if (comp != null)
                    comp.EmployeeCount++;

                context.SaveChanges();
                response.IsEmployeeCreated = true;
            }
            return response;
        }

        public ManageEmployeeResponse EmployeeDetail(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();
            //List<Employee_table> employee = new List<Employee_table>();

            using (var context = new Company_dbEntities())
            {
                int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                if (!empId.HasValue)
                    throw new Exception("Employee does not exist!");

                var employee = (from a in context.Employee_table
                                where a.EmployeeID == empId.Value
                                select a).FirstOrDefault();

                if (employee != null)
                {
                    response.IsEmployeeExist = true;

                    dynamic emp = new ExpandoObject();
                    emp.EmployeeName = employee.EmployeeName;
                    emp.EmployeeId = employee.EmployeeID;
                    emp.Email = employee.Email;
                    emp.Designation = employee.Designation;
                    emp.DOB = employee.DOB;
                    emp.Department = employee.Department;
                    emp.ManagerId = employee.ManagerID;
                    emp.CompanyId = employee.CompanyID;
                    emp.Address = employee.Address;
                    emp.Contact = employee.Contact;

                    response.EmployeeInfo = JsonConvert.SerializeObject(emp);
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

        public ManageEmployeeResponse employees(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            List<Employee_table> employee = new List<Employee_table>();
            using (var context = new Company_dbEntities())
            {
                int? compId = AdapterHelper.GetCompanyId(request.CompanyName);
                if (!compId.HasValue)
                    throw new Exception("No company found. Try again!");

                employee = (from a in context.Employee_table
                            where a.CompanyID == compId.Value
                            select a).ToList<Employee_table>();

                response.EmployeeList = employee;
            }
            return response;
        }

        public ManageEmployeeResponse CheckManager(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using (var context = new Company_dbEntities())
            {
                int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                if (!empId.HasValue)
                    throw new Exception("Employee does not exist!");

                var employee = (from a in context.Employee_table
                                where a.EmployeeID == empId.Value
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
                int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
                if (!empId.HasValue)
                    throw new Exception("Employee does not exist!");

                var employee = (from a in context.Employee_table
                                where a.EmployeeID == empId.Value
                                select a).FirstOrDefault();

                if (employee != null)
                {
                    employee.Designation = request.Designation;
                    employee.ManagerID = request.ManagerID;
                    employee.Department = request.Department;
                    employee.DOB = request.DOB;
                    employee.Address = request.Address;
                    employee.Contact = request.Contact;
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

            using (var context = new Company_dbEntities())
            {
                employee = (from a in context.Employee_table
                            where a.ManagerID == request.EmployeeID
                            select a).ToList<Employee_table>();

                if (employee.Count() > 0)
                    response.ManagerEmployees = JsonConvert.SerializeObject(employee);
                else
                    response.ManagerEmployees = null;
            }
            return response;
        }
    }
}