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

                response.TaskList = JsonConvert.SerializeObject(task); 
            }
            return response;
        }
    }
}