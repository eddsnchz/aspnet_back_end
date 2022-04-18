using System;
using System.Collections.Generic;

namespace BACKEND.DTOs
{
    public class MoviesPostGetDTO
    {
        public List<GenderDTO> Gender { get; set; }
        public List<CinemaDTO> Cinema { get; set; }
    }
}
