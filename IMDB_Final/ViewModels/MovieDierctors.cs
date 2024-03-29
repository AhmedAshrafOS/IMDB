﻿using System;
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
        public List<Actor> Actors { get; set; }
        public List<User> Users { get; set; }
        public List<MoviesComments> MoviesComments { get; set; }
        public int[] Actorss { get; set; }
        public int[] Moviess { get; set; }
        public string[] Images { get; set; }
        public virtual Actor Actor { get; set; }
        public bool checkLike { get; set; }
        public bool checkdisLike { get; set; }
        //public int countLike { get; set; }
        //public int countDislike { get; set; }
        public MovieDierctors()
        {
            checkLike = false;
            checkdisLike = false;
        }

    }
}