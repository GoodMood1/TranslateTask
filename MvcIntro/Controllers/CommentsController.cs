using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIntro.DAL;
using MvcIntro.Models.Entities;

namespace MvcIntro.Controllers
{
    public class CommentsController : BaseController
    {
        public ActionResult Comments(int blogID)
        {
            Blog blogByID = _DAL.Blogs.ByID(blogID);

            return View(blogByID);
        }

        [ChildActionOnly]
        public PartialViewResult BlogComments(int blogID)
        {
            List<Comment> blogComments = _DAL.Comments.ByBlogID(blogID);

            return PartialView(blogComments);
        }

    }
}