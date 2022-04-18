using System;
using System.ComponentModel.DataAnnotations;

namespace BACKEND.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BornDate { get; set; }
        public string Photo { get; set; }
    }
}
