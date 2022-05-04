
using IMDB_Final.Models;
using IMDB_Final.ViewModels;
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
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult Delete (User user)
        //{

        //    var usr = db.Users.FirstOrDefault(u => u.UserName == user.UserName);
        //    if (usr != null) { 
        //    db.Users.Remove(usr);
        //    db.SaveChanges();
        //    }
        //    return View();
        //}
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/imgs/ProfileUser/"), pic);
                    file.SaveAs(path);

                    user.Image = pic;
                }
                if (db.Users.Any(k => k.UserName == user.UserName)|| db.Users.Any(k => k.Email == user.Email))
                    {
                         if(db.Users.Any(k => k.UserName == user.UserName))
                            {
                                ViewBag.UserExist = "This UserName Already Exist.";
                            }
                         else
                            ViewBag.EmailExist = "This Email Already Exist.";

                }

                else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        return RedirectToAction("Login");
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

  
                    var usr = db.Users.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();

                    if (usr == null)
                    {
                        ModelState.AddModelError("CustomError", "Username or Password is wrong");

                        return View("Login", user);
                    }
                    else
                    {
                        UserView User = new UserView()
                        {
                            User = usr
                        };
                        Session["UserID"] = usr.UserId.ToString();
                        Session["UserName"] = usr.UserName.ToString();
                        return View("MainPage", User);
                    }


            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult MainPage()
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
        [HttpPost]
        public ActionResult MainPage(UserView usr)
        {
            if (Session["UserID"] != null)
            {
                return View(usr);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

   

        public ActionResult UserProfile(int id)
        {
            if (Session["UserID"] != null)
            {
                var CurrentUser = db.Users.SingleOrDefault(x => x.UserId == id);

                UserView UserView = new UserView
                {
                   User= CurrentUser
                };

                if (UserView == null)
                {
                    return HttpNotFound();
                }
                return View(UserView);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
}