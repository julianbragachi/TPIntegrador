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
        [Required(ErrorMessage = "Ingrese un nombre de usuario")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "Ingrese un nombre", MinimumLength = 1)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(50, ErrorMessage = "Ingrese un apellido", MinimumLength = 1)]
        public string Apellido { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/1/1930", "1/1/2001")]
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
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
