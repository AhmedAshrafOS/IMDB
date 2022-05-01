using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDB_Final.Models;
namespace IMDB_Final.ViewModels
{
    public class UserFavActors
    {
        public List<Actor> Actors { get; set; }
        public List<User> Users { get; set; }
    }
}