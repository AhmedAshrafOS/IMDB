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
        public ActionResult CreateMovie(MovieDierctors movieDierctors)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var x ="";
                    if (!string.IsNullOrEmpty(movieDierctors.Movie.ToString()))
                    {



                            for (int i = 0; i < files.Count; i++)
                            {
                                HttpPostedFileBase file = files[i];
                                if (file.ContentType != "application/octet-stream") { 
                                byte[] ByteImgArray;
                                ByteImgArray = ConvertToBytes(file);
                                var ImageQuality = ConfigurationManager.AppSettings["ImageQuality"];
                                var reduceIMage = ReduceImageSize(ByteImgArray, ImageQuality);
                                string fileName = file.FileName;
                                x += fileName + ",";
                                string serverMapPath = Server.MapPath("~/imgs/ProfileMovie/");
                                string filePath = serverMapPath + "//" + fileName;
                                SaveFile(reduceIMage, filePath, file.FileName);
                                }
                            }
                        if (x != "") { 
                         movieDierctors.Movie.Image = x.Remove(x.Length - 1);
                         db.Movies.Add(movieDierctors.Movie);
                         }
                        else
                        {
                            movieDierctors.Movie.Image = null;
                            db.Movies.Add(movieDierctors.Movie);

                        }
                    }
                }
                catch
                {
                    return HttpNotFound();
                }


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
        public ActionResult EditMovie(MovieDierctors movieDierctors)
        {
            if (ModelState.IsValid)
            {
                var Movie = db.Movies.SingleOrDefault(a => a.MovieId == movieDierctors.Movie.MovieId);

                Movie.MovieName= movieDierctors.Movie.MovieName;
                Movie.Like = movieDierctors.Movie.Like;
                Movie.Dislike = movieDierctors.Movie.Dislike;
                Movie.DirectorId = movieDierctors.Movie.DirectorId;
                try
                {
                    HttpFileCollectionBase files = Request.Files;

                    var x = "";
                    if (!string.IsNullOrEmpty(movieDierctors.Movie.ToString()))
                    {



                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];
                            if (file.ContentType != "application/octet-stream")
                            {
                                byte[] ByteImgArray;
                                ByteImgArray = ConvertToBytes(file);
                                var ImageQuality = ConfigurationManager.AppSettings["ImageQuality"];
                                var reduceIMage = ReduceImageSize(ByteImgArray, ImageQuality);
                                string fileName = file.FileName;
                                x += fileName + ",";
                                string serverMapPath = Server.MapPath("~/imgs/ProfileMovie/");
                                string filePath = serverMapPath + "//" + fileName;
                                SaveFile(reduceIMage, filePath, file.FileName);
                            }
                        }
                        if (x != "")
                        {
                            movieDierctors.Movie.Image = x.Remove(x.Length - 1);
                            Movie.Image = movieDierctors.Movie.Image;
                        }
     
                    }
                }
                catch
                {
                    return HttpNotFound();
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