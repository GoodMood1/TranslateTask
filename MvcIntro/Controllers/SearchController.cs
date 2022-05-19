using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIntro.DAL;
using MvcIntro.Models.Entities;

namespace MvcIntro.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string searchString = formCollection["SearchString"];

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();

                List<Blog> blogsByUserName = _DAL.Blogs.ByUserName(searchString);

                return View("SearchResults", blogsByUserName);
            }

            return View();
        }

    }
}