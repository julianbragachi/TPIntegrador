using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public PropuestasDonacionesHorasTrabajo PropuestaDonacionesHorasTrabajo { get; set; }
        public PropuestasDonacionesMonetarias PropuestasDonacionesMonetarias { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "No se permiten horas negativas o cero.")]
        public int CantidadHoras { get; set; }

        [Range(1, 100000000000, ErrorMessage = "No se permiten monto negativas o cero.")]
        public decimal Dinero { get; set; }
    }
}
