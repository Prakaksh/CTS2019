using System.Web;
using System.Web.Mvc;

namespace CTS2019.Filters
{
    public class SessionTimeoutAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["UserID"] == null)
            {
                //Session need to be killed if exists..
                filterContext.Result = new RedirectResult(AppUtility.AppUtility.AppSettingsGet("LoginUrl")); //Read from config file.. ok....
                //We'll use static call to read Config file settings okay. Wee need to pass the key only..ok...
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}