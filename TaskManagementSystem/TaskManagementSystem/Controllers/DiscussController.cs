using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class DiscussController : Controller
    {
        // GET: Discuss
        TaskManagementSystemDB db = new TaskManagementSystemDB();
        public ActionResult CreateDiscuss()
        {
            return PartialView();
        }

        public JsonResult DiscussionCreate(Discuss DiscussData)
        {
            int UID = Convert.ToInt32(this.Session["UserProfile"].ToString()); //user.getSession();
            var dat = new List<object>();
            string msg = "";
            bool DiscussionCreateStatus = false;
            Discuss newDiscussion = new Discuss();
                try
                {
                    using (db) {
                      if (ModelState.IsValid) {
                        newDiscussion.DISCUSSION_TEXT = DiscussData.DISCUSSION_TEXT;
                        newDiscussion.DISCUSSION_USER_ID = UID;
                        newDiscussion.DISCUSSION_POST_DATE = DateTime.Now;
                        newDiscussion.DISCUSS_TITLE = DiscussData.DISCUSS_TITLE;
                        db.Discussions.Add(newDiscussion);
                        db.SaveChanges();
                        DiscussionCreateStatus = true;
                        msg = "Successfully posted";                                              
                    } else { msg = "Posting Failed!"; }
                }               
            }
            catch (Exception ex) {
                msg = ex.Message;
                dat.Add(new { ProjectStatus = DiscussionCreateStatus, Message = msg });
            }
            dat.Add(new { Message = msg, ProjectStatus = DiscussionCreateStatus});
            return new JsonResult { Data = dat };
        }
        public JsonResult AllDiscussion()
        {
            try
            {
                int value = db.Discussions.Count();
                if (value > 0)
                {
                    var DiscussionList = db.Discussions.OrderByDescending( a => a.DISCUSSION_POST_DATE).ToList();//(from d in db.Projects                                                    
                    return new JsonResult { Data = DiscussionList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public JsonResult AllDiscussionPosts(int id)
        {
            try
            {
                var Discussion = db.Discussions.Where(a => a.DISCUSSION_USER_ID.Equals(id)).FirstOrDefault();
                if (Discussion != null)
                {
                    var DiscussionList = db.Discussions.Where(a => a.DISCUSSION_USER_ID.Equals(id))
                    .OrderByDescending(a => a.DISCUSSION_POST_DATE)
                    .ToList();
                    return new JsonResult { Data = DiscussionList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = null, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult ViewDiscuss()
        {
            return PartialView();
        }


        public JsonResult ReplyDiscussion(DiscussionThread updateData)
        {
            int UID = Convert.ToInt32(this.Session["UserProfile"].ToString()); //user.getSession();
            var dat = new List<object>();
            string msg = "";
            bool DiscussionCreateStatus = false;
            DiscussionThread replyDiscussion = new DiscussionThread();
            try
            {
                using (db)
                {
                    if (ModelState.IsValid)
                    {
                        replyDiscussion.D_THREAD_POST_DATE =  DateTime.Now;
                        replyDiscussion.D_THREAD_TEXT = updateData.D_THREAD_TEXT;
                        replyDiscussion.D_THREAD_ROOT_ID = updateData.D_THREAD_ROOT_ID;
                        replyDiscussion.D_THREAD_USER_ID = UID;
                        db.DiscussionThreads.Add(replyDiscussion);
                        db.SaveChanges();
                        DiscussionCreateStatus = true;
                        msg = "Successfully posted";
                    }
                    else { msg = "Posting Failed!"; }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                dat.Add(new { ProjectStatus = DiscussionCreateStatus, Message = msg });
            }
            dat.Add(new { Message = msg, ProjectStatus = DiscussionCreateStatus });
            return new JsonResult { Data = dat };
        }
    }
}