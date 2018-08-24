using CTS2019.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Models;

namespace CTS2019.Controllers
{
    public class BranchController : Controller
    {
        BankContext objBank = new BankContext();
        // GET: Branch
        [HttpGet]
        public ActionResult GetBranchDetails()
        {
            
            ViewBag.BranchList = objBank.GetBranchDetails();
            return View();
        }

        [HttpPost]
        public ActionResult CreateBranch(FormCollection collection,BranchModel branch)
        {
            try
            {
                string MessageStatus = objBank.AddBranch(branch);
                return RedirectToAction("GetBranchDetails");
            }
            catch
            {
                return View("GetBranchDetails");
            }
        }

    }
}