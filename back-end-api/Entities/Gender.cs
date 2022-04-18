using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BACKEND.Validations;

namespace BACKEND.Entities
{
    public class Gender
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido.")]
        [StringLength(maximumLength:50)]
        [FirstCapitalLetterAttribute]
        public string Name { get; set; }

        public List<MovieGender> MovieGender { get; set; }
    }
}
