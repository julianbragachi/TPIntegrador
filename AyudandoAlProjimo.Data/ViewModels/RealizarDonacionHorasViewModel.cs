using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class RealizarDonacionHorasViewModel
    {
        public RealizarDonacionHorasFormulario Formulario { get; set; }

        public Propuestas Propuesta { get; set; }
    }

    public class RealizarDonacionHorasFormulario
    {
        [Required]
        public int Profesion { get; set; }

        [Required]
        [Range(1,Int32.MaxValue, ErrorMessage = "No se puede donar horas negativas o nulas.")]
        public int Cantidad { get; set; }
    }
}
