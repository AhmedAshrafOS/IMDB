using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class UserMovies
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        public bool Like { get; set; }
        public bool disLike { get; set; }

        public UserMovies(){
            Like = false;
            disLike = false;
        }
    }
}