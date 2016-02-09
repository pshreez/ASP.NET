using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        public JsonResult RecentlyUpdatedTasks(int id)
        {
            List<User> task = new List<User>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                   
                        var updatedTasks = (from TaskAssign in db.TaskAssigned
                                     join Tasks in db.Tasks
                                       on TaskAssign.TASK_ID_ASSIGNED equals Tasks.TASK_ID
                                     where TaskAssign.TASK_UP_DATE != null
                                     select ( new 
                                     {
                                         TASK_USER          =  TaskAssign.TASK_USER_ID,
                                         USER_TASK_STATUS   =  TaskAssign.TASK_STATUS,
                                         USER_UPDATE_DATE   =  TaskAssign.TASK_UP_DATE,
                                         TASK_ASSIGN_DATE   =  TaskAssign.TASK_ASSIGN_DATE,
                                         TASK_ID            =  TaskAssign.TASK_ID_ASSIGNED,
                                         TASK_NAME          =  Tasks.TASK_NAME,
                                         TASK_STATUS        =  Tasks.TASK_STATUS,
                                         PROJECT_ID         =  Tasks.TASK_PROJECT_ID,
                                         TASK_HOUR_CONSUMED = TaskAssign.TASK_HOUR_CONSUMED,
                                         TASK_DESCRIPTION   =  Tasks.TASK_DESCRIPTION,
                                         TASK_END_DATE      =  Tasks.TASK_END_DATE,
                                         TASK_START_DATE    =  Tasks.TASK_START_DATE   
                                         
                                     })).OrderByDescending(USER_UPDATE_DATE => USER_UPDATE_DATE).ToList();
                        return new JsonResult { Data = updatedTasks, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    
                   
                }
            }
            catch (Exception ex) { task = null; }
            return new JsonResult { Data = task, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public ActionResult UpdatedTasksManager()
        {
            return PartialView();
        }



        public JsonResult ProjectStatus(int id)
        {
            var dat = new List<object>();
            try
            {
                int value = db.Projects.Count();
                if (value > 0)
                {
                    
                    int notStarted = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id) &&  a.PROJECT_STATUS == "NOT STARTED").Count();
                    int onProgress = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id) && a.PROJECT_STATUS == "ON PROGRESS").Count();
                    int completed = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id) && a.PROJECT_STATUS == "FINISHED").Count();

                    dat.Add(new { notStarted = notStarted, onProgress = onProgress, completed = completed });
                }
                return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                dat.Add(new { notStarted = 0, onProgress = 0, completed = 0 });
                return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }



        public JsonResult UpcomingTaskDeadlines(int id)
        {
            List<Task> project = new List<Task>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var TaskList = db.TaskAssigned.Where(a => a.TASK_USER_ID.Equals(id)).Count();
                    if (TaskList > 0)
                    {
                        var dprojects = (from TaskAssign in db.TaskAssigned
                                        join Tasks in db.Tasks
                                        on TaskAssign.TASK_ID_ASSIGNED equals Tasks.TASK_ID
                                        where TaskAssign.TASK_USER_ID == id && (DbFunctions.DiffDays(DateTime.Now, Tasks.TASK_END_DATE).Value > 0 && DbFunctions.DiffDays(DateTime.Now, Tasks.TASK_END_DATE).Value <20)
                                        select (new
                                        {
                                            TASK_ASSIGN_DATE    = TaskAssign.TASK_ASSIGN_DATE,
                                            TASK_HOUR_CONSUMED  = TaskAssign.TASK_HOUR_CONSUMED,
                                            TASK_ID_ASSIGNED    = TaskAssign.TASK_ID_ASSIGNED,
                                            TASK_STATUS         =  TaskAssign.TASK_STATUS,
                                            PROJECT_ID          = TaskAssign.PROJECT_ID,
                                            ID                  = TaskAssign.ID,
                                            TASK_NAME           = Tasks.TASK_NAME,
                                            TASK_START_DATE     = Tasks.TASK_START_DATE,
                                            TASK_END_DATE       = Tasks.TASK_END_DATE,
                                            TASK_DESCRIPTION    = Tasks.TASK_DESCRIPTION,
                                            TASK_PRIORITY       = Tasks.TASK_PRIORITY,
                                            U_TASK_CREATE_DATE  = Tasks.U_TASK_CREATE_DATE,
                                            TASK_USER_ID        = Tasks.TASK_USER_ID,
                                            TASK_ESTIMATED_HOUR =  Tasks.TASK_ESTIMATED_HOUR,

                                            Days = DbFunctions.DiffDays(DateTime.Now, Tasks.TASK_END_DATE).Value

                              })).OrderByDescending(TASK_END_DATE => TASK_END_DATE).ToList();
                        return new JsonResult { Data = dprojects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else
                    {
                        return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
            }  catch (Exception ex)
            {
                
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }


        public JsonResult TaskStatus(int id )
        {
            var dat = new List<object>();
            try
            {
                int value = db.Projects.Count();
                if (value > 0)
                {

                    int notStarted = (from TaskAssign in db.TaskAssigned
                                      join Tasks in db.Tasks
                                      on TaskAssign.TASK_ID_ASSIGNED equals Tasks.TASK_ID
                                      where TaskAssign.TASK_USER_ID == id && Tasks.TASK_STATUS =="NOT STARTED"
                                      select (new
                                      {
                                         NotStarted  = Tasks.TASK_STATUS
                                      })).Count();

                    int OnProgress = (from TaskAssign in db.TaskAssigned
                                      join Tasks in db.Tasks
                                      on TaskAssign.TASK_ID_ASSIGNED equals Tasks.TASK_ID
                                      where TaskAssign.TASK_USER_ID == id && Tasks.TASK_STATUS == "ON PROGRESS"
                                      select (new
                                      {
                                          Onprogress = Tasks.TASK_STATUS
                                      })).Count();

                    int completed = (from TaskAssign in db.TaskAssigned
                                     join Tasks in db.Tasks
                                     on TaskAssign.TASK_ID_ASSIGNED equals Tasks.TASK_ID
                                     where TaskAssign.TASK_USER_ID == id && Tasks.TASK_STATUS == "FINISHED"
                                     select (new
                                     {
                                         Completed = Tasks.TASK_STATUS
                                     })).Count();

                    dat.Add(new { notStarted = notStarted, OnProgress = OnProgress, completed = completed });
                }
                return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                dat.Add(new { notStarted = 0, onProgress = 0, completed = 0 });
                return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}