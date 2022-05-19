using MvcIntro.Models.Entities;
using MvcIntro.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using MvcIntro.DAL;
using MvcIntro.Tools;
using MvcIntro.Filters;
using System.Threading;

namespace MvcIntro.Controllers
{
    public class AccountController : BaseController
    {
        // GET: User Account
        [Authorize]
        public ActionResult Index()
        {
            if (TempData["LastError"] != null)
                ViewBag.Error = TempData["LastError"];

            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        [FirstUserOnly]
        public ViewResult Secret()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                User userByName = _DAL.Users.ByName(userLoginModel.Name);

                //No such user
                if (userByName == null)
                {
                    ViewBag.Error = "No such user";

                    return View(userLoginModel);
                }
                else
                {
                    if (Hash.CheckPassword(userByName.PasswordHash, userLoginModel.Password))
                    {
                        Cookie.SetLoginCookie(userByName);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Wrong password";

                        return View(userLoginModel);
                    }
                }
            }
            else
            {
                return View(userLoginModel);
            }
        }

        public RedirectToRouteResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        //Show register form
        public ViewResult Register()
        {
            return View();
        }

        //Thin controller ideology
        [HttpPost]
        public ActionResult DoRegister(UserRegisterModel userRegisterModel)
        {
            //if (ModelState.IsValid)
            //{
            //}

                //if (ModelState.IsValid)
                //{
                //    //Check user duplication
                //    User userByName = _DAL.Users.ByName(userRegisterModel.Name);

                //    //No such user yet
                //    if (userByName == null)
                //    {
                //        User registeredUser = _DAL.Users.Register(userRegisterModel);

                //        if (registeredUser.UserID > 0)
                //        {
                //            Cookie.SetLoginCookie(registeredUser);

                //            return RedirectToAction("Index");
                //        }
                //        else
                //        {
                //            ViewBag.Error = "Error registering user.";

                //            return View(userRegisterModel);
                //        }
                //    }
                //    else 
                //    {
                //        ViewBag.Error = "This user is already registered.";

                //        return View(userRegisterModel);
                //    }
                //}
                //else
                //{
                //    return View("Register", userRegisterModel);
                //}

                //TODO: Remove it and uncomment all above
                return View("Register", userRegisterModel);
        }

        //Thick controller ideology
        [HttpPost]
        public ActionResult Register(UserRegisterModel userRegisterModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Entities())
                {
                    var searchResults = db.Users.Where(u => u.UserName == userRegisterModel.Name);

                    if (searchResults.Any())
                    {
                        ViewBag.Error = "This user is already registered.";

                        return View(userRegisterModel);
                    }
                    //if there's no such user
                    else
                    {
                        SHA512 hasher = SHA512.Create();

                        byte[] passwordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(userRegisterModel.Password));

                        User userToAdd = new User()
                        {
                            UserName = userRegisterModel.Name,
                            PasswordHash = passwordHash
                        };

                        try
                        {
                            db.Users.Add(userToAdd);
                            db.SaveChanges();

                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

                            string userData = jsSerializer.Serialize(new { UserID = userToAdd.UserID });

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                version: 1,
                                name: userToAdd.UserName,
                                issueDate: DateTime.Now,
                                expiration: DateTime.Now.AddMinutes(30),
                                isPersistent: true,
                                userData: userData
                                );

                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

                            authCookie.Expires = DateTime.Now.AddMinutes(30);

                            Response.Cookies.Add(authCookie);

                            return RedirectToAction("Index");
                        }
                        catch { }
                    }

                }

                //TODO: Register user
            }

            return View(userRegisterModel);
        }

    }
}