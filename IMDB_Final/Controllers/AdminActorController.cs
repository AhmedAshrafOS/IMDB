using IMDB_Final.Models;
using IMDB_Final.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IMDB_Final.Controllers
{
    public class AdminActorController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: AdminMovie

        [HttpGet]
        public ActionResult Index()
        {
  
            MovieDierctors movieDierctors = new MovieDierctors();
            movieDierctors.Actors= db.Actors.ToList();
            foreach (var item in movieDierctors.Actors)
            {
                if (item.Image != null)
                {
                    movieDierctors.Images = item.Image.Split(new char[] { ',' });
                }

            }

            return View(movieDierctors);
        }

        [HttpGet]
        public ActionResult CreateActor()
        {

            var Movie = db.Movies.ToList();
            MovieDierctors movieDierctors = new MovieDierctors
            {
                Movies = Movie,
            };



            return View(movieDierctors);
        }
        [HttpPost]
        public ActionResult CreateActor(MovieDierctors movieDierctors)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ImageUpload objTaskDetails = new ImageUpload();
                    HttpFileCollectionBase files = Request.Files;
                    var x = "";
                    if (!string.IsNullOrEmpty(movieDierctors.Actor.ToString()))
                    {
                        if (movieDierctors.Actor.Image != null)
                        {
                            for (int i = 0; i < files.Count; i++)
                            {
                                    HttpPostedFileBase file = files[i];
                                    byte[] ByteImgArray;
                                    ByteImgArray = ConvertToBytes(file);
                                    var ImageQuality = ConfigurationManager.AppSettings["ImageQuality"];
                                    var reduceIMage = ReduceImageSize(ByteImgArray, ImageQuality);
                                    string fileName = file.FileName;
                                    x += fileName + ",";
                                    string serverMapPath = Server.MapPath("~/imgs/ProfileActor/");
                                    string filePath = serverMapPath + "//" + fileName;
                                    SaveFile(reduceIMage, filePath, file.FileName);
                            }
                            movieDierctors.Actor.Image = x.Remove(x.Length - 1);
                            db.Actors.Add(movieDierctors.Actor);
                        }
                        else
                        {
                            movieDierctors.Actor.Image = null;
                            db.Actors.Add(movieDierctors.Actor);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                foreach (var item in movieDierctors.Moviess)
                {
                    MovesActors MovesActors = new MovesActors()
                    {

                        MovieId = item,

                        ActorId = movieDierctors.Actor.ActorId
                    };

                    db.MovesActors.Add(MovesActors);

                }
                db.SaveChanges();

                //movieDierctors.Movie.Actors = movieDierctors.Actors;


                return RedirectToAction("Index");

            }
            return View();


        }


        public ActionResult DetailsActor(int id)
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


        [HttpGet]

        public ActionResult EditActor(int? id)
        {
            if (id != null)
            {
                List<int> MovieList = new List<int>();
                var Actor = db.Actors.SingleOrDefault(x => x.ActorId == id);
                var MovieID = db.MovesActors.Where(x => x.ActorId == id).ToList();
                var Movies = db.Movies.ToList();
                foreach (var iteam in MovieID)
                {
                    MovieList.Add((int)iteam.MovieId);
                }
                if (Actor == null)
                {
                    return HttpNotFound();
                }

                MovieDierctors movieDierctors = new MovieDierctors
                {
                    Movies = Movies,
                    Actor = Actor,
                    Moviess = MovieList.ToArray(),
                };

                return View(movieDierctors);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditActor(MovieDierctors movieDierctors)
        {
            if (ModelState.IsValid)
            {
                var Actor = db.Actors.SingleOrDefault(a => a.ActorId == movieDierctors.Actor.ActorId);

                Actor.FirstName = movieDierctors.Actor.FirstName;
                Actor.LastName = movieDierctors.Actor.LastName;
                Actor.Age = movieDierctors.Actor.Age;


                try
                {
                    //ImageUpload objTaskDetails = new ImageUpload();
                    HttpFileCollectionBase files = Request.Files;
                    var x = "";
                    if (!string.IsNullOrEmpty(movieDierctors.Actor.ToString()))
                    {
                        if (movieDierctors.Actor.Image != null) { 
                            for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];
                            byte[] ByteImgArray;
                            ByteImgArray = ConvertToBytes(file);
                            var ImageQuality = ConfigurationManager.AppSettings["ImageQuality"];
                            var reduceIMage = ReduceImageSize(ByteImgArray, ImageQuality);
                            string fileName = file.FileName;
                            x += fileName + ",";
                            string serverMapPath = Server.MapPath("~/imgs/ProfileActor/");
                            string filePath = serverMapPath + "//" + fileName;
                            SaveFile(reduceIMage, filePath, file.FileName);
                        }
                        movieDierctors.Actor.Image = x.Remove(x.Length - 1);
                            Actor.Image = movieDierctors.Actor.Image;
                        }
                        else
                            Actor.Image = null;
                    }
                }
                catch 
                {

                    return HttpNotFound();
                }


                var MovieActors = db.MovesActors.Where(u => u.ActorId == movieDierctors.Actor.ActorId).ToList();
                foreach (var item in MovieActors)
                {
                    db.MovesActors.Remove(item);
                }
                foreach (var item in movieDierctors.Moviess)
                {


                    //MovieActors.MovieId = movieDierctors.Movie.MovieId;

                    MovesActors MovesActors = new MovesActors()
                    {

                        ActorId = movieDierctors.Actor.ActorId,

                        MovieId = item
                    };

                    db.MovesActors.Add(MovesActors);

                }
                db.Entry(Actor).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DetailsActor", new { id = Actor.ActorId });
            }
            return View();
        }

        // POST: Director/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Actor actor = db.Actors.Find(id);
            if (actor == null)
                return HttpNotFound();

            return View(actor);
        }

        // POST: Director/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Actor actor)
        {
            try
            {
                var MovieActors = db.MovesActors.Where(u => u.ActorId == actor.ActorId).ToList();
                foreach (var item in MovieActors)
                {
                    db.MovesActors.Remove(item);
                }
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                actor = db.Actors.Find(id);
                if (actor == null)
                    return HttpNotFound();
                db.Actors.Remove(actor);
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

        #region ImageResize
        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] CoverImageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            CoverImageBytes = reader.ReadBytes((int)image.ContentLength);
            return CoverImageBytes;
        }
        public static byte[] ReduceImageSize(byte[] inputBytes, string ImageQuality)
        {
            Byte[] imageBytes;
            int jpegQuality;

            //string ImageQuality = "";/*ConfigurationManager.AppSettings["ImageQuality"];*/
            if (!string.IsNullOrEmpty(ImageQuality))
            {
                jpegQuality = Convert.ToInt32(ImageQuality);
            }
            else
            {
                jpegQuality = 25;
            }

            System.Drawing.Image image;

            using (var inputStream = new MemoryStream(inputBytes))
            {
                // Create an Encoder object based on the GUID  for the Quality parameter category.  
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                image = System.Drawing.Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                encoderParameters.Param[0] = myEncoderParameter;
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    imageBytes = outputStream.ToArray();
                }
            }
            return imageBytes;
        }
        public string SaveFile(byte[] file, string filePath, string filename)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
                System.IO.Directory.CreateDirectory(directoryPath);

            if (file != null)
            {
                using (System.Drawing.Image image = System.Drawing.Image.FromStream(new System.IO.MemoryStream(file)))
                {
                    //var i = Image.FromFile(filePath + file);
                    //var i2 = new Bitmap(i);
                    if (filename.ToLower().Contains(".jpg") || filename.ToLower().Contains(".jpeg"))
                    {
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        //i2.Save(filePath, ImageFormat.Jpeg);
                    }
                    else if (filename.ToLower().Contains(".png"))
                    {
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        //i2.Save(filePath, ImageFormat.Png);
                    }
                    else if (filename.ToLower().Contains(".bmp"))
                    {
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
                        //i2.Save(filePath, ImageFormat.Bmp);
                    }
                    else if (filename.ToLower().Contains(".gif"))
                    {
                        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Gif);
                        //i2.Save(filePath, ImageFormat.Gif);
                    }
                }
            }
            return string.Empty;
        }

        #endregion
    }
}