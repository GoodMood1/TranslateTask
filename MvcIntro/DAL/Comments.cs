using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcIntro.Models.Entities;

namespace MvcIntro.DAL
{
    public static partial class _DAL 
    {
        public static class Comments
        {
            public static List<Comment> ByBlogID(int blogID)
            {
                List<Comment> blogComments = new List<Comment>();

                using (var db = new Entities())
                {
                    var searchResults = db.Comments.Include("User").Where(c => c.fk_BlogID == blogID);

                    if (searchResults.Any())
                    {
                        blogComments = searchResults.ToList();
                    }
                }

                return blogComments;
            }
        }
    }
}