using MvcIntro.Models.Entities;
using System;
using System.Configuration;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MvcIntro.Tools
{
    public static class Cookie
    {
        public static void SetLoginCookie(User loggedInUser)
        {
            double cookieTimeout = Convert.ToDouble(ConfigurationManager.AppSettings["LoginTimeout"]);

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            string userData = jsSerializer.Serialize(new { UserID = loggedInUser.UserID });

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                version: 1,
                name: loggedInUser.UserName,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(cookieTimeout),
                isPersistent: true,
                userData: userData
                );

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            authCookie.Expires = DateTime.Now.AddMinutes(cookieTimeout);

            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}