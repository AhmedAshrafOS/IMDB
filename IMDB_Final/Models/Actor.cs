using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class Actor
    {

        public int ActorId { get; set; }
        [Required(ErrorMessage = "This Feild is Requierd")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Feild is Requierd")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This Feild is Requierd")]
        public int Age { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Movie Image")]
        public string Image { get; set; }
        //public List<Movie> Movies_participate { get; set; }


        //Navigation Properties
        //User Connectios(Many-to-Many)
        //public ICollection<User> Users { get; set; }
        //Movies Connections (Many-to-Many)
        public virtual ICollection<MovesActors> MovesActors { get; set; }
        //public ICollection<Movie> Movies { get; set; }
    }
}