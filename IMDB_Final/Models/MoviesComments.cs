using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IMDB_Final.Models
{
    public class MoviesComments
    {
        [Key]
        public int CommentId { get; set; }
        public int UserId { get; set; }

        public int MovieId { get; set; }
        public string Name{ get; set; }
        public string comment { get; set; }

    }
}