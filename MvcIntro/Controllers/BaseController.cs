using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIntro.Models.CustomUser;
using System.Globalization;
using System.Threading;
using System.Web.Routing;
using System.Configuration;

namespace MvcIntro.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CustomUser User
        {
            get { return HttpContext.User as CustomUser; }
        }

        private string[] availableCultures = new string[] 
        {
            "en", "uk", "es", "ru"
        };

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string defaultLang = ConfigurationManager.AppSettings["DefaultAppLang"];

            if (base.RouteData.Values.ContainsKey("lang"))
            {
                string langFromRoute = base.RouteData.Values["lang"].ToString();

                if (availableCultures.Contains(langFromRoute))
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(langFromRoute);
                else
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLang);
            }
            else
            {
                base.RouteData.Values.Add("lang", defaultLang);

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(defaultLang);
            }
        }
    }
}