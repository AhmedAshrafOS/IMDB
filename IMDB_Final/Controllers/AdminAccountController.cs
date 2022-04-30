using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMDB_Final;
using IMDB_Final.Services;

namespace IMDB_Final.Models
{
    public class AdminAccountController : Controller
    {
        // GET: Admin/AdminAccount/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var adminService = new AdminServices();
            var isLoggedIn = adminService.Login(admin.AdminName, admin.Password);
            if (isLoggedIn)
            {
                return RedirectToAction("LoggedIn", "AdminAccount");
            }
            else
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                
            }

            return View();
        }
        public ActionResult LoggedIn()
        {
            return View();
        }
    }
}