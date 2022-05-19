using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcIntro.Models.Entities;

namespace MvcIntro.DAL
{

    public static partial class _DAL
    {
        public static class Blogs
        {
            public static List<Blog> ByUserID(int userID)
            {
                List<Blog> userBlogs = new List<Blog>();

                using (var db = new Entities())
                {
                    var searchResults = db.Blogs.Where(b => b.fk_UserID == userID);

                    if (searchResults.Any())
                    {
                        userBlogs = searchResults.ToList();
                    }

                }

                return userBlogs;
            }


            public static List<Blog> ByUserName(string userName)
            {
                List<Blog> userBlogs = new List<Blog>();

                using (var db = new Entities())
                {
                    //TODO: Think about optimization
                    var searchResults = db.Users.Where(u => u.UserName == userName);

                    if (searchResults.Any())
                    {
                        User userByName = searchResults.First();
                        userBlogs = userByName.Blogs.ToList();
                    }
                }

                return userBlogs;
            }

            public static bool Update(Blog changedBlog)
            {
                int numberOfUpdatedRecords = 0;

                using (var db = new Entities())
                {
                    var searchResults = db.Blogs.Where(b => b.BlogID == changedBlog.BlogID);

                    if (searchResults.Any())
                    {
                        Blog blogToUpdate = searchResults.First();

                        blogToUpdate.BlogContent = changedBlog.BlogContent;

                        numberOfUpdatedRecords = db.SaveChanges();
                    }
                }

                return numberOfUpdatedRecords > 0;
            }

            public static Blog Create(Blog createdBlog)
            {
                using (var db = new Entities())
                {
                    db.Blogs.Add(createdBlog);
                    db.SaveChanges();
                }

                return createdBlog;
            }

            public static Blog ByID(int blogID)
            {
                Blog blogByID = null;

                using (var db = new Entities())
                { 
                    //This allows JSON serialization
                    db.Configuration.LazyLoadingEnabled = false;

                    var searchResults = db.Blogs.Where(b => b.BlogID == blogID);

                    if (searchResults.Any())
                    {
                        blogByID = searchResults.First();
                    }
                }

                return blogByID;
            }

            public static int Delete(int blogID)
            {
                int numberOfDeletedRecords = 0;

                using (var db = new Entities())
                {
                    var searchResults = db.Blogs.Where(b => b.BlogID == blogID);

                    if (searchResults.Any())
                    {
                        Blog BlogToDelete = searchResults.First();

                        ICollection<Comment> blogComments = BlogToDelete.Comments;

                        if (blogComments.Any())
                        {
                            db.Comments.RemoveRange(blogComments);
                        }

                        db.Blogs.Remove(BlogToDelete);

                        numberOfDeletedRecords = db.SaveChanges();
                    }
                }

                return numberOfDeletedRecords;
            }
        }
    }

}