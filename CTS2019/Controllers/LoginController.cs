using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Filters;
using CTS2019.Models;

namespace CTS2019.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AuthorizationFilter]
        [HttpGet]

        public ActionResult Index()
        {

            return View();
        }

        // GET: Login
        [AuthorizationFilter]
        [HttpPost]
       
        public ActionResult Index(LoginModel model)
        {
          
            //if (model.UserName == "hcsadmin" && model.Password == "12345")
            //{
            //    Session["UserID"] = Guid.NewGuid();
            //}

            return View();
        }

        public ActionResult Index1()
        {

            return View();
        }
    }
}