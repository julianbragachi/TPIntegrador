using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DonacionesMonetariasViewModel
    {
        public int IdPropuestaDonacionMonetaria { get; set; }
        public decimal Dinero { get; set; }
        public int Estado { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
