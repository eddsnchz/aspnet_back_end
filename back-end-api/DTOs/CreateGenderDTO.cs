using System;
using System.ComponentModel.DataAnnotations;
using BACKEND.Validations;

namespace BACKEND.DTOs
{
    public class CreateGenderDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(maximumLength: 50)]
        [FirstCapitalLetterAttribute]
        public string Name { get; set; }

    }
}
