using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class RealizarDonacionMonetariaViewModel
    {
        public RealizarDonacionMonetariaFormulario Formulario { get; set; }

        public Propuestas Propuesta { get; set; }
    }

    public class RealizarDonacionMonetariaFormulario
    {
        [Required]
        [CustomValidation(typeof(RealizarDonacionMonetariaFormulario), "ValidarDinero")]
        public decimal Dinero { get; set; }

        [Required]
        public string ArchivoTransferencia { get; set; }

        public static ValidationResult ValidarDinero(string value, ValidationContext context)
        {
            decimal money;

            if (!Decimal.TryParse(value, out money)) return new ValidationResult("El numero no es valido");
            if (money <= 0) return new ValidationResult("El dinero debe ser mayor que 0");



            return ValidationResult.Success;
        }
    }
}
