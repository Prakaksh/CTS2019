using System.Web.Mvc;
using CTS2019.Filters;
namespace CTS2019.Controllers
{
    [SessionTimeout]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportPage()
        {
            return View();
        }
    }
}