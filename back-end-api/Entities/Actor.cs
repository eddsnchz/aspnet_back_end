using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BACKEND.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BornDate { get; set; }
        public string Photo { get; set; }

        public List<MovieActor> MovieActor { get; set; }

    }
}
