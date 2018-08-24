using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTS2019.Models;
using CTS2019.Repositories;

namespace CTS2019.Controllers
{
    public class BankController : Controller
    {
        BankContext objBank = new BankContext();
        [HttpGet]
        public ActionResult GetBankDetails()
        {
            //BankContext objBank = new BankContext();
            //var model = new List<Team>();
            ViewBag.BankList= objBank.GetBankDetails();
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateBank(FormCollection collection,BankModel bank)
        {
           
            try
            {
            
                string MessageStatus = objBank.AddBank(bank);
                // TODO: Add insert logic here

                return RedirectToAction("GetBankDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bank/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bank/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bank/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bank/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
