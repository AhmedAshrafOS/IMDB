using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace IMDB_Final.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        //public string Comment { get; set; }

        //Navigation Properties
        //User Connectios(Many-to-Many)
        public ICollection<User> Users { get; set; }
        //Actors Connections (Many-to-Many)
        public ICollection<Actor> Actors { get; set; }
        //Directos Connections (Many-to-One)
        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public Director Director { get; set; }

    }
}