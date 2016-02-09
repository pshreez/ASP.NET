using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class TaskController : Controller
    {
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        public ActionResult Index()
        {
            return PartialView();
        }

        public void status()
        {
            using (db)
            {
                if (ModelState.IsValid)
                {
                    Response.Write(true);
                }
                else
                    Response.Write(false);
            }
        }

        public JsonResult CreateTask(Task TaskData)
        {   
            var dat = new List<object>();
            string msg = "";
            bool TaskCreateStatus = false;
            int UID = Convert.ToInt32(this.Session["UserProfile"].ToString()); //user.getSession();
            Task newTask = new Task();
            try
            {
                using (db) {
                   if (ModelState.IsValid) {                         
                        newTask.TASK_NAME = TaskData.TASK_NAME;
                        newTask.TASK_USER_ID = UID;           // TaskData.TASK_USER_ID;
                        newTask.TASK_PROJECT_ID = TaskData.TASK_PROJECT_ID;
                        newTask.TASK_START_DATE = TaskData.TASK_START_DATE;
                        newTask.TASK_END_DATE = TaskData.TASK_END_DATE;
                        newTask.TASK_STATUS = "NOT STARTED";
                        newTask.TASK_PRIORITY = TaskData.TASK_PRIORITY;
                        newTask.TASK_ESTIMATED_HOUR = TaskData.TASK_ESTIMATED_HOUR;
                        newTask.TASK_HOUR_CONSUMED = 0;
                        newTask.PROJECT_PHASE = TaskData.PROJECT_PHASE;
                        newTask.TASK_DESCRIPTION = TaskData.TASK_DESCRIPTION;
                        newTask.U_TASK_CREATE_DATE = DateTime.Now;
                        db.Tasks.Add(newTask);
                        db.SaveChanges();
                          TaskCreateStatus = true;
                            msg = "Success";                                               
                    } else { msg = "Failed!"; }
                }
            }
            catch (Exception ex) { msg = ex.Message; }
            dat.Add(new { Message = msg, Status = TaskCreateStatus, Formdata = TaskData });
            return new JsonResult { Data = dat };                           //, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; //  //return new JsonResult { Data = message };
        }

        public JsonResult getTaskList()
        {
            try
            {
                int value = db.Tasks.Count();
                if (value > 0)
                {
                    var TaskList = db.Tasks.ToList();
                    return new JsonResult { Data = TaskList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public ActionResult ViewTaskList()
        {
            return PartialView();
        }
        public ActionResult GetTaskDetails(int id)
        {

            List<Task> task = new List<Task>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var TaskDetails = db.Tasks.Where(a => a.TASK_ID.Equals(id)).FirstOrDefault();
                    if (TaskDetails != null)
                    {
                        task = db.Tasks.Where(a => a.TASK_ID.Equals(id)).OrderBy(a => a.TASK_ID).ToList();
                    }
                    else { task = null; }
                }
            }
            catch (Exception ex) { task = null; }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult GetAssignedUsers(int id) //project id 
        {
            List<User> task = new List<User>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var TaskDetails = db.Tasks.Where(a => a.TASK_ID.Equals(id)).FirstOrDefault();
                    if (TaskDetails != null)
                    {
                     var users = (from TaskAssign in db.TaskAssigned join User in db.Users
                                  on TaskAssign.TASK_USER_ID equals User.U_ID where TaskAssign.TASK_ID_ASSIGNED == id
                                          select new
                                          {
                                              TaskAssign.TASK_USER_ID,
                                              User.U_LOGIN_NAME,
                                              User.U_FIRST_NAME,
                                              User.U_LAST_NAME,
                                          }).ToList();
                        return new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }else { task = null; }
                }
            }
            catch (Exception ex) { task = null; }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult Details()
        {
            return PartialView();
        }

        /**************** manager ***********************************/
        public ActionResult MyCreatedTasksList()
        {
            return PartialView();
        }

        public ActionResult ProjectTasksList()
        {
            return PartialView();
        }


        public JsonResult MyCreatedTasks(int id) //project manager id 
        {
           List<User> task = new List<User>();
           try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB()){
                    var createdTasks = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id)).FirstOrDefault();
                    if (createdTasks != null)
                    {
                        var tasks = (from Project in db.Projects
                                     join Task in db.Tasks
                                     on Project.PROJECT_ID equals Task.TASK_PROJECT_ID
                                     where Project.PROJECT_MANAGER == id
                                     select new{
                                         Project,
                                         Task                                        
                                     }).ToList();
                        return new JsonResult { Data = tasks, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    } else { task = null; }
                }
            } catch (Exception ex) { task = null; }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /*** listing tasks of a project **/
        public JsonResult getTaskofproject(int id)
        {
            List<Task> task = new List<Task>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var TaskList = db.Tasks.Where(a => a.TASK_PROJECT_ID.Equals(id)).OrderBy(a =>a.U_TASK_CREATE_DATE).FirstOrDefault();
                    if (TaskList != null){
                        task = db.Tasks.Where(a => a.TASK_PROJECT_ID.Equals(id)).OrderBy(a => a.TASK_NAME).ToList();
                    } else { task = null; }
                }
            }
            catch (Exception ex) { task = null; }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult AssignTask()
        {
            return PartialView();
        }
        public ActionResult AssignTaskToUser()
        {
            return PartialView();
        }

        public JsonResult AssignTaskAction(Taskassign assignData)
         {
            var dat = new List<object>();
            string msg = "";
            bool TaskAssignStatus = false;
          //  int UID = Convert.ToInt32(this.Session["UserProfile"].ToString()); //user.getSession();
            Taskassign assignTask = new Taskassign();
            try
            {
                using (db)
                {
                    if (ModelState.IsValid)
                    {
                        var assignExist = db.TaskAssigned
                            .Where(
                                   a => a.TASK_ID_ASSIGNED.Equals(assignData.TASK_ID_ASSIGNED) &&
                                   a.PROJECT_ID.Equals(assignData.PROJECT_ID) && a.TASK_USER_ID.Equals(assignData.TASK_USER_ID))
                                   .FirstOrDefault();                          //var LoggedInUser = db.Users.Where(a => a.U_LOGIN_NAME.Equals(LoginData.U_LOGIN_NAME) && a.U_PASSWORD.Equals(LoginData.U_PASSWORD)).FirstOrDefault();
                        if (assignExist == null)
                        {
                            assignTask.PROJECT_ID = assignData.PROJECT_ID;
                            assignTask.TASK_ID_ASSIGNED = assignData.TASK_ID_ASSIGNED;
                            assignTask.TASK_USER_ID = assignData.TASK_USER_ID;
                            assignTask.TASK_ASSIGN_DATE = DateTime.Now;
                            assignTask.TASK_HOUR_CONSUMED = 0;
                            assignTask.TASK_STATUS = "NOT STARTED";
                            db.TaskAssigned.Add(assignTask);
                            db.SaveChanges();
                            msg = "Success";
                            TaskAssignStatus = true;
                        }
                        else
                        {
                            msg = "User  is already assigned";
                        }                                            
                    }
                    else { msg = "Failed!"; }
                }
            }
            catch (Exception ex) { msg = ex.Message; }
            dat.Add(new { Message = msg, AssignStatus = TaskAssignStatus });
            return new JsonResult { Data = dat };
        }
       


        /****************** developers *******************************/

        public ActionResult AssignedTaskList_d()
        {
            return PartialView();
        }

        public JsonResult AssignedTaskList(int id)
        {
            List<Task> task = new List<Task>(); 
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var MyAssignedTaskList = db.TaskAssigned.Where(a => a.TASK_USER_ID.Equals(id)).FirstOrDefault();
                    if (MyAssignedTaskList != null)
                    {
                        var assignedtasks = (from TaskAssigned in db.TaskAssigned
                                     join Task in db.Tasks
                                     on TaskAssigned.TASK_ID_ASSIGNED equals Task.TASK_ID
                                     where TaskAssigned.TASK_USER_ID == id
                                     select (  new {
                                         TASK_ID            =  Task.TASK_ID,
                                         TASK_NAME          =  Task.TASK_NAME,
                                         TASK_PROJECT_ID    =  Task.TASK_PROJECT_ID,
                                         TASK_START_DATE    =  Task.TASK_START_DATE,
                                         TASK_END_DATE      =  Task.TASK_END_DATE,
                                         TASK_PRIORITY      = Task.TASK_PRIORITY,
                                         TASK_STATUS        = Task.TASK_STATUS,
                                         TASK_ESTIMATED_HOUR= Task.TASK_ESTIMATED_HOUR,
                                         TASK_USER_ID       = Task.TASK_USER_ID,
                                         TASK_DESCRIPTION   = Task.TASK_DESCRIPTION,
                                         PROJECT_PHASE      = Task.PROJECT_PHASE,
                                         U_TASK_CREATE_DATE = Task.U_TASK_CREATE_DATE,
                                         TASK_ASSIGN_DATE   = TaskAssigned.TASK_ASSIGN_DATE,
                                         TASK_HOUR_CONSUMED = TaskAssigned.TASK_HOUR_CONSUMED,
                                         ID                 = TaskAssigned.ID,
                                         USER_TASK_STATUS   = TaskAssigned.TASK_STATUS
                                     })).ToList();
                        return new JsonResult { Data = assignedtasks, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else { task = null; }
                }           
            } catch (Exception ex) {
                task = null;               
            }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        /********        editing task      *******************/
        public ActionResult EditTask()
        {
            return PartialView();
        }   
        
        public ActionResult TaskEditAction(Task UpdateTask)
        {
            bool TaskStatus = false;
            string msg = " ";
            var dat = new List<object>();
            try
            {
                using (db)
                {
                    if (ModelState.IsValid)
                    {
                        var Task = db.Tasks.Where(a => a.TASK_ID.Equals(UpdateTask.TASK_ID)).FirstOrDefault();
                        if (Task != null)
                        {
                            Task.TASK_PROJECT_ID = UpdateTask.TASK_PROJECT_ID;
                            Task.TASK_NAME = UpdateTask.TASK_NAME;
                            Task.TASK_START_DATE = UpdateTask.TASK_END_DATE;
                            Task.TASK_PRIORITY = UpdateTask.TASK_PRIORITY;
                            Task.PROJECT_PHASE = UpdateTask.PROJECT_PHASE;
                            Task.TASK_ESTIMATED_HOUR = UpdateTask.TASK_ESTIMATED_HOUR;
                            Task.TASK_DESCRIPTION = UpdateTask.TASK_DESCRIPTION;
                            db.SaveChanges();
                            TaskStatus = true;
                            msg = "Successfully updated task";
                        }
                        else { msg = "Task not found"; }
                    }
                    else { msg = "Invalid modal state to update status"; }
                }
                dat.Add(new { Message = msg, Status = TaskStatus });
                return new JsonResult { Data = dat };
            }
            catch (Exception ex)
            {
                dat.Add(new { Message = ex, Status = TaskStatus });
                return new JsonResult { Data = dat };
            }

        }
        public ActionResult UpdatedTaskList_d()
        {
            return PartialView();
        }


        public ActionResult UpdateTaskStatus()
        {
            return PartialView();
        }

        public ActionResult UpdateTaskHour()
        {
            return PartialView();
        }

        public JsonResult UpdateTaskStatusAction(Taskassign UpdateTask)
        {
            bool TaskStatus = false;
            string msg = " ";
            var dat = new List<object>();
            try
            {              
                using (db) {
                    if (ModelState.IsValid) {
                        var TaskAssign = db.TaskAssigned.Where(a => a.ID.Equals(UpdateTask.ID)).FirstOrDefault();
                        if(TaskAssign != null) {
                            TaskAssign.TASK_STATUS        = UpdateTask.TASK_STATUS;
                            TaskAssign.TASK_HOUR_CONSUMED = TaskAssign.TASK_HOUR_CONSUMED + UpdateTask.TASK_HOUR_CONSUMED;
                            db.SaveChanges();
                            TaskStatus = true;
                            msg = "Successfully updated status";
                        }
                        else {
                            msg = "Task not found";
                        }
                    }
                    else {
                        msg = "Invalid modal state to update status";
                    }

                }
                dat.Add(new { Message = msg, Status = TaskStatus });
                return new JsonResult { Data = dat };
            }
            catch (Exception ex)
            {
                dat.Add(new { Message = ex, Status = TaskStatus });
                return new JsonResult { Data = dat };
            }
        }

        public JsonResult UpdateTaskHourAction(Taskassign UpdateTask)
        {
            bool TaskHour = false;
            string msg = " ";
            var dat = new List<object>();          
            try
            {
                using (db)  {
                    if (ModelState.IsValid) {
                        var Task = db.TaskAssigned.Where(a => a.ID.Equals(UpdateTask.ID) && a.TASK_ID_ASSIGNED.Equals(UpdateTask.TASK_ID_ASSIGNED)).FirstOrDefault();
                        if (Task != null)  {
                             Task.TASK_HOUR_CONSUMED = UpdateTask.TASK_HOUR_CONSUMED;
                             Task.TASK_UP_DATE = DateTime.Now;
                             db.SaveChanges();
                             TaskHour = true;
                             msg = "Successfully updated status";
                        }  else {
                            msg = "Task not found";
                        }
                    }  else {
                        msg = "Invalid modal state to update status";
                    }
                }
                dat.Add(new { Message = msg, Status = TaskHour });
                return new JsonResult { Data = dat };
            }
            catch (Exception ex)
            {
                dat.Add(new { Message = ex, Status = TaskHour });
                return new JsonResult { Data = dat };
            }
        }
            
        public ActionResult DateError()
        {
            return PartialView();
        }
        


    }
}