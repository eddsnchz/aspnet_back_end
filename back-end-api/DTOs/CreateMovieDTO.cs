using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BACKEND.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.DTOs
{
    public class CreateMovieDTO
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Trailer { get; set; }
        public bool InCinemas { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType=typeof(TypeBinder<int>))]
        public List<int> GendersIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<int>))]
        public List<int> CinemasIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<CreateMovieActor>))]
        public List<CreateMovieActor> Actors { get; set; }
    }
}
