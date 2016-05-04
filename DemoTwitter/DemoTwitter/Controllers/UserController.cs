﻿using System.Linq;
using System.Web.Mvc;
using DemoTwitter.BusinessLayer.Users;
using DemoTwitter.Models;
using DemoTwitter.WEB.Helpers;

namespace DemoTwitter.WEB.Controllers
{
    public class UserController : Controller
    {
        HashHelper hashHelper = new HashHelper();
        IUserBL userBl = new UserBL();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = hashHelper.CalculateMd5(user.Password);
                userBl.Register(user);
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult ShowAllUsers()
        {
            return View(userBl.GetAll());
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
