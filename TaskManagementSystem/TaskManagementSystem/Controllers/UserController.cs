using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class UserController : Controller
    {
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult UserLogin(User LoginData)
        {
            var dat = new List<object>();
            bool status = false;
            string msg = " ";
            string urlLink = " ";
            try
            {
                var userExist = db.Users.Where(
                                a => a.U_LOGIN_NAME.Equals(LoginData.U_LOGIN_NAME)).FirstOrDefault();                          //var LoggedInUser = db.Users.Where(a => a.U_LOGIN_NAME.Equals(LoginData.U_LOGIN_NAME) && a.U_PASSWORD.Equals(LoginData.U_PASSWORD)).FirstOrDefault();
                if (userExist != null) {

                   var LoggedInUser = db.Users.Where( a => a.U_LOGIN_NAME.Equals(LoginData.U_LOGIN_NAME) && a.U_PASSWORD.Equals(LoginData.U_PASSWORD))
                         .Select(a => new {
                             U_LOGIN_NAME = a.U_LOGIN_NAME,
                             U_POSITION   = a.U_POSITION,
                             U_PASSWORD   = a.U_PASSWORD,
                             U_ID         = a.U_ID
                         }).Single();
                   
                    status = true;                 
                    msg = "You are logged in";
                    urlLink = Url.Action("Index", "Project");                                             //  return Json(new { Data = dat, newurl = Url.Action("About","Home") });  //  return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };                              //in a real world, here will be multiple database calls - or others // return Json(new { ok = true, newurl = Url.Action("About") });
                    dat.Add(new { Status = status, User = LoggedInUser, Message = msg, newurl = urlLink });
                    this.Session["UserProfile"] = LoggedInUser.U_ID;
                } else {
                    msg = " User doesn't Exist";
                    dat.Add(new { Status = status, User = " ", Message = msg, newurl = urlLink });
                }              
            } catch (Exception ex) {
                msg = ex.Message;
                dat.Add(new { Status = status, User = " ", Message = msg, newurl = urlLink });
            }                                                                                                //return  Json (new { Data =  LoggedInUser, url = Url.Action("Index", "Home") });
            return new JsonResult { Data = dat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

     

        
        public ActionResult Register()
        {
           
            return PartialView();
        }

   
        public JsonResult RegisterUser(User RegisterData)
        {
            var dat = new List<object>();
            string msg = "";           
            bool RegisterStatus = false;
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    if (ModelState.IsValid)
                    {
                        var RegisterUser = db.Users.Where(a => a.U_LOGIN_NAME.Equals(RegisterData.U_LOGIN_NAME)).FirstOrDefault();
                        if (RegisterUser == null)
                        {
                            User newUser = new User();
                            newUser.U_EMAIL = RegisterData.U_EMAIL;
                            newUser.U_FIRST_NAME = RegisterData.U_FIRST_NAME;
                            newUser.U_LAST_NAME = RegisterData.U_LAST_NAME;
                            newUser.U_LOGIN_NAME = RegisterData.U_LOGIN_NAME;
                            newUser.U_PASSWORD = RegisterData.U_PASSWORD;
                            newUser.U_POSITION = RegisterData.U_POSITION;
                            newUser.U_REGISTER_DATE = DateTime.Now;
                            db.Users.Add(newUser);
                            db.SaveChanges();
                            RegisterStatus = true;
                            msg = "Success";
                        }
                        else { msg = "Username already exist!"; }
                    }
                    else { msg = "Failed!"; }
                }
            } catch (Exception ex) {
                msg = ex.Message;
                dat.Add(new { Status = RegisterStatus, Username = " ", Message = msg });
            }
            dat.Add(new { Status = RegisterStatus, Username = RegisterData.U_LOGIN_NAME, Message = msg });
            return new JsonResult { Data = dat };                                                                                       //, JsonRequestBehavior.AllowGet); //  //return new JsonResult { Data = message };
        
        }
        public JsonResult getUserList()
        {
            try
            {
                int value = db.Users.Count();
                if (value > 0) {
                    var UserList = db.Users.OrderBy(a => a.U_REGISTER_DATE).OrderByDescending(a => a.U_REGISTER_DATE).ToList();
                    return new JsonResult { Data = UserList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }catch (Exception ex) {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            

        }

        public ActionResult ViewUserList()
        {
            return PartialView();
        }

     
        public JsonResult GetUserDetails(int id)
        {         
           List<User> user = new List<User>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {                                      
                    var RegisterUser = db.Users.Where(a => a.U_ID.Equals(id)).FirstOrDefault();
                    if (RegisterUser != null)
                    {
                        user = db.Users.Where(a => a.U_ID.Equals(id)).OrderBy(a => a.U_LOGIN_NAME).ToList();
                    } else { user = null;  }
                }
            } catch (Exception ex) { user = null;  }
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult getDevelopers()
        {
            try
            {
                int value = db.Users.Count();
                if (value > 0) {
                    var DeveloperList = db.Users.Where(a => a.U_POSITION == "developer").ToList();
                    return new JsonResult { Data = DeveloperList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult Details()
        {           
            return PartialView();
        }

        public JsonResult IsUserAvailable(User LoginData)
        {
            bool status = false;
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var LoggedInUser = db.Users.Where(
                       a => a.U_LOGIN_NAME.Equals(LoginData.U_LOGIN_NAME))
                       .FirstOrDefault();
                     if (LoggedInUser != null) status = true;
                    return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            } catch (Exception)  {
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult GetUserType(string uname )
        {
            
            List<User> user = new List<User>();
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    var LoggedInUser = db.Users.Where(
                       a => a.U_LOGIN_NAME.Equals(uname))
                       .FirstOrDefault();
                    if (LoggedInUser != null)
                        user = db.Users.Where(a => a.U_LOGIN_NAME.Equals(uname)).ToList();
                        return new JsonResult { Data = uname, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            catch (Exception Ex)
            {
                // return Ex.ToString();
                Response.Write(Ex);
                return new JsonResult { Data = false, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }


        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }



        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RegisterPage()
        {
            return PartialView();
        }

       
    }
}

/*


    angular.module('cookieStoreExample', ['ngCookies'])
.controller('ExampleController', ['$cookieStore', function($cookieStore) {
  // Put cookie
  $cookieStore.put('myFavorite','oatmeal');
  // Get cookie
  var favoriteCookie = $cookieStore.get('myFavorite');
  // Removing a cookie
  $cookieStore.remove('myFavorite');
}]);
*/