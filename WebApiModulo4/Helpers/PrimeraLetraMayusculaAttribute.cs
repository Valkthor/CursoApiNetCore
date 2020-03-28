using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiModulo4.Helpers
{
    // siempre tiene que estar terminado en atribute.
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute
    {
        // se sobre escribe el metodo is valid.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //value es el valor de la propiedad de donde se trae el atributo.
            //validationContext trae el origen de donde se esta ejecutando la validacion.

            // esto es para que no se repitan las validaciones, una validacion solo tiene que aplicarse a una sola cosa
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula");
            }

            return ValidationResult.Success;

        }
    }
}
