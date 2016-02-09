using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class OrganizationController : Controller
    {
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getOrganizationList() {
            int value = db.Organisations.Count();
            if (value > 0)
            {
                var orgList  = db.Organisations.ToList();
                return new JsonResult { Data = orgList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult getOrgDetail(int id)
        {
            try
            {
                using (TaskManagementSystemDB db = new TaskManagementSystemDB())
                {
                    int value = db.Organisations.Count();
                    if (value > 0)
                    {
                        var OrgDetails = db.Organisations.Where(a => a.ORG_ID.Equals(id)).FirstOrDefault();
                        return new JsonResult { Data = OrgDetails, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}