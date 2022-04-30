using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        //public List<Movie> Movies_participate { get; set; }


        //Navigation Properties
        //User Connectios(Many-to-Many)
        public ICollection<User> Users { get; set; }
        //Movies Connections (Many-to-Many)
        public ICollection<Movie> Movies { get; set; }
    }
}