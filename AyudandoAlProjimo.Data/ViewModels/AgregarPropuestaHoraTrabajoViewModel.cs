using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AyudandoAlProjimo.Data.ViewModels
{
    class AgregarPropuestaHoraTrabajoViewModel : AgregarPropuestaBase
    {
        [Required]
        public int CantidadHoras { get; set; }
        [Required]
        public string Profesion { get; set; }
    }
}
