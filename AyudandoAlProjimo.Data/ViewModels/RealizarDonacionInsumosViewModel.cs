using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class RealizarDonacionInsumosViewModel
    {
        public RealizarDonacionInsumosFormulario Formulario { get; set; }

        public Propuestas Propuesta { get; set; }
    }

    public class RealizarDonacionInsumosFormulario
    {
        [Required]
        public List<InsumosViewModel> Insumos { get; set; }
    }
}
