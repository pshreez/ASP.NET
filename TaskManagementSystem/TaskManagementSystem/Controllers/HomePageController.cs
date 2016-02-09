using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MainMenu()
        {
            return PartialView();
        }

        public ActionResult Project()
        {
            return PartialView();
        }
        public ActionResult Task()
        {
            return PartialView();
        }
        public ActionResult Users()
        {
            return PartialView();
        }
        public ActionResult UserHome()
        {
            return PartialView();
        }
        public ActionResult Discuss()
        {
            return PartialView();
        }
    }
}