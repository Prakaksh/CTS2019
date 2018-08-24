using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Models;
using System.Configuration;
using System.Data.SqlClient;

using System.Data;
using CTS2019.Repositories;

namespace CTS2019.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                Repositories.AuthorizationContext objAuthorization = new Repositories.AuthorizationContext();
                AuthorizationModel Result = objAuthorization.GetAuthorization();
                string ProductKey = ConfigurationManager.AppSettings["ScreteKey"];
                string dfsdf = Result.AuthorizationID;
                if (Result.AuthorizationID == ProductKey.ToLower())
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Unauthorized");
                }
            }
            catch { }
            
        }
    } 
}