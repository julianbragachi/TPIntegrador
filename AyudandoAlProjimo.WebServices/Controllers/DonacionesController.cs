using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace AyudandoAlProjimo.WebServices.Controllers
{
    public class DonacionesController : ApiController
    {
        Entities context = new Entities();

        //visualizar todas las donaciones
        public IEnumerable<DonacionesViewModel> Get()
        {
            List<DonacionesViewModel> listaDonaciones = new List<DonacionesViewModel>();

            List<DonacionesInsumos> lista1 = context.DonacionesInsumos.ToList();

            List<DonacionesMonetarias> lista2 = context.DonacionesMonetarias.ToList();

            List<DonacionesHorasTrabajo> lista3 = context.DonacionesHorasTrabajo.ToList();

            foreach (var insumo in lista1)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesInsumos = insumo;
                dvm.tipo = "Insumos";
                listaDonaciones.Add(dvm);
            }

            foreach (var monetario in lista2)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesMonetarias = monetario;
                dvm.tipo = "Monetaria";
                listaDonaciones.Add(dvm);
            }

            foreach (var horastrabajo in lista3)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesHorasTrabajo = horastrabajo;
                dvm.tipo = "HorasTrabajo";
                listaDonaciones.Add(dvm);
            }

            return listaDonaciones;
        }
    }
}
