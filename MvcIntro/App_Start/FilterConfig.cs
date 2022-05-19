using System.Web;
using System.Web.Mvc;
using MvcIntro.Filters;

namespace MvcIntro
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Makes filter global. Applies to any controller and action
            filters.Add(new LogItAttribute());
        }
    }
}
