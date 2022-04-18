using System;
namespace BACKEND.Entities
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        //Propiedades de navegacion
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }


        public string Character { get; set; }
        public int Order { get; set; }
    }
}
