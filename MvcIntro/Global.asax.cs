using MvcIntro.Models.CustomUser;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MvcIntro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);     
        }

        protected void Application_PostAuthenticateRequest()
        {
            HttpCookie loginCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (loginCookie != null)
            {
                FormsAuthenticationTicket originalTicket = FormsAuthentication.Decrypt(loginCookie.Value);

                double cookieTimeout = Convert.ToDouble(ConfigurationManager.AppSettings["LoginTimeout"]);

                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                                    version: 1,
                                    name: originalTicket.Name,
                                    issueDate: DateTime.Now,
                                    expiration: DateTime.Now.AddMinutes(cookieTimeout),
                                    isPersistent: true,
                                    userData: originalTicket.UserData
                    );

                string encryptedTicket = FormsAuthentication.Encrypt(newTicket);

                loginCookie.Value = encryptedTicket;
                loginCookie.Expires = DateTime.Now.AddMinutes(cookieTimeout);

                Response.Cookies.Add(loginCookie);

                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                UserData userData = jsSerializer.Deserialize<UserData>(originalTicket.UserData);
                
                HttpContext.Current.User = new CustomUser(originalTicket.Name, userData.UserID);
            }
        }
    }
}