using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;

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
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This Feild is Requierd")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation Password is Required")]
        [Compare("Password", ErrorMessage = "Password Must Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Profile Photo")]
        public string Image { get; set; }
        //Relationships(Many-to-Many)
        public virtual ICollection<UserActors> UserActors { get; set; }
        public virtual ICollection<UserDirector> UserDirector { get; set; }
        public virtual ICollection<UserMovies> UserMovies { get; set; }



    }
}