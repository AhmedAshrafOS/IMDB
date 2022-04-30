using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class Director
    {

        public int DirectorId { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public int Age { get; set; }


        //Navigation Properties
        //User Connectios
        public ICollection<User> Users { get; set; }

        //Movie Connections
        public List<Movie> Movies_Produced { get; set; }

    }
}