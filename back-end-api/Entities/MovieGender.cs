using System;
namespace BACKEND.Entities
{
    public class MovieGender
    {
        public int MovieId { get; set; }
        public int GenderId { get; set; }

        //Propiedades de navegacion
        public Movie Movie { get; set; }
        public Gender Gender { get; set; }
    }
}
