using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using MvcIntro.Models.CustomUser;

namespace MvcIntro.Filters
{
    public class FirstUserOnlyAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (
                !filterContext.HttpContext.User.Identity.IsAuthenticated
                || (filterContext.HttpContext.User as CustomUser).UserID != 1
               )
            {
                //Way #1
                //RouteValueDictionary redirectPathParts = new RouteValueDictionary();

                //redirectPathParts.Add("controller", "Home");
                //redirectPathParts.Add("action", "Error");

                //filterContext.Result = new RedirectToRouteResult(redirectPathParts);

                filterContext.Result = new RedirectToRouteResult("Error", null);

            }
        }
    }
}