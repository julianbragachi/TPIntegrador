using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DonacionesInsumosViewModel
    {
        public int IdPropuestaDonacionInsumo { get; set; }
        public int Cantidad { get; set; }
        public int Estado { get; set; }
        public string Nombre { get; set; }
        public string NombreDonado { get; set; }
    }
}
