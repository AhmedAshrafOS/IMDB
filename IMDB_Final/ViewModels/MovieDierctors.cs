using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDB_Final.Models;

namespace IMDB_Final.ViewModels
{
    public class MovieDierctors
    {
        public Movie Movie { get; set; }
        public Director Director { get; set; }
        public List<Director> Directors { get; set; }
        public List<Movie> Movies { get; set; }

    }
}