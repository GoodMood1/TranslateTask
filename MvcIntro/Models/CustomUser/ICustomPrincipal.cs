using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MvcIntro.Models.CustomUser
{
    public interface ICustomPrincipal : IPrincipal
    {
        int UserID { get; set; }
    }
}
