using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class RegistroViewModel
    {
        //[Required]
        //[Display(Name = "Nombre")]
        //[StringLength(50, ErrorMessage = "Ingrese un nombre", MinimumLength = 1)]
        //public string Nombre { get; set; }
        //[Required]
        //[Display(Name = "Apellido")]
        //[StringLength(50, ErrorMessage = "Ingrese un apellido", MinimumLength = 1)]
        //public string Apellido { get; set; }
        [Required]
        [CustomValidation(typeof(RegistroViewModel), "ValidarEdadCorrespondiente")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,50}$",
        ErrorMessage = "La contraseña debe tener por lo menos una mayúscula y un número.")]
        [StringLength(20, ErrorMessage = "La contraseña debe tener al menos 8 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme su contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña no coincide con la anterior.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [CustomValidation(typeof(RegistroViewModel), "ValidarEmailUnico")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public static ValidationResult ValidarEmailUnico(object value, ValidationContext context)
        {
            if (!(context.ObjectInstance is RegistroViewModel usuario) || string.IsNullOrEmpty(usuario.Email))
                return new ValidationResult(string.Format("Email es requerido."));

            //para validar que no exista otro email igual, debo chequear en la base
            Entities db = new Entities();

            var existeEmail = db.Usuarios.Any(o => o.Email == usuario.Email);

            if (existeEmail)
            {
                return new ValidationResult(string.Format("El Email {0} ya está siendo usado.", usuario.Email));
            }

            return ValidationResult.Success;
        }
        public static ValidationResult ValidarEdadCorrespondiente(object value, ValidationContext context)
        {
            if (!(context.ObjectInstance is RegistroViewModel fecha) || string.IsNullOrEmpty(fecha.FechaNacimiento.ToLongDateString()))
                return new ValidationResult(string.Format("Fecha es requerida."));
            int edad = DateTime.Now.Year - fecha.FechaNacimiento.Year;
            if (edad >= 18)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format("No es mayor de 18"));
        }
    }
}
