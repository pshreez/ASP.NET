using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TaskManagementSystem.Models;


namespace TaskManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        public ActionResult Index()
        {
         
           return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return PartialView();
        }
        public ActionResult Menu()
        {
            return PartialView();
        }

        public ActionResult Home()
        {


            return PartialView();
        }
        public ActionResult ManagerHome()
        {


            return PartialView();
        }

        public ActionResult DeveloperHome()
        {


            return PartialView();
        }






    }
}