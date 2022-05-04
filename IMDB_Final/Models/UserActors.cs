using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class UserActors
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
    }
}