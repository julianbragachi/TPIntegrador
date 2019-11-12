using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DonacionMonetariaViewModel
    {
        [CustomValidation(typeof(AgregarPropuestaViewModel), "ValidarDonacion")]
        public decimal Dinero { get; set; }
        [CustomValidation(typeof(AgregarPropuestaViewModel), "ValidarDonacion")]
        public string CBU { get; set; }

        public static ValidationResult ValidarDonacion(object value, ValidationContext context)
        {
            return ValidationResult.Success;
        }
    }
}
