using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class AgregarPropuestaBase
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string FechaFin { get; set; }

        [Required]
        public string TelefonoContacto { get; set; }

        [Required]
        public int TipoDonacion { get; set; }

        [Required]
        public string Foto { get; set; }

        [Required]
        public List<ReferenciasViewModel> Referencias { get; set; }
    }
}
