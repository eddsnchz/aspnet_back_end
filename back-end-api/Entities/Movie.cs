using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BACKEND.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }

        public List<MovieActor> MovieActor { get; set; }
        public List<MovieGender> MovieGender { get; set; }
        public List<MovieCinema> MovieCinema { get; set; }
    }
}
