using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AyudandoAlProjimo.Data.ViewModels
{
    public class AgregarPropuestaMonetariaViewModel : AgregarPropuestaBase
    {
        [Required]
        public decimal Dinero { get; set; }
        [Required]
        public string CBU { get; set; }
    }
}
