
using IMDB_Final.Models;
using IMDB_Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                        return RedirectToAction("MainPage");
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
            List<Movie> Movie = db.Movies.ToList();
            List<Director> Director = db.Directors.ToList();
            List<Actor> Actor = db.Actors.ToList();

            UserView UserView = new UserView
            {
                Movies = Movie,
                Directors=Director,
                Actors=Actor,
            };

            return View(UserView);

        }



        public ActionResult UserProfile()
        {
            if (Session["UserID"] != null)
            {
                var id = Session["UserID"].ToString();
                var usr = db.Users.Find(Int16.Parse(id));
                List<int> ActorList = new List<int>();
                List<int> MovieList = new List<int>();
                List<int> DirectorList = new List<int>();
                var ActorID = db.UserActors.Where(x => x.UserId == usr.UserId).ToList();
                var MovieID = db.UserMovies.Where(x => x.UserId == usr.UserId).ToList();
                var DirectorID = db.UserDirector.Where(x => x.UserId == usr.UserId).ToList();
                var Actors = db.Actors.ToList();
                var Directors = db.Directors.ToList();
                var Movies = db.Movies.ToList();
                foreach (var iteam in ActorID)
                {
                    ActorList.Add((int)iteam.ActorId);
                }
                foreach (var iteam in MovieID)
                {
                    MovieList.Add((int)iteam.MovieId);
                }
                foreach (var iteam in DirectorID)
                {
                    DirectorList.Add((int)iteam.DirectorId);
                }
                UserView UserView = new UserView
                {
                   User= usr,
                   Actors= Actors,
                   Directors= Directors,
                   Movies= Movies,
                   Actorss= ActorList.ToArray(),
                    Moviess = MovieList.ToArray(),
                  Directorss = DirectorList.ToArray(),
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
        [HttpGet]
        public ActionResult EditProfile(int? id)
        {
            if (id != null)
            {
                List<int> ActorList = new List<int>();
                List<int> MovieList = new List<int>();
                List<int> DirectorList = new List<int>();
                var user = db.Users.SingleOrDefault(x => x.UserId == id);
                //couldbe null
                var ActorID = db.UserActors.Where(x => x.UserId == id).ToList();
                var MoviesID = db.UserMovies.Where(x => x.UserId == id).ToList();
                var DirectorID = db.UserDirector.Where(x => x.UserId == id).ToList();
                //couldbe null
                var Dierctor = db.Directors.ToList();
                var Actors = db.Actors.ToList();
                var Movies = db.Movies.ToList();

                    foreach (var iteam in ActorID)
                {
                    if (iteam != null)
                    {
                        ActorList.Add((int)iteam.ActorId);
                    }
                }

                if (MoviesID != null)
                {
                    foreach (var iteam in MoviesID)
                    {
                        MovieList.Add((int)iteam.MovieId);
                    }
                }
                if (DirectorID != null)
                {
                    foreach (var iteam in DirectorID)
                    {
                        DirectorList.Add((int)iteam.DirectorId);
                    }
                }
                if (user == null)
                {
                    return HttpNotFound();
                }

         

                UserView UserView = new UserView
                {
                    User=user,
                    Actors = Actors,
                    Movies = Movies,
                    Directors = Dierctor,
                    //Actorss = ActorList.ToArray(),
                    //Moviess= MovieList.ToArray(),
                    //Directorss= DirectorList.ToArray(),
                };

                return View(UserView);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditProfile(UserView UserView, HttpPostedFileBase file)
        {

                var User = db.Users.SingleOrDefault(a => a.UserId == UserView.User.UserId);
                var test2 = UserView.Moviess;
                User.FirstName = UserView.User.FirstName;
                User.LastName = UserView.User.LastName;
                User.Email = UserView.User.Email;
                User.PhoneNumber = UserView.User.PhoneNumber;
                User.Address = UserView.User.Address;
                 User.Image= UserView.User.Image;
            if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/imgs/ProfileUser/"), pic);
                    file.SaveAs(path);
                    User.Image = pic;
                }

                    var UserMovie = db.UserMovies.Where(u => u.UserId == UserView.User.UserId).ToList();
                    foreach (var item in UserMovie)
                    {
                        if (item != null) {
                            db.UserMovies.Remove(item);
                        }
                        
                    }


                    foreach (var item in UserView.Moviess)
                    {
                        UserMovies UserMovies = new UserMovies()
                        {
                            MovieId = item,
                            UserId = UserView.User.UserId,
                        };
                        db.UserMovies.Add(UserMovies);
                    }

                var UserActor = db.UserActors.Where(u => u.UserId == UserView.User.UserId).ToList();

               foreach (var item in UserActor)
                 {
                if (item != null)
                {
                    db.UserActors.Remove(item);

                    }
                }
                if (UserView.Actorss != null)
                {
                    foreach (var item in UserView.Actorss)
                    {
                        UserActors UserActors = new UserActors()
                        {
                            ActorId = item,
                            UserId = UserView.User.UserId,
                        };
                        db.UserActors.Add(UserActors);
                    }
                }
                var UserDirector = db.UserDirector.Where(u => u.UserId == UserView.User.UserId).ToList();

                    foreach (var item in UserDirector)
                    {
                        if (item != null)
                        {
                            db.UserDirector.Remove(item);
                        }
                    }

                    foreach (var item in UserView.Directorss)
                    {
                        if (item != null)
                        {  //MovieActors.MovieId = movieDierctors.Movie.MovieId;
                            UserDirector UserDirectors = new UserDirector()
                            {
                                DirectorId = item,
                                UserId = UserView.User.UserId,
                            };
                            db.UserDirector.Add(UserDirectors);
                        }
                    }
                db.Entry(User).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("UserProfile");

        }

        public ActionResult Search(string txt)
        {
            //UserView userView = new UserView();
            //var actor = db.Actors.Where(x => x.FirstName == txt).ToList();
            //foreach(var item in actor)
            //{
            //    if (item != null)
            //    {
            //        userView.Actors.Add(item);
            //    }
            //}
            //var Movie = db.Movies.Where(x => x.MovieName == txt).ToList();
            //foreach (var item in Movie)
            //{
            //    if (item != null)
            //    {
            //        userView.Actors.Add(item);
            //    }
            //}
            //var actor = db.Actors.Where(x => x.FirstName == txt).ToList();
            //foreach (var item in actor)
            //{
            //    if (item != null)
            //    {
            //        userView.Actors.Add(item);
            //    }
            //}
            return View();
        }
    }
}