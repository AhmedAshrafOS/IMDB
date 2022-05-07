
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

        [HttpGet]
        public ActionResult Search(string SearchString)
        {
            int i = 0;
            UserView userView = new UserView();
            var actor = db.Actors.Where(x => x.FirstName == SearchString || x.LastName == SearchString).ToList();

                if (actor != null)
                {
                    i++;
                userView.Actors = actor;
                }

            var Movie = db.Movies.Where(x => x.MovieName == SearchString).ToList();

                if (Movie != null)
                {
                    i++;
                userView.Movies = Movie;
                }
      
            var Director = db.Directors.Where(x => x.FirstName == SearchString ||  x.LastName == SearchString).ToList();

                if (Director != null)
                {
                    i++;
                userView.Directors = Director;
                }

            if (i == 0)
            {
                userView.notfound = "Your search " + SearchString + " did not match any documents.";
            }
            return View(userView);
        }


        [HttpGet]
        public ActionResult DirectorProfile(int? id)
        {
            using (AppDbContext db = new AppDbContext())
            {
                var Director = db.Directors.Find(id);
                var MovieDierctor = db.Movies.Where(x => x.DirectorId == id).ToList();
                MovieDierctors AllDierctors = new MovieDierctors
                {
                    Director = Director,
                    Movies = MovieDierctor,
                };

                return View(AllDierctors);
            }
        }

        public ActionResult ActorProfile(int id)
        {
            List<int> MovieList = new List<int>();
            var Actor = db.Actors.SingleOrDefault(x => x.ActorId == id);
            var MovieID = db.MovesActors.Where(x => x.ActorId == id).ToList();
            var Movies = db.Movies.ToList();
            foreach (var iteam in MovieID)
            {
                MovieList.Add((int)iteam.MovieId);
            }

            MovieDierctors ActorDetails = new MovieDierctors
            {
                Movies = Movies,
                Actor = Actor,

                Moviess = MovieList.ToArray(),
            };
            if (Actor.Image != null)
            {
                ActorDetails.Images = Actor.Image.Split(new char[] { ',' });
            }
            else
                ActorDetails.Images = null;
            if (ActorDetails == null)
            {
                return HttpNotFound();
            }
            return View(ActorDetails);
        }


        public ActionResult MovieProfile(int id)
        {
            int likecounter=0;
            int dislikecounter=0;
            List<int> ActorList = new List<int>();
            var movie = db.Movies.SingleOrDefault(x => x.MovieId == id);
            var Dierctor = db.Directors.SingleOrDefault(x => x.DirectorId == movie.DirectorId);
            var ActorID = db.MovesActors.Where(x => x.MovieId == id).ToList();
            var Actors = db.Actors.ToList();
            var comments = db.MoviesComments.Where(x => x.MovieId == id).ToList();
            var likechecker = db.UserMovies.Where(x => x.MovieId == id).ToList();
            foreach (var iteam in likechecker)
            {
                if (iteam.Like == true)
                {
                    likecounter++;
                }
                else if (iteam.disLike == true)
                {
                    dislikecounter++;
                }
            }
            movie.Like = likecounter;
            movie.Dislike = dislikecounter;
                foreach (var iteam in ActorID)
            {
                ActorList.Add((int)iteam.ActorId);
            }
            MovieDierctors MovieDetails = new MovieDierctors
            {
                Actors = Actors,
                Movie = movie,
                MoviesComments= comments,
                Actorss = ActorList.ToArray(),
                Director = Dierctor

            };
            if (Session["UserID"] != null) { 
                foreach (var iteam in likechecker)
                {
                    if (iteam.UserId == (int)Int16.Parse(Session["UserID"].ToString()))
                    {
                        if (iteam.Like == true)
                        {
                            MovieDetails.checkLike = true;
                        }
                        else if (iteam.disLike == true)
                        {
                            MovieDetails.checkdisLike = true;
                        }
                    }
                }
            }
            if (movie.Image != null)
            {
                MovieDetails.Images = movie.Image.Split(new char[] { ',' });
            }
            else
                MovieDetails.Images = null;
            if (MovieDetails == null)
            {
                return HttpNotFound();
            }
            return View(MovieDetails);
        }

        public ActionResult Comments(string comment, MovieDierctors MovieDierctors)
        {
            int id = MovieDierctors.Movie.MovieId;
            if (Session["UserID"] != null)
            {

                var usr = (int)Int16.Parse(Session["UserID"].ToString());
                var user = db.Users.Find(usr);
                MoviesComments MoviesComments = new MoviesComments()
                {
                    UserId = usr,
                    MovieId = MovieDierctors.Movie.MovieId,
                    Name=user.FirstName+" "+user.LastName,
                    comment = comment,

                };

                db.MoviesComments.Add(MoviesComments);
            }
            else
            {
                MoviesComments MoviesComments = new MoviesComments()
                {
                    MovieId = MovieDierctors.Movie.MovieId,
                    Name= "Anonymous",
                    comment = comment,

                };

                db.MoviesComments.Add(MoviesComments);
            }
            db.SaveChanges();
            return RedirectToAction("MovieProfile", new { id = id });
        }

        [HttpPost]
        public ActionResult Like(MovieDierctors MovieDierctors,string like)
        {
            int UserID = (int)Int16.Parse(Session["UserID"].ToString());
            var x = db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId);
            if (x != null)
            {

                if (x.Like == true)
                {
                    return Json(new { result = 0 });
                }
                else if (x.Like == false)
                {
                    db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId).disLike = false;
                    db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId).Like = true;
                    db.SaveChanges();

                    return Json(new { result = 1 });
                }
            }
            else
            {
                Models.UserMovies likes = new UserMovies()
                {
                    MovieId = MovieDierctors.Movie.MovieId,
                    UserId = UserID,
                    Like = true,
                    disLike=false,
                   
                };
                db.UserMovies.Add(likes);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            return Json(new { result = 0 });
        }

        public ActionResult DisLike(MovieDierctors MovieDierctors, string like)
        {
            int UserID = (int)Int16.Parse(Session["UserID"].ToString());
        var x = db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId);
            if (x != null)
            {

                if (x.disLike == true)
                {
                    return Json(new { result = 0 });
                }
                else if (x.disLike == false)
                {
                    db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId).disLike = true;
                    db.UserMovies.FirstOrDefault(a => a.UserId == UserID && a.MovieId == MovieDierctors.Movie.MovieId).Like = false;
                    db.SaveChanges();

                    return Json(new { result = 1 });
                }
            }
            else
            {
                Models.UserMovies likes = new UserMovies()
                {
                    MovieId = MovieDierctors.Movie.MovieId,
                    UserId = UserID,
                    disLike = true,
                    Like=false
                };
                db.UserMovies.Add(likes);
                db.SaveChanges();
                return Json(new { result = 1 });
            }
            return Json(new { result = 0 });
        }

    }





}