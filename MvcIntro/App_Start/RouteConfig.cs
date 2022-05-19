using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcIntro
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Scan controllers and actions for [Route] attributes (decorators)
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Error",
                url: "Error",
                defaults: new { controller = "Home", action = "Error" }
                );

            //routes.MapRoute(
            //    name: "3ParamsRoute",
            //    url: "Home/RouteManyParams/userID/{userID}/blogID/{blogID}/commentID/{commentID}",
            //    defaults: new { 
            //        controller = "Home", 
            //        action = "RouteManyParams"
            //        }
            //    );

            routes.MapRoute(
                name: "Comments",
                url: "Comments/{action}/{commentID}",
                defaults: new { controller = "Comments", commentID = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Blogs",
                url: "Blogs/{action}/{blogID}",
                defaults: new { controller = "Blogs", blogID = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional,
                    lang = ConfigurationManager.AppSettings["DefaultAppLang"]
                }
                );
        }
    }
}
