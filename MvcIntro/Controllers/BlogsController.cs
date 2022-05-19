using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIntro.DAL;
using MvcIntro.Models.Entities;
using System.Web.Security;
using System.Web.Script.Serialization;
using MvcIntro.Models.CustomUser;
using System.IO;

namespace MvcIntro.Controllers
{
    public class BlogsController : BaseController
    {
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Blog newBlog)
        {
            if (ModelState.IsValid)
            {
                newBlog.fk_UserID = User.UserID;
                newBlog.BlogDateTime = DateTime.Now;

                Blog createdBlog = _DAL.Blogs.Create(newBlog);

                //Possible check
                //createdBlog.BlogID > 0

                return RedirectToAction("Index", "Account");
            }

            return View(newBlog);
        }

        public RedirectToRouteResult Delete(int blogID)
        {
            int numberOfDeletedBlogs = _DAL.Blogs.Delete(blogID);

            if (numberOfDeletedBlogs == 0)
                TempData["LastError"] = "No such blog to delete";

            return RedirectToAction("Index", "Account");  
        }

        //Use CORS policy setup in your real projects
        //Don't allow anonymous usage
        //[AllowAnonymous]
        public JsonResult DeleteJS(int blogID)
        {
            int numberOfDeletedBlogs = _DAL.Blogs.Delete(blogID);

            return Json(numberOfDeletedBlogs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveUpdatedJS()
        {
            string jsonRequest = new StreamReader(Request.InputStream).ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();  

            Blog updatedBlog = serializer.Deserialize<Blog>(jsonRequest);

            bool isUpdated = _DAL.Blogs.Update(updatedBlog);

            return Json(isUpdated);
        }

        public ViewResult Details(int blogID)
        {
            Blog blogByID = _DAL.Blogs.ByID(blogID);

            if(blogByID == null)
                TempData["LastError"] = "No such blog to view";

            return View(blogByID);
        }

        public ViewResult Edit(int blogID)
        {
            Blog blogByID = _DAL.Blogs.ByID(blogID);

            if (blogByID == null)
                TempData["LastError"] = "No such blog to edit";

            return View(blogByID);
        }

        public JsonResult EditJS(int blogID)
        {
            Blog blogByID = _DAL.Blogs.ByID(blogID);

            return Json(blogByID, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Edit(Blog updatedBlog)
        {
            //TODO: Подумать над защитой от подмены ключей объекта
            //BlogID, fk_UserID через ручное редактирование hidden-полей формы
            //TODO: Перевести операции создания и редактирования блога на вью-модели с метками валидации
            //Разработать мапперы которые преобразуют эти вью-модели к чистым сущностям Blog

            if (ModelState.IsValid)
            {
                _DAL.Blogs.Update(updatedBlog);

                return RedirectToAction("Index", "Account");
            }
            else
            {
                return View(updatedBlog);
            }
        }

        [Authorize]
        [ChildActionOnly]
        public PartialViewResult UserBlogs()
        {
            //Way #1
            //string currentUserName = User.Identity.Name;
            //List<Blog> userBlogs = _DAL.Blogs.ByUserID(_DAL.Users.ByName(currentUserName).UserID);

            //Way #2
            //HttpCookie loginCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(loginCookie.Value);
            //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            //UserData userData = jsSerializer.Deserialize<UserData>(ticket.UserData);
            //List<Blog> userBlogs = _DAL.Blogs.ByUserID(userData.UserID);

            //Way #3
            List<Blog> userBlogs = _DAL.Blogs.ByUserID(User.UserID);
            return PartialView(userBlogs);
        }
    }

    //Way #2
    //public class UserData
    //{
    //    public int UserID { get; set; }
    //}
}