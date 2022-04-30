
using IMDB_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB_Final.Controllers
{
    public class UserController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Profle()
        {
            return View();
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (AppDbContext db = new AppDbContext())
                {
                    if (db.Users.Any(k => k.UserName == user.UserName))
                    {
                        //ModelState.AddModelError("User", "This UserName Already Exist");
                        ViewBag.UserExist = "This UserName Already Exist.";
                    }
                    if (db.Users.Any(k => k.Email == user.Email))
                    {
                        //ModelState.AddModelError("User", "This Email Already Exist");
                        ViewBag.EmailExist = "This Email Already Exist.";
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                }
            }

            return View(user);
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {

            if (ModelState.IsValidField("Username") && (ModelState.IsValidField("Password")))
            {
                using (AppDbContext db = new AppDbContext())
                {
                    User usr = null;
                    usr = db.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();

                    if (usr == null)
                    {
                        ModelState.AddModelError("CustomError", "Username or Password is wrong");

                        return View("Login", user);
                    }
                    else
                    {
                        Session["UserID"] = usr.UserId.ToString();
                        Session["UserName"] = usr.UserName.ToString();
                        return RedirectToAction("LoggedIn");
                    }


                }
            }
            else
            {
                return View();
            }
        }


        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


    }
}