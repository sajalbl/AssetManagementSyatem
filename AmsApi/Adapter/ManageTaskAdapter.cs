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
            using (var context = new Company_dbEntities())
            {
                Task_table task = new Task_table();
                task.EmployeeID = request.EmployeeID;
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
            List<Task_table> task = new List<Task_table>();
            using(var context = new Company_dbEntities())
            {
                task = (from a in context.Task_table where a.EmployeeID == request.EmployeeID select a).ToList<Task_table>();

                response.TaskList = task;

            }
            return response;
        }


        public ManageTaskResponse TaskAssign(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();
            List<Task_table> task = new List<Task_table>();
            using (var context = new Company_dbEntities())
            {
                task = (from a in context.Task_table where a.AssignedBy == request.EmployeeID select a).ToList<Task_table>();

                response.TaskAssign = JsonConvert.SerializeObject(task);
            }
            return response;
        }

        public ManageTaskResponse EmployeeConfirm(ManageTaskRequest request)
        {
            ManageTaskResponse response = new ManageTaskResponse();
            
            using(var context = new Company_dbEntities())
            {
              var  confirm = (from a in context.Task_table where a.EmployeeID == request.EmployeeID && a.Description == request.Description && a.AssignedBy == request.AssignedBy select a).FirstOrDefault<Task_table>();

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
                var approve = (from a in context.Task_table where a.EmployeeID == request.EmployeeID && a.Description == request.Description && a.EmployeeConfirm == "Completed" select a).FirstOrDefault<Task_table>();

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
                var delete = (from a in context.Task_table where a.EmployeeID == request.EmployeeID && a.Description == request.Description select a).FirstOrDefault<Task_table>();

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
}