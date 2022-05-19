using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MvcIntro.Models.CustomUser
{
    public class CustomUser : ICustomPrincipal
    {
        public int UserID { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }

        public CustomUser(string userName, int userID)
        {
            Identity = new GenericIdentity(userName);
            UserID = userID;
        }
    }
}