using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BACKEND.Validations;

namespace BACKEND.Entities
{
    public class Gender: IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido.")]
        [StringLength(maximumLength:10)]
        //[FirstCapitalLetterAttribute]
        public string Name { get; set; }
        [Range(18,20)]
        public int age { get; set; }
        [CreditCard]
        public string creditCard { get; set; }
        [Url]
        public string url { get; set; }


        public Gender()
        {
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name[0].ToString();

                if (firstLetter != firstLetter.ToUpper())
                    yield return new ValidationResult("La primera letra debe ser mayuscula.");
            }
        }
    }
}
