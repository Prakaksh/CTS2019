//using CTS2019.Models;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;

//namespace CTS2019.Filters
//{
//    public class AuthenticationFilter: ActionFilterAttribute
//    {
//        //public void OnAuthenticated(AuthenticationContext filterContext)
//        //{
//        //    //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
//        //    if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserID"])))
//        //    {
//        //        filterContext.Result = new HttpUnauthorizedResult();
//        //    }
//        //}

//        //public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
//        //{
//        //    //We are checking Result is null or Result is HttpUnauthorizedResult 
//        //    // if yes then we are Redirect to Error View
//        //    if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
//        //    {
//        //        filterContext.Result = new ViewResult
//        //        {
//        //            ViewName = "Error"
//        //        };
//        //    }
//        //}


//        public override void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            try
//            {
//                LoginModel objLogin = new LoginModel();
//                string SuperAdminUserName = "HCSAdmin";
//                string SuperAdminPassword = "HCSInfo@758";
//                if ((objLogin.UserName.ToLower() == SuperAdminUserName.ToLower()) && (objLogin.Password == SuperAdminPassword))
//                {
//                    base.OnActionExecuted(filterContext);
//                }
//                else
//                {
//                    filterContext.Result = new RedirectResult("~/Home/Unauthorized");
//                }
//            }
//            catch { }

//        }
//    }
//}