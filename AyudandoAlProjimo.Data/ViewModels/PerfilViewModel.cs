using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class PerfilViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ingrese un nombre", MinimumLength = 1)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Ingrese un apellido", MinimumLength = 1)]
        public string Apellido { get; set; }

        [CustomValidation(typeof(PerfilViewModel), "ValidarEdadCorrespondiente")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Foto { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public static ValidationResult ValidarEdadCorrespondiente(string value, ValidationContext context)
        {
            DateTime date;

            if (!DateTime.TryParse(value, out date)) return new ValidationResult("Formato de fecha incorrecto"); 

            int edad = DateTime.Now.Year - date.Year;

            if (edad >= 18)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format("No es mayor de 18"));
        }
    }
}
