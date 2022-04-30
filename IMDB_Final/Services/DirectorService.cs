using IMDB_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_Final.Services
{
    public interface IDirectorService
    {
        int Update(Director updatedDierctor);
        bool Delete(int id);
        Director ReadById(int id);
        List<Director> ReadAll();

        int Create(Director newDirector);
    }

    public class DirectorService : IDirectorService
    {
        private readonly AppDbContext db;

        public DirectorService()
        {
            db = new AppDbContext();
        }

        public int Create(Director newDirector)
        {

            var directorName = newDirector.FirstName.ToLower();
            var directorNameLast= newDirector.LastName.ToLower();
            var directorNameExists = db.Directors.Where(c => c.FirstName.ToLower() == directorName && c.LastName== directorNameLast).Any();

            if (directorNameExists)
            {
                return -2;
            }

            db.Directors.Add(newDirector);
            return db.SaveChanges();
        }

        public List<Director> ReadAll()
        {
            return db.Directors.ToList();
        }

        public Director ReadById(int id)
        {
            return db.Directors.Find(id);
        }

        public int Update(Director updatedDierctor)
        {
            var directorName = updatedDierctor.FirstName.ToLower();
            var directorNameLast = updatedDierctor.LastName.ToLower();
            var directorNameExists = db.Directors.Where(c => c.FirstName.ToLower() == directorName && c.LastName == directorNameLast).Any();

            if (directorNameExists)
            {
                return -2;
            }
            db.Directors.Attach(updatedDierctor);
            db.Entry(updatedDierctor).State=System.Data.Entity.EntityState.Modified;
           return db.SaveChanges();
        }
        public bool Delete(int id)
        {
            var director = ReadById(id);

            if (director != null)
            {
                db.Directors.Remove(director);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;

        }
    }
}