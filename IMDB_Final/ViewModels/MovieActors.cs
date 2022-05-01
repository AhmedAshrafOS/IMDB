using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDB_Final.Models;
namespace IMDB_Final.ViewModels
{
    public class MovieActors
    {
        public List<Actor> Actors { get; set; }
        public List<Movie> Movies { get; set; }
    }
}