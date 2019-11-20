using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class PropuestaViewModel
    {
        public Propuestas Propuesta { get; set; }
        public List<DonacionesHorasTrabajo> DonacionesHorasTrabajo { get; set; }
        public List<DonacionesInsumos> DonacionesInsumos { get; set; }
        public List<DonacionesMonetarias> DonacionesMonetarias { get; set; }
        public Usuarios UsuarioCreador { get; set; }
        public int PorcentajeRealizacion { get; set; }
    }
}
