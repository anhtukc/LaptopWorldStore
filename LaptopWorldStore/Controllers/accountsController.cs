using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using LaptopWorldStore.Models;

namespace LaptopWorldStore.Controllers
{
    public class accountsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: accounts
        public ActionResult Index()
        {
            var accounts = db.accounts.Include(a => a.employee);
            return View();
        }

         [HttpGet]
        public ActionResult Index(string username, string password)
        {
            
            if (username != null)
            {
                if (Check(username, password) == "success")
                {
                    ViewBag.Fail = "";
                    Session["Username"] = username;
                    return RedirectToAction("Index", "products", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("FailSignin", "Sai tài khoản hoặc mật khẩu");
                }
            }
         

            return View();
        }
       public string Check(string username, string password)
        {
            account acc = new account();
          
             acc = db.accounts.Where(a => a.username.Equals(username) && a.password.Equals(password)).FirstOrDefault();
            if(acc != null)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
