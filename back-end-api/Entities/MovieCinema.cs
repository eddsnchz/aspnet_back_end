using System;
namespace BACKEND.Entities
{
    public class MovieCinema
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }

        //Propiedades de navegacion
        public Movie Movie { get; set; }
        public Cinema Cinema { get; set; }
    }
}
