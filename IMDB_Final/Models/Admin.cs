using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required]
        [Display(Name ="UserName")]
        public string AdminName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}