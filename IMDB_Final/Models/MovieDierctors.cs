using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDB_Final.Models;

namespace IMDB_Final.Models
{
    public class MovieDierctors
    {
        public Movie Movie { get; set; }
        public List<Director> Directors { get; set; }
      
    }
}