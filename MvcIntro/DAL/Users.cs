using MvcIntro.Models.Entities;
using MvcIntro.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcIntro.Tools;

namespace MvcIntro.DAL
{
    public static partial class _DAL
    {
        public static class Users
        {
            public static User ByName(string userName)
            {
                User user = null;

                using (var db = new Entities())
                {
                    var searchResults = db.Users.Where(u => u.UserName == userName);

                    if (searchResults.Any())
                    {
                        user = searchResults.First();
                    }
                }

                return user;
            }

            public static User Register(UserRegisterModel userRegisterModel)
            {
                byte[] passwordHash = Hash.CreateHash_SHA512(userRegisterModel.Password);

                User userToAdd = new User()
                {
                    UserName = userRegisterModel.Name.ToLower(),
                    PasswordHash = passwordHash
                };

                using (var db = new Entities())
                {
                    db.Users.Add(userToAdd);
                    db.SaveChanges();
                }

                return userToAdd;
            }
        }
    }
}