using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcIntro.Controllers
{
    public class HomeController : BaseController
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ViewResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult Error()
        {
            return View();
        }

        [Route(template: "Home/RouteManyParams/userID/{userID}/blogID/{blogID}/commentID/{commentID}", Name = "3ParamsRoute")]
        public ViewResult RouteManyParams(int userID, int blogID, int commentID)
        {
            return View("Index");
        }

        [Route("ActionLevel")]
        public ViewResult ActionLevelRoute()
        {
            return View("Index");
        }
    }
}