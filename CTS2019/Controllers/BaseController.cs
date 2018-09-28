using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Models;

namespace CTS2019.Controllers
{

    public class BaseController : Controller
    {
        public UserInfo UserInfoGet()
        {
            try
            {
                UserInfo obj = new UserInfo();
                if (Session != null)
                {
                    obj.UserID = Session["UserID"].ToString();
                    obj.OrganizationID = Session["OrganizationID"].ToString();
                    obj.UserName = (string)Session["UserName"];
                    obj.RoleName = (string)Session["RoleName"];
                    obj.RoleTypeID = Session["RoleTypeID"].ToString();
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
