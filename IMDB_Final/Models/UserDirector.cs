using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class UserDirector
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public int DirectorId { get; set; }
        [ForeignKey("DirectorId")]
        public virtual Director Director { get; set; }
    }
}