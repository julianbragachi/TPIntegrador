using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DonacionesViewModel
    {
        public DonacionesHorasTrabajo donacionesHorasTrabajo { get; set; }

        public DonacionesInsumos donacionesInsumos { get; set; }

        public DonacionesMonetarias donacionesMonetarias { get; set; }

        public string tipo { get; set; }

        public int total { get; set; }

        public DonacionesHorasTrabajoViewModel DonacionesHorasTrabajoVM { get; set; }
        public DonacionesInsumosViewModel DonacionesInsumosVM { get; set; } 
        public DonacionesMonetariasViewModel DonacionesMonetariasVM { get; set; }

    }
}
