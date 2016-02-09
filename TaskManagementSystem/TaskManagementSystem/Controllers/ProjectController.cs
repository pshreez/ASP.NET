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
    public class ProjectController : Controller
    {
        // GET: Project
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        public ActionResult Index()
        {
           
            return PartialView();

        }
       

      //  public 
        public JsonResult CreateProject(Project ProjectData)
        {
            //UserController user = new UserController();
            int UID = Convert.ToInt32(this.Session["UserProfile"].ToString()); //user.getSession();

            var dat = new List<object>();
            string msg = "";
            bool ProjectCreateStatus = false;

            Project newProject = new Project();
            try
            {
                using (db)
                {
                    if (ModelState.IsValid)
                    {
                        var ProjectExist = db.Projects.Where(a => a.PROJECT_NAME.Equals(ProjectData.PROJECT_NAME)).FirstOrDefault();
                        if (ProjectExist == null)
                        {
                            newProject.PROJECT_NAME = ProjectData.PROJECT_NAME;
                            newProject.PROJECT_MANAGER = UID;// ProjectData.PROJECT_MANAGER;
                            newProject.PROJECT_START_DATE = ProjectData.PROJECT_START_DATE;
                            newProject.PROJECT_END_DATE = ProjectData.PROJECT_END_DATE;
                            newProject.ORG_ID = ProjectData.ORG_ID;
                            newProject.PROJECT_STATUS = "NOT STARTED";
                            newProject.PROJECT_DESCRIPTION = ProjectData.PROJECT_DESCRIPTION;
                            newProject.U_PROJECT_CREATE_DATE = DateTime.Now;
                            db.Projects.Add(newProject);
                            db.SaveChanges();
                            ProjectCreateStatus = true;
                            msg = "Success";
                        
                        } else { msg = "Project name already Exist!"; }
                    } else { msg = "Failed!"; }
                }
               
            }
            catch (Exception ex) {
                msg = ex.Message;
                dat.Add(new { ProjectStatus = ProjectCreateStatus, Message = msg });
            }
            dat.Add(new { Message = msg, ProjectStatus = ProjectCreateStatus });
            return new JsonResult { Data = dat };//, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; //  //return new JsonResult { Data = message };
        }

        public JsonResult MyProjectList(int id)
        {
            try
            {
                int value = db.Projects.Count();
               if (value > 0)
               {                                               
                    var ProjectList  = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id)).OrderByDescending(a => a.U_PROJECT_CREATE_DATE).ToList();//(from d in db.Projects                                                    
                    return new JsonResult { Data = ProjectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
               }
               return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
           {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult MyProjectListView()
        {
            return PartialView();
        }

        public JsonResult getProjectList()
        {
            try
            {
                int value = db.Projects.Count();
                if (value > 0)
                {
                    var ProjectList = db.Projects.OrderByDescending( a => a.U_PROJECT_CREATE_DATE ).ToList();
                    return new JsonResult { Data = ProjectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }catch (Exception ex) {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult ViewProjectList()
        {
            return PartialView();
        }


        public JsonResult getNotStartedProjectList(int id)
        {
            try
            {
                int value = db.Projects.Count();
                if (value > 0) {
                    var ProjectList = db.Projects.Where(
                        a => a.PROJECT_MANAGER.Equals(id) && 
                        a.PROJECT_STATUS.Equals("NOT STARTED"))
                        .OrderByDescending(a => a.U_PROJECT_CREATE_DATE).ToList();//(from d in db.Projects                                                    
                    return new JsonResult { Data = ProjectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            } catch (Exception ex) {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult getOnProgressProjectList(int id)
        {
            try
            {
                int value = db.Projects.Count();
                if (value > 0) {
                    var ProjectList = db.Projects.Where(
                        a => a.PROJECT_MANAGER.Equals(id) &&
                        a.PROJECT_STATUS.Equals("ON PROGRESS"))
                        .OrderByDescending(a => a.U_PROJECT_CREATE_DATE).ToList();
                    return new JsonResult { Data = ProjectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }catch (Exception ex){
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public JsonResult getCompletedProjectList(int id)
        {
            try
            {
                int value = db.Projects.Count();
                if (value > 0) {
                    var ProjectList = db.Projects.Where(
                       a => a.PROJECT_MANAGER.Equals(id) &&
                       a.PROJECT_STATUS.Equals("FINISHED"))
                       .OrderByDescending(a => a.U_PROJECT_CREATE_DATE).ToList();
                    return new JsonResult { Data = ProjectList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            } catch (Exception ex) {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult ShowAllProjects()
        {
            return PartialView();
        }
        public ActionResult NotStartedProjectList()
        {
            return PartialView();
        }
        public ActionResult OnProgressProjectList()
        {
            return PartialView();
        }

        public ActionResult CompletedProjectList()
        {
            return PartialView();
        }

        public JsonResult GetProjectDetails(int id)
        {
            List<Project> project = new List<Project>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB()) {
                    var ProjectDetails = db.Projects.Where(a => a.PROJECT_ID.Equals(id)).FirstOrDefault();
                    if (ProjectDetails != null) {
                        project = db.Projects.Where(a => a.PROJECT_ID.Equals(id)).OrderBy(a => a.PROJECT_NAME).ToList();
                    } else { project = null; }
                }
            } catch (Exception ex) { project = null; }
            return new JsonResult { Data = project, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult DeadlineProjects(int id)
        {
            List<Project> project = new List<Project>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var dProjectList = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id)).Count();
                    if(dProjectList > 0 )
                    {
                      var  dprojects = db.Projects.Where(a => a.PROJECT_MANAGER.Equals(id) && (DbFunctions.DiffDays(DateTime.Now, a.PROJECT_END_DATE).Value > 0 && DbFunctions.DiffDays(DateTime.Now, a.PROJECT_END_DATE).Value <= 10))
                            .Select(
                            a => new {
                                ORG_ID                = a.ORG_ID,
                                PROJECT_ID            = a.PROJECT_ID,
                                PROJECT_NAME          = a.PROJECT_NAME,
                                PROJECT_START_DATE    = a.PROJECT_START_DATE,
                                PROJECT_END_DATE      = a.PROJECT_END_DATE,
                                PROJECT_STATUS        = a.PROJECT_STATUS,
                                PROJECT_DESCRIPTION   = a.PROJECT_DESCRIPTION,
                                U_PROJECT_CREATE_DATE = a.U_PROJECT_CREATE_DATE,
                                Days = DbFunctions.DiffDays(DateTime.Now, a.PROJECT_END_DATE).Value
                                
                             }).OrderBy(Days => Days).ToList(); 
                         return new JsonResult { Data = dprojects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else
                    {
                        return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }            
            }
            catch (Exception ex) {  ;
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
          }

       public ActionResult Details()
        {
            return PartialView();
        }

      

        public ActionResult EditProject()
        {
            return PartialView();


        }

        public JsonResult EditProjectAction(Project UpdateProject)
        {

            bool ProjectStatus = false;
            string msg = " ";
            var dat = new List<object>();
            try
            {
                using (db)
                {
                    if (ModelState.IsValid)
                    {
                        var Project = db.Projects.Where(a => a.PROJECT_ID.Equals(UpdateProject.PROJECT_ID)).FirstOrDefault();
                        if (Project != null)
                        {                           
                            Project.PROJECT_NAME = UpdateProject.PROJECT_NAME;
                            Project.PROJECT_START_DATE = UpdateProject.PROJECT_START_DATE;
                            Project.PROJECT_END_DATE = UpdateProject.PROJECT_END_DATE;
                            Project.ORG_ID = UpdateProject.ORG_ID;
                            Project.PROJECT_STATUS = UpdateProject.PROJECT_STATUS;
                            Project.PROJECT_DESCRIPTION = UpdateProject.PROJECT_DESCRIPTION;
                            db.SaveChanges();                           
                            ProjectStatus = true;
                            msg = "Successfully updated Project";
                        } else{ msg = "Project not found"; }
                    } else { msg = "Invalid modal state to update status";}
                }
                dat.Add(new { Message = msg, Status = ProjectStatus });
                return new JsonResult { Data = dat };
            }
            catch (Exception ex)
            {
                dat.Add(new { Message = ex, Status = ProjectStatus });
                return new JsonResult { Data = dat };
            }
        }

       



    }
}