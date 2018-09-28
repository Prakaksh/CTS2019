using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CTS2019.Filters;
using CTS2019.Models;
using CTS2019.Repositories;

namespace CTS2019.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        // [AuthorizationFilter]
        [HttpGet]
        public ActionResult LoginPage()
        {

            return View();
        }

        // GET: Login
        //[AuthorizationFilter]
        [HttpPost]
        public ActionResult LoginPage(Login objlogin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    LoginContext obj = new LoginContext();

                    UserInfo objUser = obj.GetLogin(objlogin);
                    if (objUser != null && objUser.Status != "0")
                    {
                        //Forms Authentication
                        FormsAuthentication.SetAuthCookie(objUser.UserID, false);
                        Session["UserID"] = objUser.UserID.ToString();
                        //Session["OrganizationId"] = objUser.OrganizationID;
                        Session["UserName"] = objUser.FirstName + " " + objUser.LastName;
                        Session["RoleName"] = objUser.RoleName;
                        Session["RoleTypeID"] = objUser.RoleTypeID;

                        switch (objUser.RoleName)
                        {
                            case "Admin":
                                return RedirectToAction("AdminDashboard", "Home");

                            case "Manager":
                                return RedirectToAction("OutwardPage", "Outward");

                            case "SuperAdmin":
                                return RedirectToAction("SuperAdminDashboard", "Home");
                            case "0":
                                ViewBag.Message = "Please enter valid credentials!";
                                return View();

                            default:
                                return RedirectToAction("Login", "Login");
                        }

                    }

                    else
                    {
                        ViewBag.Message = "Please enter valid credentials!";
                    }
                    return View();
                }

                catch (Exception ex)
                {

                }
            }
            else
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            Session.RemoveAll();
            Session["UserID"] = null;
            return RedirectToAction("LoginPage");
            //return View("LoginPage");
        }


  
    }




}
