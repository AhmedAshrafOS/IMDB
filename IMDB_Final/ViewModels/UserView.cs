using IMDB_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMDB_Final.ViewModels
{
    public class UserView
    {
        public User User { get; set; }
        public List<Director> Directors { get; set; }
        public List<Movie> Movies { get; set; }
        public List<Actor> Actors { get; set; }
        public int[] Actorss { get; set; }
        public int[] Moviess { get; set; }
        public int[] Directorss { get; set; }
    }
}