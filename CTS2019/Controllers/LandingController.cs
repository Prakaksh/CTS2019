using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTS2019.Controllers
{
    public class LandingController : Controller
    {
        // GET: Landing
        public ActionResult LandingPage()
        {
            return View();
        }
    }
}