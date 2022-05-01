using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMDB_Final.Models;
using IMDB_Final.ViewModels;
using IMDB_Final.Services;
namespace IMDB_Final.Controllers
{
    public class AdminDirectorController : Controller
    {
        private readonly DirectorService directorService;

        public AdminDirectorController()
        {
            directorService = new DirectorService();
        }

        // GET: AdminDirector
        public ActionResult Index()
        {
            var directors = directorService.ReadAll();
            return View(directors);
        }

        [HttpGet]
        // GET: Director/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Director/Create
        [HttpPost]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                int CreateResult = directorService.Create(director);
                if (CreateResult == -2)
                {
                    ViewBag.Message = "DierctorNameIsExist";
                    return View(director);
                }
                else
                return RedirectToAction("Index");

            }
            return View();
        }
        // POST: Director/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var currentDierctor = directorService.ReadById(id.Value);
            if (currentDierctor == null)
            {
                return HttpNotFound($"This Diecrtor ({id}) not found !");
            }
            var DierctorModel = new Director {
                DirectorId = currentDierctor.DirectorId,
                FirstName = currentDierctor.FirstName,
                Age = currentDierctor.Age,
                LastName = currentDierctor.LastName,
                Movies_Produced = currentDierctor.Movies_Produced
            };

            return View(DierctorModel);
        }

        [HttpPost]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                var updatedDierctor = new Director
                {
                    DirectorId = director.DirectorId,
                    FirstName = director.FirstName,
                    Age = director.Age,
                    LastName = director.LastName,
                    Movies_Produced = director.Movies_Produced
                };
               var result= directorService.Update(updatedDierctor);
                if (result == -2)
                {
                    ViewBag.Message = "DierctorNameIsExist";
                    return View(director);
                }
                else if (result > 0)
                    ViewBag.Message = "DierctorUpdated Successfully";
                else
                    ViewBag.Message = "Erorr";

            }
            return View(director);
        }
        // POST: Director/Edit


        // POST: Director/Delete
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var director = directorService.ReadById(id.Value);

                var directorInfo = new Director
                {
                    DirectorId = director.DirectorId,
                    FirstName = director.FirstName,
                    LastName = director.LastName,
                    Age = director.Age

                };

                return View(directorInfo);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                var deleted = directorService.Delete(id.Value);
                if (deleted)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new { id = id });
            }
            return HttpNotFound();
        }
        // POST: Director/Delete
        //DetailsDierctor
        //[HttpGet]
        //public ActionResult DirectorDetails()
        //{
        //    using (AppDbContext db = new AppDbContext())
        //    {

        //        var Movies = db.Movies.ToList();

        //        MovieDierctors AllDierctors = new MovieDierctors
        //        {
        //            Directors = Directors
        //        };
        //        return View(AllDierctors);
        //    }

        //}


        [HttpGet]
        public ActionResult DirectorDetails(int? id)
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
        //public ActionResult DirectorDetails(int? id)
        //{
        //    var directorDetails = directorService.ReadById(id.Value);
        //    if (directorDetails == null)
        //        return RedirectToAction("Index");
        //    return View(directorDetails);
        //}
    }
}