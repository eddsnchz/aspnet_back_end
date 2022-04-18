using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BACKEND.DTOs
{
    public class CreateActorDTO
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime BornDate { get; set; }
        public IFormFile Photo { get; set; }
    }
}
