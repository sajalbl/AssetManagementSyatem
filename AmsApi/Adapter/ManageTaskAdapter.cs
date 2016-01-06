using AmsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsApi.Adapter
{
    public class ManageTaskAdapter
    {

        public ManageTaskResponse AddTask(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();
            //int? empId = AdapterHelper.GetEmployeeId(request.AssignedBy, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");

            using (var context = new Company_dbEntities())
            {
                Task_table task = new Task_table();
                task.EmployeeID = request.UserName;
                task.EmployeeName = request.EmployeeName;
                task.Description = request.Description;
                task.AssignedBy = request.AssignedBy;
                task.EmployeeConfirm = request.EmployeeConfirm;

                context.Task_table.Add(task);
                context.SaveChanges();

                response.TaskAdded = true;
            }

            return response;
        }

        public ManageTaskResponse Task(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();
            //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");
            List<Task_table> task = new List<Task_table>();
            List<task> list = new List<task>();
            using(var context = new Company_dbEntities())
            {
                task = (from a in context.Task_table where a.EmployeeID == request.UserName select a).ToList<Task_table>();

                if (task != null)
                {
                    foreach (var entry in task)
                    {
                        task t = new task();

                        t.EmployeeID = entry.EmployeeID;
                        t.EmployeeName = entry.EmployeeName;
                        t.Description = entry.Description;
                        t.AssignedBy = entry.AssignedBy;
                        t.EmployeeConfirm = entry.EmployeeConfirm;
                        t.ManagerConfirm = entry.ManagerConfirm;

                        list.Add(t);
                    }
                    response.TaskList = JsonConvert.SerializeObject(list);
                }
                else
                {
                    response.TaskList = null;
                }

            }
            return response;
        }


        public ManageTaskResponse TaskAssign(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();

            //int? empId = AdapterHelper.GetEmployeeId(request.EmployeeName, request.Email);
            //if (!empId.HasValue)
            //    throw new Exception("Employee does not exist!");
            List<Task_table> task = new List<Task_table>();
            List<task> list = new List<task>();
            using (var context = new Company_dbEntities())
            {
                task = (from a in context.Task_table where a.AssignedBy == request.UserName select a).ToList();

                if(task != null)
                {
                    foreach(var entry in task)
                    {
                        task t = new task();

                        t.EmployeeID = entry.EmployeeID;
                        t.EmployeeName = entry.EmployeeName;
                        t.Description = entry.Description;
                        t.EmployeeConfirm = entry.EmployeeConfirm;
                        t.ManagerConfirm = entry.ManagerConfirm;

                        list.Add(t);
                    }
                    response.TaskAssign = JsonConvert.SerializeObject(list);
                }
                else
                {
                    response.TaskAssign = null;
                }

                
            }
            return response;
        }

        public ManageTaskResponse EmployeeConfirm(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();
            
            using(var context = new Company_dbEntities())
            {
              var  confirm = (from a in context.Task_table where a.EmployeeID == request.UserName && a.Description == request.Description && a.AssignedBy == request.AssignedBy select a).FirstOrDefault<Task_table>();

                if(confirm != null)
                {
                    
                    
                    if(confirm.EmployeeConfirm == "Completed" && confirm.ManagerConfirm == "Approved")
                    {
                        context.SaveChanges();
                        response.ConfirmEmployee = true;
                    }
                    else
                    {
                    confirm.EmployeeConfirm = request.EmployeeConfirm;
                    confirm.ManagerConfirm = null;
                    context.SaveChanges();
                    response.ConfirmEmployee = true;
                    }
                }
                else
                {
                    response.ConfirmEmployee = false;
                }

            }
            return response;
        }

        public ManageTaskResponse Approval(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();

            using (var context = new Company_dbEntities())
            {
                var approve = (from a in context.Task_table where a.EmployeeID == request.UserName && a.Description == request.Description && a.EmployeeConfirm == "Completed" select a).FirstOrDefault<Task_table>();

                if (request.Accept == true)
                {
                    if (approve != null)
                    {

                        approve.ManagerConfirm = request.ManagerConfirm;

                        context.SaveChanges();
                        response.ConfirmManager = true;
                    }
                    else
                    {
                        response.ConfirmManager = false;
                    }
                }

                else
                {
                    if (approve != null)
                    {

                        approve.ManagerConfirm = request.ManagerConfirm;
                        approve.EmployeeConfirm = "Pending";
                        context.SaveChanges();
                        response.ConfirmManager = true;
                    }
                    else
                    {
                        response.ConfirmManager = false;
                    }
                }
            }
            return response;
        }

        
        public ManageTaskResponse DeleteTask(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();

            using (var context = new Company_dbEntities())
            {
                var delete = (from a in context.Task_table where a.EmployeeID == request.UserName && a.Description == request.Description select a).FirstOrDefault<Task_table>();

                if (delete != null) 
                { 
                    
                     context.Task_table.Remove(delete);
                     context.SaveChanges();
                     response.TaskDeleted = true;
                       
                }
                else
                {
                    response.TaskDeleted = false;
                }
            }
            return response;
        }

        
    }

    public class task
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string AssignedBy { get; set; }
        public string EmployeeConfirm { get; set; }
        public string ManagerConfirm { get; set; }
        public bool Accept { get; set; }
    }
}