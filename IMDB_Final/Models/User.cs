using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace IMDB_Final.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "This Feild is Requierd")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //Relationships(Many-to-Many)

        //public ICollection<Movie> Movies { get; set; }
        //public ICollection<Actor> Actors { get; set; }
        //public ICollection<Director> Directors { get; set; }


    }
}