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
        public ActionResult Login(LoginUserModel user)
        {
            if (ModelState.IsValid)
            {
                User userFromDatabase = userRepository.GetByUsername(user.Username);
                GetUserId(userFromDatabase);
                Session["UserFullName"] = userFromDatabase.FirstName + " " + userFromDatabase.LastName;
                return RedirectToAction("Index", "User");
            }
            return View();


            //    return View();

            //User userFromDatabase = userRepository.GetByUsername(user.Username);

            //if (userFromDatabase == null)
            //{
            //    return View(user);
            //}
            //else
            //{
            //    GetUserId(userFromDatabase);
            //    Session["UserFullName"] = userFromDatabase.FirstName + " " + userFromDatabase.LastName;
         //    return RedirectToAction("Index", "User");
            //}
        }

        //if (ModelState.IsValid)
        //   {
        //       user.Password = hashHelper.CalculateMd5(user.Password);
        //       userBl.Register(user);
        //       return View();
        //   }
        //   else
        //   {
        //       return View(user);
        //   }

        public int GetUserId(User user)
        {
            int userId;
            Session["UserID"] = user.Id;
            int.TryParse(Session["UserID"].ToString(), out userId);
            return userId;
        }
    }
}
