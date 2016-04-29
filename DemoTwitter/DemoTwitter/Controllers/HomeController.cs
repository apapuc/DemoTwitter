﻿using System.Web.Mvc;
using DemoTwitter.BusinessLayer.Users;
using DemoTwitter.Models;

namespace DemoTwitter.Controllers
{
    public class HomeController : Controller
    {
        private IUserBL userRepository = new UserBL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid) return View(user);

            User userFromDatabase = userRepository.GetByUsername(user.Username);

            if (userFromDatabase == null) return View(user);
            GetUserId(userFromDatabase);
            Session["UserFullName"] = userFromDatabase.FirstName + " " + userFromDatabase.LastName;
            return RedirectToAction("Index", "User");
        }

        public int GetUserId(User user)
        {
            int userId;
            Session["UserID"] = user.Id;
            int.TryParse(Session["UserID"].ToString(), out userId);
            return userId;
        }
    }
}
