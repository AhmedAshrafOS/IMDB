using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDB_Final.Models;
namespace IMDB_Final.ViewModels
{
    public class MovieActors
    {
        public int ActorIdM { get; set; }
        public int MovieIdM { get; set; }

        public virtual Movie Movies { get; set; }
    }
}