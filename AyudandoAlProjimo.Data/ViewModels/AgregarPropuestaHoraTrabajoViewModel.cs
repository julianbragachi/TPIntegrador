using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class AgregarPropuestaHoraTrabajoViewModel : AgregarPropuestaBase
    {
        [Required]
        public int CantidadHoras { get; set; }
        [Required]
        [StringLength(50)]
        public string Profesion { get; set; }
    }
}
