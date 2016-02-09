using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem.Controllers
{
    public class RoutesDemoController : Controller
    {
        // GET: RoutesDemo

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult One()
        {
            return View();
        }

      

        public ActionResult Two(int donuts = 1)
        {
            ViewBag.Donuts = donuts;
            return PartialView();
        }

        public ActionResult Three()
        {
            return PartialView();
        }

        public ActionResult Four()
        {
            return View();
        }

    }
}