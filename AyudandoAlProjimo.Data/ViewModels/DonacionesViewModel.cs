using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DonacionesViewModel
    {
        //public int idUsuario { get; set; }

        //public int IdDonacionHorasTrabajo { get; set; }

        //public int IdPropuestaDonacionHorasTrabajo { get; set; }

        //public int Cantidad { get; set; }

        //public int IdDonacionMonetaria { get; set; }

        //public int IdPropuestaDonacionMonetaria { get; set; }

        //public double Dinero { get; set; }

        //public int ArchivoTransferencia { get; set; }

        //public DateTime FechaCreacion { get; set; }

        //public int IdDonacionInsumo { get; set; }

        //public int IdPropuestaDonacionInsumo { get; set; }

        public DonacionesHorasTrabajo donacionesHorasTrabajo { get; set; }

        public DonacionesInsumos donacionesInsumos { get; set; }

        public DonacionesMonetarias donacionesMonetarias { get; set; }

        public string tipo { get; set; }

        public int total { get; set; }

    }
}
