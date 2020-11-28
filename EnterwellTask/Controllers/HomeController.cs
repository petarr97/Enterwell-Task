using Glimpse.AspNet.Tab;
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
    [Authorize]

    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ActionResult Index()
        {
            return RedirectToAction("../Fakturas");
        }
      
    }
}