using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ybm.UI.Infrastructure.Authorization;

namespace Ybm.UI.Controllers
{
    [TokenCategory(CategoryTitle = "Home Page", CategoryName = "HomePage")]
    public class HomeController : Controller
    {
        [ClaimBasedAuthorzation(TokenName = "View Home Page", ClaimType = "UserRight")]
        public ActionResult Index()
        {
            return View();
        }

        [ClaimBasedAuthorzation(TokenName = "Do Something", ClaimType = "UserRight")]
        public ActionResult DoSomething()
        {
            return View();
        }
    }
}