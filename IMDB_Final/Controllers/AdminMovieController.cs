using IMDB_Final.Models;
using IMDB_Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IMDB_Final.Controllers
{
    public class AdminMovieController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: AdminMovie
        [HttpGet]
        public ActionResult Index()
        {
            List<Movie> Movie = db.Movies.ToList();
            return View(Movie);
        }

        [HttpGet]
        public ActionResult CreateMovie()
        {
            var Director = db.Directors.ToList();
            var Actor = db.Actors.ToList();
            MovieDierctors movieDierctors = new MovieDierctors
            {
                Directors = Director,
                Actors=Actor,
            };
      

            //ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "FirstName");

            return View(movieDierctors);
        }
        [HttpPost]
        public ActionResult CreateMovie(MovieDierctors movieDierctors,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/imgs/ProfileMovie/"), pic);

                    file.SaveAs(path);
                    movieDierctors.Movie.Image = pic;
                }
                db.Movies.Add(movieDierctors.Movie);
           
                foreach (var item in movieDierctors.Actorss)
                {
                    MovesActors MovesActors = new MovesActors()
                    {

                        MovieId = movieDierctors.Movie.MovieId,

                        ActorId = item
                    };

                    db.MovesActors.Add(MovesActors);

                }
                db.SaveChanges();

                //movieDierctors.Movie.Actors = movieDierctors.Actors;


                return RedirectToAction("Index");

            }
            return View();


        }


        public ActionResult DetailsMovie(int id)
        {
            List<int> ActorList = new List<int>();
            var movie = db.Movies.SingleOrDefault(x => x.MovieId == id);
            var Dierctor = db.Directors.SingleOrDefault(x => x.DirectorId == movie.DirectorId);
            var ActorID = db.MovesActors.Where(x => x.MovieId == id).ToList();
            var Actors = db.Actors.ToList();
            foreach (var iteam in ActorID)
            {
                ActorList.Add((int)iteam.ActorId);
            }
            MovieDierctors MovieDetails = new MovieDierctors
            {
                Actors=Actors,
                Movie=movie,
                Actorss= ActorList.ToArray(),
                Director= Dierctor

            };
            if (MovieDetails == null)
            {
                return HttpNotFound();
            }
            return View(MovieDetails);
        }


        [HttpGet]

        public ActionResult EditMovie(int? id)
        {
            if (id != null)
            {
                List<int> ActorList = new List<int>();
                var Movie = db.Movies.SingleOrDefault(x => x.MovieId == id);
                var ActorID = db.MovesActors.Where(x => x.MovieId == id).ToList();
                var Dierctor = db.Directors.ToList();
                var Actors = db.Actors.ToList();
                foreach (var iteam in ActorID)
                {
                    ActorList.Add((int)iteam.ActorId);
                }
                if (Movie == null)
                {
                    return HttpNotFound();
                }

                var Directors = db.Directors.ToList();

                MovieDierctors movieDierctors = new MovieDierctors
                {
                    Actors = Actors,
                    Movie = Movie,
                    Directors = Dierctor,
                    Actorss = ActorList.ToArray(),
                };

                return View(movieDierctors);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditMovie(MovieDierctors movieDierctors, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var Movie = db.Movies.SingleOrDefault(a => a.MovieId == movieDierctors.Movie.MovieId);

                Movie.MovieName= movieDierctors.Movie.MovieName;
                Movie.Like = movieDierctors.Movie.Like;
                Movie.Dislike = movieDierctors.Movie.Dislike;
                Movie.DirectorId = movieDierctors.Movie.DirectorId;
                Movie.Image = movieDierctors.Movie.Image;


                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/imgs/ProfileMovie/"), pic);

                    file.SaveAs(path);
                    Movie.Image = pic;
                }


                var MovieActors = db.MovesActors.Where(u => u.MovieId == movieDierctors.Movie.MovieId).ToList();
                foreach (var item in MovieActors)
                {
                    db.MovesActors.Remove(item);
                }
                foreach (var item in movieDierctors.Actorss)
                {


                    //MovieActors.MovieId = movieDierctors.Movie.MovieId;

                        MovesActors MovesActors = new MovesActors()
                        {

                            MovieId = movieDierctors.Movie.MovieId,

                            ActorId = item
                        };

                        db.MovesActors.Add(MovesActors);

                }
                db.Entry(Movie).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DetailsMovie", new { id = Movie.MovieId });
            }
            return View();
        }

        // POST: Director/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Movie movie = db.Movies.Find(id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        // POST: Director/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Movie movie)
        {
            try
            {
                var MovieActors = db.MovesActors.Where(u => u.MovieId == movie.MovieId).ToList();
                foreach (var item in MovieActors)
                {
                    db.MovesActors.Remove(item);
                }
                if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    movie = db.Movies.Find(id);
                    if (movie == null)
                        return HttpNotFound();
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
   

            }
            catch
            {
                return View();
            }
        }


        //public ActionResult Delete(int? id)
        //{
        //    if (id != null)
        //    {
        //        var Movie = db.Movies.Find(id);

        //        var MovieInfo = new Movie
        //        {
        //            MovieId = Movie.MovieId,
        //            MovieName = Movie.MovieName,
        //            Image = Movie.Image,
        //            Like = Movie.Like,
        //            Dislike = Movie.Dislike,
        //            DirectorId = Movie.DirectorId,
        //        };

        //        return View(MovieInfo);
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpPost]
        //public ActionResult DeleteConfirmed(int? id)
        //{
        //    if (id != null)
        //    {
        //        var Movie = db.Movies.Find(id);

        //        if (Movie != null)
        //        {
        //            db.Movies.Remove(Movie);
        //            return RedirectToAction("Index");

        //        }
        //        else
        //        {
        //            return RedirectToAction("Delete", new { id = Movie.MovieId });
        //        }

        //    }
        //    return HttpNotFound();
        //}


    }
}