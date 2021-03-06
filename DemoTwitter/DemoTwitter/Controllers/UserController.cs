﻿using System;
 ﻿using System.Configuration;
 ﻿using System.Web;
 ﻿using System.Web.Mvc;
 ﻿using System.Web.Security;
 ﻿using DemoTwitter.BusinessLayer;
 ﻿using DemoTwitter.Models;
 ﻿using PagedList;
using System.Collections.Generic;
using System.Linq;
using DemoTwitter.WEB.Helpers;
using log4net;

namespace DemoTwitter.WEB.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private IUserBL userBl;
        private static ILog log = LogManager.GetLogger(typeof(UserController));
        private HashHelper hashHelper = new HashHelper();

        public UserController(IUserBL userBl)
        {
            this.userBl = userBl;
        }

        public ActionResult All(int? page, string searchString)
        {
            int pageNumber = page ?? 1;
            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            List<User> allUsers = userBl.GetAllUsers().ToList();
            List<User> final = new List<User>();

            int Id = (int)Session["UserID"];
            foreach (var currentUser in allUsers)
            {
                if (userBl.IsFollowed(Id, currentUser.Id ?? 0))
                {
                    currentUser.IsFollowed = true;
                    final.Add(currentUser);
                }
                else
                {
                    final.Add(currentUser);
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                final = final.Where(u => u.FirstName.Contains(searchString)
                                                || u.LastName.Contains(searchString)
                                                || u.Username.Contains(searchString)).ToList();
            }

            return View(final.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            var urlToRemove = Url.Action("Index", "User");
            if (urlToRemove != null) HttpResponse.RemoveOutputCacheItem(urlToRemove);
            log.Info("User logged out");
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult Edit(int userId)
        {
            User user = userBl.GetById(userId);
            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                log.Info("User updated his information");
                user.Password = hashHelper.CalculateMd5(user.Password);
                userBl.Update(user);
                return RedirectToAction("Index", "User");
            }
            return View(user);
        }

        public ActionResult Remove(int userId)
        {
            User user = new User
            {
                Id = userId
            };

            userBl.Remove(user);
            log.Info("User has removed his/her account");
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Follow(int? followedUserId)
        {
            User follower = new User
            {
                Id = (int?)Session["UserID"]
            };

            if (ModelState.IsValid)
            {
                userBl.Follow(follower.Id ?? 0, followedUserId ?? 0);
                if (follower.Id == 0 || followedUserId == 0)
                {
                    log.Info("User followed another user");
                    return RedirectToAction("All", "User");
                }
            }
            return RedirectToAction("All", "User");
        }

        public ActionResult UnFollow(int? followedUserId)
        {
            User follower = new User
            {
                Id = (int?)Session["UserID"]
            };

            if (ModelState.IsValid)
            {
                log.Info("User unfollowed somebody");
                userBl.UnFollow(follower.Id ?? 0, followedUserId ?? 0);
                if (follower.Id == 0 || followedUserId == 0)
                {
                    return RedirectToAction("All", "User");
                }
            }
            return RedirectToAction("All", "User");
        }

        public ActionResult Info(int userId)
        {
            return PartialView(userBl.GetAllUsers().FirstOrDefault(u => u.Id == userId));
        }
    }
}