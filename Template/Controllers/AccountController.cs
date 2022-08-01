using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.Models;

namespace Template.Controllers
{
    public class AccountController : Controller
    {
        Database1Entities db = new Database1Entities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Table tb)
        {
            var login = db.Tables.Where(a => a.Username == tb.Username && a.Password == tb.Password).FirstOrDefault();
            if(login != null)
            {
                Session["userId"] = login.Id;
                Session["uname"] = login.Username;
                if (login.Role_Id == 1)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.er = "Username or Password is incorrect";
            }
            return View();
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Table tb)
        {
            if (ModelState.IsValid)
            {
                tb.Role_Id = 2;
                db.Tables.Add(tb);
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["userId"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}