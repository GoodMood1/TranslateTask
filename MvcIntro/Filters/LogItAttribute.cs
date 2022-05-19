using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Diagnostics;
using System.Web.Routing;

namespace MvcIntro.Filters
{
    public class LogItAttribute : FilterAttribute, IActionFilter
    {
        //After action executed
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            RouteValueDictionary routeInformation = filterContext.RouteData.Values;

            Debug.WriteLine($"Method {routeInformation["controller"]}/{routeInformation["action"]} is ended");
        }

        //Before action starts
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RouteValueDictionary routeInformation = filterContext.RouteData.Values;

            Debug.WriteLine($"Method {routeInformation["controller"]}/{routeInformation["action"]} is started");
        }
    }
}