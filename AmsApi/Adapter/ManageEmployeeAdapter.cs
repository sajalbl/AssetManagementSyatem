using AmsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            
            using(var context = new Company_dbEntities())
            {
               var employee = (from a in context.Employee_table where a.EmployeeID == request.EmployeeID select a).FirstOrDefault<Employee_table>();

                if(employee == null)
                {
                    employee = new Employee_table();
                    employee.EmployeeName = request.EmployeeName;
                    employee.CompanyName = request.CompanyName;
                    employee.EmployeeID = request.EmployeeID;
                    employee.Department = request.Department;
                    employee.Designation = request.Designation;
                    employee.ManagerID = request.ManagerID;

                    context.Employee_table.Add(employee);
                    context.SaveChanges();

                    response.IsEmployeeCreated = true; 
                }
                else
                {
                    context.SaveChanges();
                    response.IsEmployeeCreated = false;
                }
            }
            return response;
        }

        public ManageEmployeeResponse checkEmployee(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using(var context = new Company_dbEntities())
            {
                var employee = (from a in context.Employee_table where a.EmployeeID == request.EmployeeID && a.EmployeeName == request.EmployeeName select a).FirstOrDefault<Employee_table>();

                if(employee != null)
                {
                    response.IsEmployeeExist = true;
                }
                else
                {
                    response.IsEmployeeExist = false;
                }
            }
            return response;
        }

        public ManageEmployeeResponse countEmployee(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using (var context = new Company_dbEntities())
            {

                var employee = (from a in context.Employee_table where request.CompanyName == a.CompanyName select a).Count();

                var company = (from a in context.Company_table where request.CompanyName == a.CompanyName select a).FirstOrDefault<Company_table>();

                company.EmployeeCount = employee;

              
                context.SaveChanges();
                

                response.EmployeeCount = employee;

            }
            return response;
        }

        public ManageEmployeeResponse employees(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            List<Employee_table> employee = new List<Employee_table>();
            using (var context = new Company_dbEntities())
            {

                employee = (from a in context.Employee_table where request.EmployeeID == a.EmployeeID select a).ToList<Employee_table>();
                response.EmployeeList = employee;

            }
            return response;
        }

        public ManageEmployeeResponse CheckManager(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using(var context = new Company_dbEntities())
            {
                var employee = (from a in context.Employee_table where request.EmployeeID == a.EmployeeID select a).FirstOrDefault<Employee_table>();

                if(employee.Designation == "manager")
                {
                    response.IsManager = true;
                    
                }
                else
                {
                    response.IsManager = false;
                }
            }
            return response;
        }

        public ManageEmployeeResponse UpdateEmployee(ManageEmployeeRequest request)
        {
            //var destinationPath = "http://localhost:58474/images/";
            
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            using(var context = new Company_dbEntities())
            {
                var employee = (from a in context.Employee_table where a.EmployeeID == request.EmployeeID select a).FirstOrDefault<Employee_table>();

                employee.DOB = request.DOB.ToShortDateString();
                employee.Address = request.Address;
                employee.Contact = request.Contact;
                employee.Email = request.Email;
                

                //var image = request.Picture;
                //var source = Path.Combine(image);
                //var destination = Path.Combine(destinationPath, image);

                //File.Copy(source, destination , true);

                context.SaveChanges();
                response.Edit = true;
            }
            return response;
        }

        public ManageEmployeeResponse EmployeeDetail(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            List<Employee_table> employee = new List<Employee_table>();
            using (var context = new Company_dbEntities())
            {

                employee = (from a in context.Employee_table where request.CompanyName == a.CompanyName select a).ToList<Employee_table>();
                response.EmployeeDetail = JsonConvert.SerializeObject(employee);

            }
            return response;
        }


        public ManageEmployeeResponse ManagerEmployees(ManageEmployeeRequest request)
        {
            ManageEmployeeResponse response = new ManageEmployeeResponse();

            List<Employee_table> employee = new List<Employee_table>();
            using (var context = new Company_dbEntities())
            {

                employee = (from a in context.Employee_table where request.EmployeeID == a.ManagerID select a).ToList<Employee_table>();
                response.ManagerEmployees = JsonConvert.SerializeObject(employee);

            }
            return response;
        }
    }
}