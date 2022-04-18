using System;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;

namespace BACKEND.DTOs
{
    public class CinemaDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        public string Name { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude {get; set;}
    }
}
