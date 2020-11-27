﻿using Glimpse.AspNet.Tab;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace EnterwellTask.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            else return RedirectToAction("../Account/Login");


        }
       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}